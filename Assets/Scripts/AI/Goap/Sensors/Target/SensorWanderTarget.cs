using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Classes.References;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using UnityEngine;

namespace AI.GOAP.Sensors.Target
{
    public class WanderTargetSensor : LocalTargetSensorBase
    {
        private static readonly Vector2 Bounds = new Vector2(40, 30);
        Vector2[] vectors;
        int pointer = 0;

        public override void Created()
        {
            vectors = new Vector2[10] {
                Random.insideUnitCircle,
                Random.insideUnitCircle,
                Random.insideUnitCircle,
                Random.insideUnitCircle,
                Random.insideUnitCircle,
                Random.insideUnitCircle,
                Random.insideUnitCircle,
                Random.insideUnitCircle,
                Random.insideUnitCircle,
                Random.insideUnitCircle
            };
        }

        public override void Update()
        {
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var random = this.GetRandomPosition(agent);
            
            return new PositionTarget(random);
        }

        private Vector3 GetRandomPosition(IMonoAgent agent, int recursivityLevel = 0)
        {
            int _recursivityLevel = recursivityLevel + 1;
            var random = vectors[pointer] * 5f;
            AdvancePointer();
            var position = agent.transform.position + new Vector3(random.x, 0f, random.y);
            
            if (position.x > -Bounds.x && position.x < Bounds.x && position.z > -Bounds.y && position.z < Bounds.y)
                return position;

            if (_recursivityLevel >= 5)
                return agent.transform.position;

            return this.GetRandomPosition(agent, _recursivityLevel);
        }

        void AdvancePointer()
        {
            pointer = (pointer + 1)%10;
        }
    }
}