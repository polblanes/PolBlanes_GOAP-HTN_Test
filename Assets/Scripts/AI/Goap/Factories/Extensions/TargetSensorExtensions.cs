using CrashKonijn.Goap.Classes.Builders;
using Classes;
using Interfaces;
using AI.GOAP.Sensors.Target;
using AI.GOAP.Targets;
using UnityEngine;

namespace AI.GOAP.Factories.Extensions
{
    public static class TargetSensorExtensions
    {
        public static void AddWanderTargetSensor(this GoapSetBuilder builder)
        {
            builder.AddTargetSensor<WanderTargetSensor>()
                .SetTarget<WanderTarget>();
        }
        
        public static void AddTransformTargetSensor(this GoapSetBuilder builder)
        {
            builder.AddTargetSensor<TransformSensor>()
                .SetTarget<TransformTarget>();
        }
        
        public static void AddClosestItemTargetSensor<T>(this GoapSetBuilder builder)
            where T : class, IHoldable
        {
            builder.AddTargetSensor<ClosestItemSensor<T>>()
                .SetTarget<ClosestTarget<T>>();
        }
        
        public static void AddClosestObjectTargetSensor<T>(this GoapSetBuilder builder)
            where T : MonoBehaviour
        {
            builder.AddTargetSensor<ClosestObjectSensor<T>>()
                .SetTarget<ClosestTarget<T>>();
        }

        public static void AddClosestSourceTargetSensor<T>(this GoapSetBuilder builder)
            where T : IGatherable
        {
            builder.AddTargetSensor<ClosestSourceSensor<T>>()
                .SetTarget<ClosestSourceTarget<T>>();
        }
    }
}