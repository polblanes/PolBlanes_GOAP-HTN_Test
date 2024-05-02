using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Sensors;
using Behaviours.GOAP;
using UnityEngine;

namespace AI.GOAP.Sensors.World
{
    public class ThereAreApplesSensor : GlobalWorldSensorBase
    {
        private AppleCollection apples;
        
        public override void Created()
        {
            this.apples = GameObject.FindObjectOfType<AppleCollection>();
        }

        public override SenseValue Sense()
        {
            return this.apples.Any();
        }
    }
}