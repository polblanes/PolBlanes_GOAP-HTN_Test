using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Sensors;
using Behaviours.GOAP;
using UnityEngine;

namespace AI.GOAP.Sensors.World
{
    public class ItemOnFloorSensor : GlobalWorldSensorBase
    {
        private ItemCollection collection;

        public override void Created()
        {
            this.collection = GameObject.FindObjectOfType<ItemCollection>();
        }

        public override SenseValue Sense()
        {
            return this.collection.Count(false, false);
        }
    }
}