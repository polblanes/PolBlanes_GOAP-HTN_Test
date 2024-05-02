using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Sensors;
using Behaviours.GOAP;
using Interfaces;
using UnityEngine;

namespace AI.GOAP.Sensors.World
{
    public class IsInWorldSensor<T> : GlobalWorldSensorBase where T : IHoldable
    {
        private ItemCollection collection;

        public override void Created()
        {
            this.collection = GameObject.FindObjectOfType<ItemCollection>();
        }

        public override SenseValue Sense()
        {
            return this.collection.GetFiltered<T>(false, true, false).Length;
        }
    }
}