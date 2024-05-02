using FluidHTN;
using FluidHTN.Operators;
using Unity.VisualScripting;
using UnityEngine;
using Interfaces.HTN;
using Classes.HTN;
using UnityEngine.UIElements;

namespace HTN.Operators
{
    public class SetTargetOperator : IOperator
    {
        public virtual TaskStatus Update(IContext ctx)
        {
            if (ctx is not AIContext context)
                return TaskStatus.Failure;

            ITarget target = GetTarget(ctx);

            if (null == target)
                return TaskStatus.Failure;

            context.CurrentTarget = target;
            return TaskStatus.Success;
        }

        public virtual void Stop(IContext ctx)
        {

        }

        public virtual void Aborted(IContext ctx)
        {

        }

        public virtual ITarget GetTarget(IContext ctx)
        {
            return new PositionTarget(Vector3.zero);
        }
    }
}