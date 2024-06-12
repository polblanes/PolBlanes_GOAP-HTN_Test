using System;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Resolver;
using Behaviours.GOAP;
using Classes.Items.GOAP;
using Classes.Sources.GOAP;
using Interfaces;
using AI.GOAP.Targets;
using AI.GOAP.WorldKeys;

namespace AI.GOAP.Factories.Extensions
{
    public static class ActionExtensions
    {
        public static void AddWanderAction(this GoapSetBuilder builder)
        {
            builder.AddAction<Actions.Wander>()
                .SetTarget<WanderTarget>()
                .AddEffect<IsWandering>(EffectType.Increase);
        }
        
        public static void AddPickupItemAction<T>(this GoapSetBuilder builder)
            where T : class, IHoldable
        {
            builder.AddAction<Actions.PickupItem<T>>()
                .SetTarget<ClosestTarget<T>>()
                .AddEffect<IsHolding<T>>(EffectType.Increase)
                .AddCondition<IsInWorld<T>>(Comparison.GreaterThanOrEqual, 1);
        }
        
        public static void AddGatherItemAction<TGatherable, TRequired>(this GoapSetBuilder builder)
            where TGatherable : ItemBase, IGatherable
            where TRequired : IHoldable
        {
            builder.AddAction<Actions.GatherItem<TGatherable>>()
                .SetTarget<ClosestSourceTarget<TGatherable>>()
                .AddEffect<IsInWorld<TGatherable>>(EffectType.Increase)
                .AddCondition<IsHolding<TRequired>>(Comparison.GreaterThanOrEqual, 1);
        }
        
        public static void AddGatherItemSlowAction<TGatherable>(this GoapSetBuilder builder)
            where TGatherable : ItemBase, IGatherable
        {
            builder.AddAction<Actions.GatherItem<TGatherable>>()
                .SetTarget<ClosestSourceTarget<TGatherable>>()
                .AddEffect<IsInWorld<TGatherable>>(EffectType.Increase)
                .SetBaseCost(3);
        }
        
        public static void AddCreateItemAction<T>(this GoapSetBuilder builder)
            where T : ItemBase, ICreatable
        {
            var action = builder.AddAction<Actions.CreateItem<T>>()
                .SetTarget<ClosestTarget<AnvilSource>>()
                .AddEffect<CreatedItem<T>>(EffectType.Increase)
                .AddEffect<IsInWorld<T>>(EffectType.Increase);
            
            if (typeof(T) == typeof(Axe))
            {
                action
                    .AddCondition<IsHolding<Iron>>(Comparison.GreaterThanOrEqual, 1)
                    .AddCondition<IsHolding<Wood>>(Comparison.GreaterThanOrEqual, 2);
                return;
            }
            
            if (typeof(T) == typeof(Pickaxe))
            {
                action
                    .AddCondition<IsHolding<Iron>>(Comparison.GreaterThanOrEqual, 2)
                    .AddCondition<IsHolding<Wood>>(Comparison.GreaterThanOrEqual, 1);
                return;
            }

            throw new Exception("No conditions set for this type of item!");
        }
        
        public static void AddEatAction(this GoapSetBuilder builder)
        {
            builder.AddAction<Actions.Eat>()
                .SetTarget<TransformTarget>()
                .AddEffect<IsHungry>(EffectType.Decrease)
                .AddCondition<IsHolding<IEatable>>(Comparison.GreaterThanOrEqual, 1);
        }
    }
}