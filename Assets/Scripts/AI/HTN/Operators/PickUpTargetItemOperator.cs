using FluidHTN;
using FluidHTN.Operators;
using Unity.VisualScripting;
using UnityEngine;
using Interfaces.HTN;
using Classes.HTN;
using UnityEngine.UIElements;
using Interfaces;
using Behaviours.HTN;

namespace HTN.Operators
{
    public class PickUpTargetItemOperator : IOperator
    {
        ItemBase Item = null;

        public const float Time = 0.5f; 

        public TaskStatus Update(IContext ctx)
        {
            if (ctx is not AIContext context)
                return TaskStatus.Failure;
            
            if (context.Time < context.GenericTimer)
                return TaskStatus.Continue;

            if (context.GenericTimer <= 0f)
            {
                return BeginInteraction(context);
            }

            context.GenericTimer = -1f;
            context.Agent.Inventory.Add(Item);
            return TaskStatus.Success;            
        }

        public TaskStatus BeginInteraction(AIContext context)
        {
            var transformTarget = context.CurrentTarget as TransformTarget;
            
            if (transformTarget == null)
                return TaskStatus.Failure;
            
            Item = transformTarget.Transform.GetComponent<ItemBase>();

            if (Item == null || Item.IsClaimed)
                return TaskStatus.Failure;

            Item.Claim();
            context.GenericTimer = context.Time + Time;
            return TaskStatus.Continue;
        }

        public void Stop(IContext ctx)
        {
            if (ctx is not AIContext context)
                return;
            
            context.GenericTimer = -1f;
        }

        public void Aborted(IContext ctx)
        {
            Stop(ctx);
        }

        public virtual ITarget GetTarget(IContext ctx)
        {
            return new PositionTarget(Vector3.zero);
        }
    }
}