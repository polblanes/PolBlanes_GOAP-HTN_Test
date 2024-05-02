using FluidHTN;
using FluidHTN.Operators;
using Unity.VisualScripting;
using UnityEngine;
using Interfaces.HTN;
using Classes.HTN;
using UnityEngine.UIElements;

namespace HTN.Operators
{
    public class SetTargetTransformOperator : SetTargetOperator
    {
        protected virtual Transform TargetTransform { get; }

        protected virtual AITargetType TargetType { get; }

        public override ITarget GetTarget(IContext ctx)
        {
            return new TransformTarget(TargetTransform, TargetType);
        }
    }
}