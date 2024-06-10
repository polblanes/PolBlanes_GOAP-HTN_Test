using Behaviours.GOAP;
using FluidHTN;
using FluidHTN.Operators;
using UnityEngine;

namespace HTN.Operators
{
    public class MoveToOperator : IOperator
    {
        public TaskStatus Update(IContext ctx)
        {
            if (ctx is not AIAgentContext context)
                return TaskStatus.Failure;

            if (null == context.CurrentTarget)
                return TaskStatus.Failure;
            
            if (Vector3.Distance(context.Agent.transform.position, context.CurrentTarget.Position) <= 0.5f)
            {
                context.Agent.StopMoving();
                return TaskStatus.Success;
            }

            if (context.Agent.IsMovementEnabled)
                return TaskStatus.Continue;
                
            context.Agent.StartMoving();
            return TaskStatus.Continue;
        }

        public void Stop(IContext ctx)
        {
            if (ctx is not AIAgentContext context)
                return;
            
            context.Agent.StopMoving();
        }

        public void Aborted(IContext ctx)
        {
            Stop(ctx);
        }
    }
}
