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
    public class AIDomainBuilder : BaseDomainBuilder<AIDomainBuilder, AIAgentContext>
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
                task.SetOperator(new FindClosestItemOperator<Apple>());

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
                task.SetOperator(new FindClosestItemOperator<Axe>());

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
                task.SetOperator(new FindClosestItemOperator<Pickaxe>());

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
                task.SetOperator(new FindClosestItemOperator<Wood>());

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
                task.SetOperator(new FindClosestItemOperator<Iron>());

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

        public AIDomainBuilder MoveToTargetWood()
        {
            Action("Move to target wood");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new MoveToOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsWood));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, true, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder MoveToTargetIron()
        {
            Action("Move to target iron");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new MoveToOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsIron));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, true, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder MoveToTargetAxe()
        {
            Action("Move to target axe");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new MoveToOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsAxe));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, true, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder MoveToTargetPickaxe()
        {
            Action("Move to target pickaxe");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new MoveToOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsPickaxe));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, true, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder MoveToTargetAnvil()
        {
            Action("Move to target anvil");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new MoveToOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsAnvil));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, true, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder MoveToTargetApple()
        {
            Action("Move to target apple");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new MoveToOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsApple));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, true, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder MoveToTargetTree()
        {
            Action("Move to target tree");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new MoveToOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsTree));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, true, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder MoveToTargetMine()
        {
            Action("Move to target mine");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new MoveToOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsMine));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, true, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        public AIDomainBuilder MoveToTargetPosition()
        {
            Action("Move to target");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new MoveToOperator());
                task.AddCondition(new HasWorldStateCondition(AIWorldState.TargetIsPosition));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, true, EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPosition, false, EffectType.PlanAndExecute));
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
                task.AddCondition(new HasWorldStateCondition(AIWorldState.HasTargetClose));
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.HasWood, 1, EffectType.PlanOnly));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsWood, false, EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
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
                task.AddCondition(new HasWorldStateCondition(AIWorldState.HasTargetClose));
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.HasIron, 1, EffectType.PlanOnly));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsIron, false, EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
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
                task.AddCondition(new HasWorldStateCondition(AIWorldState.HasTargetClose));
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.HasApple, 1, EffectType.PlanOnly));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsApple, false, EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
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
                task.AddCondition(new HasWorldStateCondition(AIWorldState.HasTargetClose));
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.HasAxe, 1, EffectType.PlanOnly));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAxe, false, EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
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
                task.AddCondition(new HasWorldStateCondition(AIWorldState.HasTargetClose));
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.HasPickaxe, 1, EffectType.PlanOnly));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsPickaxe, false, EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
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
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsTree, false, EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
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
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsMine, false, EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
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
                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.HasWood, 1));
                task.SetOperator(new CreateItemOperator<Axe>());
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.AxeIsInWorld, 1, EffectType.PlanOnly));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAnvil, false, EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
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
                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.HasIron, 1));
                task.AddCondition(new HasWorldStateGreaterThanCondition(AIWorldState.HasWood, 0));
                task.SetOperator(new CreateItemOperator<Pickaxe>());
                task.AddEffect(new IncrementWorldStateEffect(AIWorldState.PickaxeIsInWorld, 1, EffectType.PlanOnly));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.TargetIsAnvil, false, EffectType.PlanAndExecute));
                task.AddEffect(new SetWorldStateEffect(AIWorldState.HasTargetClose, false, EffectType.PlanAndExecute));
            }
            End();
            return this;
        }

        ////////////////////////////////////////////////
        // Sub domains
        ////////////////////////////////////////////////
        
        public AIDomainBuilder ComplexTask_PickUpWoodFromWorld()
        {
            var ComplexTask = new AIDomainBuilder("Pick up wood from world")
                .Sequence("Pick up wood from world")
                    .FindExistingWoodTarget()
                    .MoveToTargetWood()
                    .PickUpWoodItem()
                .End()
                .Build();

            return this.Splice(ComplexTask);
        }

        public AIDomainBuilder ComplexTask_PickUpIronFromWorld()
        {
            var ComplexTask = new AIDomainBuilder("Pick up iron from world")
                .Sequence("Pick up iron from world")
                    .FindExistingIronTarget()
                    .MoveToTargetIron()
                    .PickUpIronItem()
                .End()
                .Build();

            return this.Splice(ComplexTask);
        }

        public AIDomainBuilder ComplexTask_PickUpAxeFromWorld()
        {
            var ComplexTask = new AIDomainBuilder("Pick up axe from world")
                .HasStateSmallerThan(AIWorldState.HasAxe, 1)
                .Sequence("Pick up axe from world")
                    .FindExistingAxeTarget()
                    .MoveToTargetAxe()
                    .PickUpAxeItem()
                .End()
                .Build();

            return this.Splice(ComplexTask);
        }

        public AIDomainBuilder ComplexTask_PickUpPickaxeFromWorld()
        {
            var ComplexTask = new AIDomainBuilder("Pick up pickaxe from world")
                .HasStateSmallerThan(AIWorldState.HasPickaxe, 1)
                .Sequence("Pick up pickaxe from world")
                    .FindExistingPickaxeTarget()
                    .MoveToTargetPickaxe()
                    .PickUpPickaxeItem()
                .End()
                .Build();

            return this.Splice(ComplexTask);
        }

        public AIDomainBuilder ComplexTask_GatherWoodBase()
        {
            var ComplexTask = new AIDomainBuilder("Gather wood")
                .HasStateSmallerThan(AIWorldState.HasWood, 2)
                .Sequence("Gather wood")
                    .FindTreeTarget()
                    .MoveToTargetTree()
                    .GatherWoodFromSource()
                .End()
                .Build();

            return this.Splice(ComplexTask);
        }

        public AIDomainBuilder ComplexTask_GatherIronBase()
        {
            var ComplexTask = new AIDomainBuilder("Gather iron")
                .HasStateSmallerThan(AIWorldState.HasIron, 2)
                .Sequence("Gather iron")
                    .FindIronMineTarget()
                    .MoveToTargetMine()
                    .GatherIronFromSource()
                .End()
                .Build();

            return this.Splice(ComplexTask);
        }

        public AIDomainBuilder SubDomain_GatherWoodWithAxe()
        {
            var SubDomain = new AIDomainBuilder("Gather wood with axe")
                .Select("Gather Wood with or without axe")
                    .ComplexTask_PickUpAxeFromWorld()
                    .ComplexTask_GatherWoodBase()
                .End()
                .Build();
            
            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GatherIronWithPickaxe()
        {
            var SubDomain = new AIDomainBuilder("Gather iron with pickaxe")
                .Select("Gather iron with or without pickaxe")
                    .ComplexTask_PickUpPickaxeFromWorld()
                    .ComplexTask_GatherIronBase()
                    
                .End()
                .Build();
            
            return this.Splice(SubDomain);
        }

        public AIDomainBuilder ComplexTask_Wander()
        {
            var ComplexTask = new AIDomainBuilder("Wander")
                .Select("Wander")
                    .Sequence("Move to wander target and wait")
                        .MoveToTargetPosition()
                        .Wait(3.0f)
                    .End()
                    .FindWanderTarget()
                .End()
                .Build();

            return this.Splice(ComplexTask);
        }

        public AIDomainBuilder ComplexTask_GetApple()
        {
            var ComplexTask = new AIDomainBuilder("Get apple")
                .HasStateSmallerThan(AIWorldState.HasApple, 1)
                .Sequence("Go pick up apple")
                    .FindExistingAppleTarget()
                    .MoveToTargetApple()
                    .PickUpAppleItem()
                .End()
                .Build();

            return this.Splice(ComplexTask);
        }

        public AIDomainBuilder SubDomain_Eat()
        {
            var SubDomain = new AIDomainBuilder("Eat")
                .HasState(AIWorldState.IsHungry)
                .Sequence("Get apple and eat it")
                    .ComplexTask_GetApple()
                    .EatAppleItem()
                .End()
                .Build();
            
            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GetIron(byte ironRequired)
        {
            var SubDomain = new AIDomainBuilder("Get iron")
                .HasStateSmallerThan(AIWorldState.HasIron, ironRequired)
                .Select("Pick up or gather iron")
                    .ComplexTask_PickUpIronFromWorld()
                    .SubDomain_GatherIronWithPickaxe()
                .End()
                .Build();
            
            return this.Splice(SubDomain);                    
        }

        public AIDomainBuilder SubDomain_GetWood(byte woodRequired)
        {
            var SubDomain = new AIDomainBuilder("Get wood")
                .HasStateSmallerThan(AIWorldState.HasWood, woodRequired)
                .Select("Pick up or gather wood")
                    .ComplexTask_PickUpWoodFromWorld()
                    .SubDomain_GatherWoodWithAxe()
                .End()
                .Build();
            
            return this.Splice(SubDomain);                    
        }

        public AIDomainBuilder SubDomain_GetMaterialsForTool()
        {
            var SubDomain = new AIDomainBuilder("Get materials for tool")
                .Select("Get materials for tool")
                    .SubDomain_GetWood(2)
                    .SubDomain_GetIron(2)
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GetMaterialsForAxe()
        {
            var SubDomain = new AIDomainBuilder("Get materials for axe")
                .Select("Get materials for axe")
                    .SubDomain_GetWood(2)
                    .SubDomain_GetIron(1)
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GetMaterialsForPickaxe()
        {
            var SubDomain = new AIDomainBuilder("Get materials for pickaxe")
                .Select("Get materials for pickaxe")
                    .SubDomain_GetWood(1)
                    .SubDomain_GetIron(2)
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder ComplexTask_GoToAnvilAndMakeAxe()
        {
            var ComplexTask = new AIDomainBuilder("Go to anvil and make axe")
                .Sequence("Go to anvil and make axe")
                    .FindAnvilTarget()
                    .MoveToTargetAnvil()
                    .CreateAxe()
                .End()
                .Build();

            return this.Splice(ComplexTask);
        }

        public AIDomainBuilder ComplexTask_GoToAnvilAndMakePickaxe()
        {
            var ComplexTask = new AIDomainBuilder("Go to anvil and make pickaxe")
                .Sequence("Go to anvil and make pickaxe")
                    .FindAnvilTarget()
                    .MoveToTargetAnvil()
                    .CreatePickaxe()
                .End()
                .Build();

            return this.Splice(ComplexTask);
        }

        public AIDomainBuilder SubDomain_CraftAxe()
        {
            var SubDomain = new AIDomainBuilder("Craft Axe")
                .HasStateSmallerThan(AIWorldState.AxeIsInWorld, 1)
                .Select("Get materials and make axe")
                    .ComplexTask_GoToAnvilAndMakeAxe()
                    .SubDomain_GetMaterialsForTool()
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
                    .ComplexTask_GoToAnvilAndMakePickaxe()
                    .SubDomain_GetMaterialsForTool()
                .End()
                .Build();

            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GetAxe()
        {
            var SubDomain = new AIDomainBuilder("Get Axe")
                .HasStateSmallerThan(AIWorldState.HasAxe, 1)
                .Select("Pick up or make axe")
                    .ComplexTask_PickUpAxeFromWorld()
                    .SubDomain_CraftAxe()
                .End()
                .Build();
            
            return this.Splice(SubDomain);
        }

        public AIDomainBuilder SubDomain_GetPickaxe()
        {
            var SubDomain = new AIDomainBuilder("Get Pickaxe")
                .HasStateSmallerThan(AIWorldState.HasPickaxe, 1)
                .Select("Pick up or make pickaxe")
                    .ComplexTask_PickUpPickaxeFromWorld()
                    .SubDomain_CraftPickaxe()
                .End()
                .Build();
            
            return this.Splice(SubDomain);
        }
    }
}