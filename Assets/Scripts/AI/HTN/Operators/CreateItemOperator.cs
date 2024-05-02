using System.Linq;
using FluidHTN;
using FluidHTN.Operators;
using Unity.VisualScripting;
using UnityEngine;
using Interfaces.HTN;
using Classes.HTN;
using UnityEngine.UIElements;
using Interfaces;
using Classes.Items.HTN;
using UnityEditor;
using Behaviours.HTN;

namespace HTN.Operators
{
    public class CreateItemOperator<TCreatable> : IOperator
        where TCreatable : ItemBase, ICreatable
    {
        public const float Time = 5.0f;
        
        private Vector3 GetRandomPosition(AIAgent agent)
        {
            var pos = Random.insideUnitCircle.normalized * Random.Range(1f, 2f);

            return agent.transform.position + new Vector3(pos.x, 0f, pos.y);
        }

        private void RemoveRequiredResources(AIContext context)
        {
            var iron = context.Agent.Inventory.Get<Iron>().FirstOrDefault();
            context.Agent.Inventory.Remove(iron);
            context.Agent.instanceHandler.QueueForDestroy(iron);            
            
            var wood = context.Agent.Inventory.Get<Wood>().FirstOrDefault();
            context.Agent.Inventory.Remove(wood);
            context.Agent.instanceHandler.QueueForDestroy(wood);
        }

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
            
            RemoveRequiredResources(context);
            var item = context.Agent.itemFactory.Instantiate<TCreatable>();
            item.transform.position = this.GetRandomPosition(context.Agent);

            return TaskStatus.Success;            
        }

        public TaskStatus BeginInteraction(AIContext context)
        {
            var transformTarget = context.CurrentTarget as TransformTarget;
            
            if (transformTarget == null)
                return TaskStatus.Failure;
            
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