using System;
using UnityEngine;

using HTN;

namespace HTN.Sensors
{
    [Serializable]
    public class AISenses
    {
        public float EyeSight = 15f;
        public float SqrEyeSight => EyeSight * EyeSight;

        public float MeleeRange = 2f;
        public float SqrMeleeRange => MeleeRange * MeleeRange;

        public void DrawGizmos(AIAgentContext context)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.125f);
            Gizmos.DrawSphere(context.Position, EyeSight);
        }
    }
}