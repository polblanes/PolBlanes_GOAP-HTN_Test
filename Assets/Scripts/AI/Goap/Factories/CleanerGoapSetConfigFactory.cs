using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using Classes;
using AI.GOAP.Factories.Extensions;
using Interfaces;

namespace AI.GOAP.Factories
{
    public class CleanerGoapSetConfigFactory : GoapSetFactoryBase
    {
        public override IGoapSetConfig Create()
        {
            var builder = new GoapSetBuilder(SetIds.Cleaner);
            
            // Debugger
            builder.SetAgentDebugger<AgentDebugger>();

            // Goals
            builder.AddWanderGoal();
            
            builder.AddCleanItemsGoal();
            builder.AddFixHungerGoal();

            // Actions
            builder.AddWanderAction();

            builder.AddHaulItemAction();
            builder.AddPickupItemAction<IEatable>();
            builder.AddEatAction();
            
            // TargetSensors
            builder.AddWanderTargetSensor();
            builder.AddTransformTargetSensor();
            builder.AddClosestItemTargetSensor<IEatable>();
            
            // WorldSensors
            builder.AddIsHoldingSensor<IEatable>();
            
            builder.AddIsInWorldSensor<IEatable>();
            
            builder.AddItemOnFloorSensor();

            return builder.Build();
        }
    }
}