using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Resolver;
using AI.GOAP.Goals;
using Interfaces;
using AI.GOAP.WorldKeys;

namespace AI.GOAP.Factories.Extensions
{
    public static class GoalExtensions
    {
        public static void AddWanderGoal(this GoapSetBuilder builder)
        {
            builder.AddGoal<WanderGoal>()
                .AddCondition<IsWandering>(Comparison.GreaterThanOrEqual, 1);
        }

        public static void AddCreateItemGoal<T>(this GoapSetBuilder builder)
            where T : ICreatable
        {
            builder.AddGoal<CreateItemGoal<T>>()
                .AddCondition<CreatedItem<T>>(Comparison.GreaterThanOrEqual, 999);
        }

        public static void AddCleanItemsGoal(this GoapSetBuilder builder)
        {
            builder.AddGoal<CleanItemsGoal>()
                .AddCondition<ItemsOnFloor>(Comparison.SmallerThanOrEqual, 0);
        }

        public static void AddFixHungerGoal(this GoapSetBuilder builder)
        {
            builder.AddGoal<FixHungerGoal>()
                .AddCondition<IsHungry>(Comparison.SmallerThanOrEqual, 0);
        }
        
        public static void AddGatherItemGoal<T>(this GoapSetBuilder builder)
            where T : IGatherable
        {
            builder.AddGoal<GatherItemGoal<T>>()
                .AddCondition<IsInWorld<T>>(Comparison.GreaterThanOrEqual, 999);
        }
        
        public static void AddPickupItemGoal<T>(this GoapSetBuilder builder)
            where T : IHoldable
        {
            builder.AddGoal<PickupItemGoal<T>>()
                .AddCondition<IsHolding<T>>(Comparison.GreaterThanOrEqual, 1);
        }
    }
}