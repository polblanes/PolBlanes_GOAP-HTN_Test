using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Classes.References;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Behaviours.GOAP;
using UnityEngine;

namespace AI.GOAP.Sensors.Target
{
    public class ClosestAppleSensor : LocalTargetSensorBase
    {
        private AppleCollection apples;

        public override void Created()
        {
            this.apples = GameObject.FindObjectOfType<AppleCollection>();
        }

        public override void Update()
        {
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var closestApple = this.apples.Get().Closest(agent.transform.position);

            if (closestApple is null)
                return null;
            
            return new TransformTarget(closestApple.transform);
        }
    }
}