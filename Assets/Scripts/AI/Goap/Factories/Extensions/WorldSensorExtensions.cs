using CrashKonijn.Goap.Classes.Builders;
using Interfaces;
using AI.GOAP.Sensors.World;
using AI.GOAP.WorldKeys;

namespace AI.GOAP.Factories.Extensions
{
    public static class WorldSensorExtensions
    {
        public static void AddIsHoldingSensor<THoldable>(this GoapSetBuilder builder)
            where THoldable : IHoldable
        {
            builder.AddWorldSensor<IsHoldingSensor<THoldable>>()
                .SetKey<IsHolding<THoldable>>();
        }
        
        public static void AddIsInWorldSensor<THoldable>(this GoapSetBuilder builder)
            where THoldable : IHoldable
        {
            builder.AddWorldSensor<IsInWorldSensor<THoldable>>()
                .SetKey<IsInWorld<THoldable>>();
        }
        
        public static void AddItemOnFloorSensor(this GoapSetBuilder builder)
        {
            builder.AddWorldSensor<ItemOnFloorSensor>()
                .SetKey<ItemsOnFloor>();
        }
    }
}