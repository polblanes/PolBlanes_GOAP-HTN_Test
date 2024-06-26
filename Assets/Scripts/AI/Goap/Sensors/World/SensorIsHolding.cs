﻿using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Classes.References;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Behaviours.GOAP;
using Interfaces;

namespace AI.GOAP.Sensors.World
{
    public class IsHoldingSensor<T> : LocalWorldSensorBase
        where T : IHoldable
    {
        public override void Created()
        {
        }

        public override void Update()
        {
        }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            var inventory = references.GetCachedComponent<ComplexInventoryBehaviour>();
            
            if (inventory == null)
                return false;

            return inventory.Count<T>();
        }
    }
}