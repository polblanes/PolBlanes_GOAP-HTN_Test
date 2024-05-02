using UnityEngine;

namespace Interfaces.HTN
{
    public interface ITarget
    {
        public Vector3 Position { get; }
        public AITargetType TargetType { get; }
    }

    public enum AITargetType
    {
        None,
        Source,
        Box,
        Item,
        Object,
        Position
    }
}