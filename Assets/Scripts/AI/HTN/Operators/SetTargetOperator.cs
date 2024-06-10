using FluidHTN;
using FluidHTN.Operators;
using Unity.VisualScripting;
using UnityEngine;
using Interfaces.HTN;
using Classes.HTN;
using UnityEngine.UIElements;
using Behaviours.HTN;
using Interfaces;

namespace HTN.Operators
{
    public class SetTargetOperator : IOperator
    {
        public virtual TaskStatus Update(IContext ctx)
        {
            if (ctx is not AIAgentContext context)
                return TaskStatus.Failure;

            ITarget target = GetTarget(ctx);

            if (null == target)
                return TaskStatus.Failure;

            if (context.CurrentTarget != null)
            {
                ReleaseCurrentTarget(context);
            }

            context.CurrentTarget = target;
            return TaskStatus.Success;
        }

        private void ReleaseCurrentTarget(AIAgentContext context)
        {
            Transform targetTransform = context.CurrentTarget as Transform;

            if (targetTransform == null)
                return;
            
            IHoldable itemTarget = targetTransform.GetComponent(typeof(IHoldable)) as IHoldable;
            if (itemTarget == null)
                return;
            
            if (itemTarget.IsHeld)
                return;
                
            itemTarget.Drop();
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