using UnityEngine;
using Interfaces.HTN;

namespace Classes.HTN
{
    public class PositionTarget : ITarget
    {
        public Vector3 Position { get; }
        public AITargetType TargetType { get; }

        public PositionTarget(Vector3 position)
        {
            this.Position = position;
            this.TargetType = AITargetType.Position;
        }
    }
}