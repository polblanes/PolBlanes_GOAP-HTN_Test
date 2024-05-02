using UnityEngine;
using Interfaces.HTN;

namespace Classes.HTN
{
    public class TransformTarget : ITarget
    {
        public Transform Transform { get; }
        public AITargetType TargetType { get; }

        public Vector3 Position
        {
            get
            {
               if (this.Transform == null)
                   return Vector3.zero;

               return this.Transform.position;
            }
        }

        public TransformTarget(Transform transform, AITargetType targetType)
        {
            this.Transform = transform;
            this.TargetType = targetType;
        }
    }
}