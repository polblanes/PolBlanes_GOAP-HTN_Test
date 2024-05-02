using FluidHTN;
using System.Collections;
using System.Collections.Generic;
using FluidHTN.Factory;
using FluidHTN.PrimitiveTasks;
using UnityEngine;
using HTN;
using HTN.Conditions;
using HTN.Effects;
using HTN.Operators;
using Classes.Items.HTN;
using Unity.VisualScripting;
using AI.GOAP.WorldKeys;
using Classes.Sources.HTN;

namespace HTN.Domain
{
    public class AIDomainBuilder : BaseDomainBuilder<AIDomainBuilder, AIContext>
    {
        public AIDomainBuilder(string domainName) : base(domainName, new DefaultFactory())
        {
        }

        public AIDomainBuilder HasState(AIWorldState state)
        {
            var condition = new HasWorldStateCondition(state);
            Pointer.AddCondition(condition);
            return this;
        }

        public AIDomainBuilder HasState(AIWorldState state, byte value)
        {
            var condition = new HasWorldStateCondition(state, value);
            Pointer.AddCondition(condition);
            return this;
        }

        public AIDomainBuilder HasStateGreaterThan(AIWorldState state, byte value)
        {
            var condition = new HasWorldStateGreaterThanCondition(state, value);
            Pointer.AddCondition(condition);
            return this;
        }

        public AIDomainBuilder HasStateSmallerThan(AIWorldState state, byte value)
        {
            var condition = new HasWorldStateSmallerThanCondition(state, value);
            Pointer.AddCondition(condition);
            return this;
        }

        public AIDomainBuilder SetState(AIWorldState state, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new SetWorldStateEffect(state, type);
                task.AddEffect(effect);
            }
            return this;
        }

