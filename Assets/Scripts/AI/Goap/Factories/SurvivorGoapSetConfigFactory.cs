using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using Classes;
using Classes.Items.GOAP;
using Classes.Sources.GOAP;
using AI.GOAP.Factories.Extensions;
using Interfaces;

namespace AI.GOAP.Factories
{
    public class SurvivorGoapSetConfigFactory : GoapSetFactoryBase
    {
        public override IGoapSetConfig Create()
        {
            var builder = new GoapSetBuilder(SetIds.Survivor);
            
            #region Debugger
            builder.SetAgentDebugger<AgentDebugger>();
            #endregion

            #region Goals
            builder.AddWanderGoal();
            
            builder.AddCreateItemGoal<Axe>();
            builder.AddCreateItemGoal<Pickaxe>();
            builder.AddFixHungerGoal();

            builder.AddPickupItemGoal<Axe>();
            builder.AddGatherItemGoal<Wood>();
            builder.AddPickupItemGoal<Pickaxe>();
            builder.AddGatherItemGoal<Iron>();            
            #endregion

            #region Actions
            builder.AddWanderAction();

            builder.AddPickupItemAction<Iron>();
            builder.AddPickupItemAction<Wood>();
            builder.AddPickupItemAction<Pickaxe>();
            builder.AddPickupItemAction<Axe>();
            builder.AddPickupItemAction<IEatable>();
            
            builder.AddCreateItemAction<Pickaxe>();
            builder.AddCreateItemAction<Axe>();

            builder.AddGatherItemAction<Iron, Pickaxe>();
            builder.AddGatherItemSlowAction<Iron>();

            builder.AddGatherItemAction<Wood, Axe>();
            builder.AddGatherItemSlowAction<Wood>();

            builder.AddEatAction();
            #endregion
            
            #region TargetSensors
            builder.AddWanderTargetSensor();
            builder.AddTransformTargetSensor();
            
            builder.AddClosestItemTargetSensor<IEatable>();
            builder.AddClosestItemTargetSensor<Iron>();
            builder.AddClosestItemTargetSensor<Pickaxe>();
            builder.AddClosestItemTargetSensor<Wood>();
            builder.AddClosestItemTargetSensor<Axe>();
            
            builder.AddClosestSourceTargetSensor<Iron>();
            builder.AddClosestSourceTargetSensor<Wood>();
            
            builder.AddClosestObjectTargetSensor<AnvilSource>();
            #endregion
            
            #region WorldSensors
            builder.AddItemOnFloorSensor();

            builder.AddIsHoldingSensor<IEatable>();            
            builder.AddIsHoldingSensor<Pickaxe>();
            builder.AddIsHoldingSensor<Iron>();
            builder.AddIsHoldingSensor<Axe>();
            builder.AddIsHoldingSensor<Wood>();
            
            builder.AddIsInWorldSensor<IEatable>();
            builder.AddIsInWorldSensor<Pickaxe>();
            builder.AddIsInWorldSensor<Iron>();            
            builder.AddIsInWorldSensor<Axe>();
            builder.AddIsInWorldSensor<Wood>();
            #endregion            

            return builder.Build();
        }
    }
}