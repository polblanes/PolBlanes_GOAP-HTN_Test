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
    public class GatherItemFromSourceOperator<TItem, TTool> : IOperator
        where TItem : ItemBase, IGatherable
        where TTool : ItemBase, ICreatable
    {
        public const float TimeWithTool = 1.0f; 
        public const float TimeWithoutTool = 3.0f;
        
        private Vector3 GetRandomPosition(AIAgent agent)
        {
            var pos = Random.insideUnitCircle.normalized * Random.Range(1f, 2f);

            return agent.transform.position + new Vector3(pos.x, 0f, pos.y);
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
            
            var item = context.Agent.itemFactory.Instantiate<TItem>();
            item.transform.position = this.GetRandomPosition(context.Agent);

            return TaskStatus.Success;            
        }

        public TaskStatus BeginInteraction(AIContext context)
        {
            var transformTarget = context.CurrentTarget as TransformTarget;
            
            if (transformTarget == null)
                return TaskStatus.Failure;
            
            if (context.Agent.Inventory.Has<TTool>())
            {
                context.GenericTimer = context.Time + TimeWithTool;
            }
            else
            {
                context.GenericTimer = context.Time + TimeWithoutTool;
            }
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