        public AIDomainBuilder SetState(AIWorldState state, bool value, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new SetWorldStateEffect(state, value, type);
                task.AddEffect(effect);
            }
            return this;
        }

        public AIDomainBuilder SetState(AIWorldState state, byte value, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new SetWorldStateEffect(state, value, type);
                task.AddEffect(effect);
            }
            return this;
        }

        public AIDomainBuilder IncrementState(AIWorldState state, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new IncrementWorldStateEffect(state, type);
                task.AddEffect(effect);
            }
            return this;
        }

        public AIDomainBuilder IncrementState(AIWorldState state, byte value, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new IncrementWorldStateEffect(state, value, type);
                task.AddEffect(effect);
            }
            return this;
        }

        public AIDomainBuilder Wait(float waitTime)
        {
            Action("Wait");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new WaitOperator(waitTime));
            }
            End();
            return this;
        }

        public AIDomainBuilder FindWanderTarget()
        {
            Action("Find wander target");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new FindWanderTargetOperator());

                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPosition,   true,       EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsTree,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsMine,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsIron,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsWood,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAxe,        false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPickaxe,    false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAnvil,      false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsApple,      false,      EffectType.PlanAndExecute));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder FindExistingAppleTarget()
        {
            Action("Find apple item target");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new FindClosestOperator<Apple>());

                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.AppleIsInWorld, 0));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPosition,   false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsTree,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsMine,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsIron,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsWood,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAxe,        false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPickaxe,    false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAnvil,      false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsApple,      true,       EffectType.PlanAndExecute));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder FindExistingAxeTarget()
        {
            Action("Find axe item target");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new FindClosestOperator<Axe>());

                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.AxeIsInWorld, 0));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPosition,   false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsTree,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsMine,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsIron,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsWood,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAxe,        true,       EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPickaxe,    false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAnvil,      false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsApple,      false,      EffectType.PlanAndExecute));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder FindExistingPickaxeTarget()
        {
            Action("Find pickaxe item target");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new FindClosestOperator<Pickaxe>());

                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.PickaxeIsInWorld, 0));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPosition,   false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsTree,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsMine,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsIron,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsWood,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAxe,        false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPickaxe,    true,       EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAnvil,      false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsApple,      false,      EffectType.PlanAndExecute));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder FindExistingWoodTarget()
        {
            Action("Find wood item target");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new FindClosestOperator<Wood>());

                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.WoodIsInWorld, 0));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPosition,   false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsTree,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsMine,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsIron,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsWood,       true,       EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAxe,        false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPickaxe,    false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAnvil,      false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsApple,      false,      EffectType.PlanAndExecute));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder FindExistingIronTarget()
        {
            Action("Find iron item target");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new FindClosestOperator<Iron>());

                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.IronIsInWorld, 0));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPosition,   false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsTree,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsMine,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsIron,       true,       EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsWood,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAxe,        false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPickaxe,    false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAnvil,      false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsApple,      false,      EffectType.PlanAndExecute));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder FindTreeTarget()
        {
            Action("Find tree target");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new FindClosestSourceOperator<Wood>());

                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.TreeIsInWorld, 0));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPosition,   false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsTree,       true,       EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsMine,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsIron,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsWood,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAxe,        false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPickaxe,    false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAnvil,      false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsApple,      false,      EffectType.PlanAndExecute));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder FindIronMineTarget()
        {
            Action("Find iron mine target");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new FindClosestSourceOperator<Iron>());

                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.MineIsInWorld, 0));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPosition,   false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsTree,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsMine,       true,       EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsIron,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsWood,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAxe,        false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPickaxe,    false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAnvil,      false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsApple,      false,      EffectType.PlanAndExecute));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder FindAnvilTarget()
        {
            Action("Find iron anvil target");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new FindClosestOperator<AnvilSource>());

                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.AnvilIsInWorld, 0));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPosition,   false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsTree,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsMine,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsIron,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsWood,       false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAxe,        false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPickaxe,    false,      EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAnvil,      true,       EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsApple,      false,      EffectType.PlanAndExecute));

                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder MoveToTarget()
        {
            Action("Move to target");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new MoveToOperator());
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, true, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder PickUpWoodItem()
        {
            Action("Pick up wood");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new PickUpTargetItemOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsWood));
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.HasWood, 1, EffectType.PlanOnly));
            }
            End();
            return this;
        }

        public AIDomainBuilder PickUpIronItem()
        {
            Action("Pick up iron");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new PickUpTargetItemOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsIron));
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.HasIron, 1, EffectType.PlanOnly));
            }
            End();
            return this;
        }

        public AIDomainBuilder PickUpAppleItem()
        {
            Action("Pick up apple");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new PickUpTargetItemOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsApple));
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.HasApple, 1, EffectType.PlanOnly));
            }
            End();
            return this;
        }

        public AIDomainBuilder PickUpAxeItem()
        {
            Action("Pick up axe");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new PickUpTargetItemOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsAxe));
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.HasAxe, 1, EffectType.PlanOnly));
            }
            End();
            return this;
        }

        public AIDomainBuilder PickUpPickaxeItem()
        {
            Action("Pick up pickaxe");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new PickUpTargetItemOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsPickaxe));
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.HasPickaxe, 1, EffectType.PlanOnly));
            }
            End();
            return this;
        }

        public AIDomainBuilder GatherWoodFromSource()
        {
            Action("Gather wood");
            if (Pointer is IPrimitiveTask task)
            {
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsTree));
                task.AddCondition(new HasWorldStateCondition(AIWorldState.HasTargetClose));
                task.SetOperator(new GatherItemFromSourceOperator<Wood, Axe>());
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.WoodIsInWorld, 1, EffectType.PlanOnly));
            }
            End();
            return this;
        }

        public AIDomainBuilder GatherIronFromSource()
        {
            Action("Gather iron");
            if (Pointer is IPrimitiveTask task)
            {
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsMine));
                task.AddCondition(new HasWorldStateCondition(AIWorldState.HasTargetClose));
                task.SetOperator(new GatherItemFromSourceOperator<Iron, Pickaxe>());
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.IronIsInWorld, 1, EffectType.PlanOnly));
            }
            End();
            return this;
        }

        public AIDomainBuilder EatAppleItem()
        {
            Action("Eat apple");
            if (Pointer is IPrimitiveTask task)
            {
                task.AddCondition(new HasWorldStateCondition(AIWorldState.IsHungry));
                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.HasApple, 0));
                task.SetOperator(new EatEatableOperator<Apple>());
                task.AddEffect(new SetWorldStateEffect(AIWorldState.IsHungry, false, EffectType.PlanOnly));
            }
            End();
            return this;
        }

        public AIDomainBuilder CreateAxe()
        {
            Action("Create axe");
            if (Pointer is IPrimitiveTask task)
            {
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsAnvil));
                task.AddCondition(new HasWorldStateCondition(AIWorldState.HasTargetClose));
                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.HasIron, 0));
                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.HasWood, 0));
                task.SetOperator(new CreateItemOperator<Axe>());
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.AxeIsInWorld, 1, EffectType.PlanOnly));
            }
            End();
            return this;
        }

        public AIDomainBuilder CreatePickaxe()
        {
            Action("Create pickaxe");
            if (Pointer is IPrimitiveTask task)
            {
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsAnvil));
                task.AddCondition(new HasWorldStateCondition(AIWorldState.HasTargetClose));
                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.HasIron, 0));
                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.HasWood, 0));
                task.SetOperator(new CreateItemOperator<Pickaxe>());
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.PickaxeIsInWorld, 1, EffectType.PlanOnly));
            }
            End();
            return this;
        }

        ////////////////////////////////////////////////
        // Sub domains
        ////////////////////////////////////////////////
        
        public AIDomainBuilder SubDomain_PickUpWoodFromWorld()
        {
            var SubDomain = new AIDomainBuilder("Pick up wood from world")
                .Sequence("Pick up wood from world")
                    .FindExistingWoodTarget()
                    .MoveToTarget()
                    .PickUpWoodItem()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_PickUpIronFromWorld()
        {
            var SubDomain = new AIDomainBuilder("Pick up iron from world")
                .Sequence("Pick up iron from world")
                    .FindExistingIronTarget()
                    .MoveToTarget()
                    .PickUpIronItem()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_PickUpAxeFromWorld()
        {
            var SubDomain = new AIDomainBuilder("Pick up axe from world")
                .HasStateSmallerThan(AIWorldState.HasAxe, 1)
                .Sequence("Pick up axe from world")
                    .FindExistingAxeTarget()
                    .MoveToTarget()
                    .PickUpAxeItem()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_PickUpPickaxeFromWorld()
        {
            var SubDomain = new AIDomainBuilder("Pick up pickaxe from world")
                .HasStateSmallerThan(AIWorldState.HasPickaxe, 1)
                .Sequence("Pick up pickaxe from world")
                    .FindExistingPickaxeTarget()
                    .MoveToTarget()
                    .PickUpPickaxeItem()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GatherWoodBase()
        {
            var SubDomain = new AIDomainBuilder("Gather wood")
                .Sequence("Gather wood")
                    .HasStateSmallerThan(AIWorldState.WoodIsInWorld, 2)
                    .FindTreeTarget()
                    .MoveToTarget()
                    .GatherWoodFromSource()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GatherIronBase()
        {
            var SubDomain = new AIDomainBuilder("Gather iron")
                .HasStateSmallerThan(AIWorldState.IronIsInWorld, 2)
                .Sequence("Gather iron")
                    .FindIronMineTarget()
                    .MoveToTarget()
                    .GatherIronFromSource()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GatherWoodWithAxe()
        {
            var SubDomain = new AIDomainBuilder("Gather wood with axe")
                .Select("Gather Wood with or without axe")
                    .HasStateSmallerThan(AIWorldState.WoodIsInWorld, 2)
                    .SubDomain_PickUpAxeFromWorld()
                    .SubDomain_GatherWoodBase()
                .End()
                .Build();
            
            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GatherIronWithPickaxe()
        {
            var SubDomain = new AIDomainBuilder("Gather iron with pickaxe")
                .HasStateSmallerThan(AIWorldState.IronIsInWorld, 2)
                .Select("Gather iron with or without pickaxe")
                    .SubDomain_PickUpPickaxeFromWorld()
                    .SubDomain_GatherIronBase()
                .End()
                .Build();
            
            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_Wander()
        {
            var SubDomain = new AIDomainBuilder("Wander")
                .Sequence("Wander")
                    .FindWanderTarget()
                    .MoveToTarget()
                    .Wait(3.0f)
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GetApple()
        {
            var SubDomain = new AIDomainBuilder("Get apple")
                .Sequence("Go pick up apple")
                    .HasStateSmallerThan(AIWorldState.HasApple, 1)
                    .FindExistingAppleTarget()
                    .MoveToTarget()
                    .PickUpAppleItem()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_EatApple()
        {
            var SubDomain = new AIDomainBuilder("Eat apple")
                .Sequence("Eat apple")
                    .HasStateGreaterThan(AIWorldState.HasApple, 0)
                    .EatAppleItem()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_Eat()
        {
            var SubDomain = new AIDomainBuilder("Eat")
                .HasState(AIWorldState.IsHungry)
                .Select("Get apple and eat it")
                    .SubDomain_GetApple()
                    .SubDomain_EatApple()
                .End()
                .Build();
            
            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GetIron()
        {
            var SubDomain = new AIDomainBuilder("Get iron")
                .HasStateSmallerThan(AIWorldState.HasIron, 1)
                .Select("Pick up or gather iron")
                    .SubDomain_PickUpIronFromWorld()
                    .SubDomain_GatherIronWithPickaxe()
                .End()
                .Build();
            
            return this.Splice(SubDomain);                    
        }

        public AIDomainBuilder SubDomain_GetWood()
        {
            var SubDomain = new AIDomainBuilder("Get wood")
                .HasStateSmallerThan(AIWorldState.HasWood, 1)
                .Select("Pick up or gather wood")
                    .SubDomain_PickUpWoodFromWorld()
                    .SubDomain_GatherWoodWithAxe()
                .End()
                .Build();
            
            return this.Splice(SubDomain);                    
        }

        public AIDomainBuilder SubDomain_GetMaterialsForTool()
        {
            var SubDomain = new AIDomainBuilder("Get materials for tool")
                .Select("Get materials for tool")
                    .SubDomain_GetWood()
                    .SubDomain_GetIron()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GoToAnvilAndMakeAxe()
        {
            var SubDomain = new AIDomainBuilder("Go to anvil and make axe")
                .Sequence("Go to anvil and make axe")
                    .FindAnvilTarget()
                    .MoveToTarget()
                    .CreateAxe()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GoToAnvilAndMakePickaxe()
        {
            var SubDomain = new AIDomainBuilder("Go to anvil and make pickaxe")
                .Sequence("Go to anvil and make pickaxe")
                    .FindAnvilTarget()
                    .MoveToTarget()
                    .CreatePickaxe()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_CraftAxe()
        {
            var SubDomain = new AIDomainBuilder("Craft Axe")
                .HasStateSmallerThan(AIWorldState.AxeIsInWorld, 1)
                .HasStateSmallerThan(AIWorldState.HasAxe, 1)
                .Select("Get materials and make axe")
                    .SubDomain_GetMaterialsForTool()
                    .SubDomain_GoToAnvilAndMakeAxe()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_CraftPickaxe()
        {
            var SubDomain = new AIDomainBuilder("Craft Pickaxe")
                .HasStateSmallerThan(AIWorldState.PickaxeIsInWorld, 1)
                .HasStateSmallerThan(AIWorldState.HasPickaxe, 1)
                .Select("Get materials and make pickaxe")
                    .SubDomain_GetMaterialsForTool()
                    .SubDomain_GoToAnvilAndMakePickaxe()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }
    }
}