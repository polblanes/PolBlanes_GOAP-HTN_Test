using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using CrashKonijn.Goap.Resolver;
using CrashKonijn.Goap.Enums;

namespace AI.GOAP
{
    public class GoapSetConfigFactory : GoapSetFactoryBase
    {
        public override IGoapSetConfig Create()
        {
            var builder = new GoapSetBuilder("GettingStartedSet");
            
            // Goals
            builder.AddGoal<Goals.WanderGoal>()
                .AddCondition<WorldKeys.IsWandering>(Comparison.GreaterThanOrEqual, 1);

            // Actions
            builder.AddAction<Actions.Wander>()
                .SetTarget<Targets.WanderTarget>()
                .AddEffect<WorldKeys.IsWandering>(EffectType.Increase)
                .SetBaseCost(1)
                .SetInRange(0.3f);

            // Target Sensors
            builder.AddTargetSensor<Sensors.Target.WanderTargetSensor>()
                .SetTarget<Targets.WanderTarget>();

            // World Sensors
            // This example doesn't have any world sensors. Look in the examples for more information on how to use them.

            return builder.Build();
        }
    }
}