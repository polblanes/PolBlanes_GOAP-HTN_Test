using System.Collections.Generic;
using UnityEngine;

using HTN;

namespace HTN.Sensors
{
    public class SensorySystem
    {
        private ISensor[] _sensors;

        public SensorySystem(AIAgent agent)
        {
            _sensors = agent.transform.GetComponents<ISensor>();
        }

        public void Tick(AIAgentContext context)
        {
            foreach (var sensor in _sensors)
            {
                if (context.Time >= sensor.NextTickTime)
                {
                    sensor.NextTickTime = context.Time + sensor.TickRate;
                    sensor.Tick(context);
                }
            }
        }

        public void DrawGizmos(AIAgentContext context)
        {
            foreach (var sensor in _sensors)
            {
                sensor.DrawGizmos(context);
            }
        }
    }
}