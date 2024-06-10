using System.Linq;
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
    public class EatEatableOperator<TEatable> : IOperator
        where TEatable : ItemBase, IEatable
    {
        ItemBase EatableItem;
        bool bStarted = false;
        bool bIsRemoved = false;

        private void Reset()
        {
            EatableItem = null;
            bStarted = false;
            bIsRemoved = false;
        }

        public TaskStatus Update(IContext ctx)
        {
            if (ctx is not AIAgentContext context)
                return TaskStatus.Failure;

            if (!bStarted)
            {
                return BeginInteraction(context);
            }

            var eatNutrition = Mathf.Min(context.DeltaTime * 20f, (EatableItem as IEatable).NutritionValue);
            (EatableItem as IEatable).NutritionValue -= eatNutrition;
            context.Agent.Hunger.hunger -= eatNutrition;

            if (context.Agent.Hunger.hunger <= 20f)
                return TaskStatus.Success;

            if (EatableItem == null)
                return TaskStatus.Failure;                

            if ((EatableItem as IEatable).NutritionValue > 0)
                return TaskStatus.Continue;

            return TaskStatus.Failure;            
        }

        public TaskStatus BeginInteraction(AIAgentContext context)
        {
            var transformTarget = context.CurrentTarget as TransformTarget;
            
            if (transformTarget == null)
                return TaskStatus.Failure;

            EatableItem = context.Agent.Inventory.Get<TEatable>().FirstOrDefault();

            if (EatableItem == null)
                return TaskStatus.Failure;
                
            bStarted = true;
            context.Agent.Inventory.Hold(EatableItem);
            return TaskStatus.Continue;
        }

        public void Stop(IContext ctx)
        {
            if (ctx is not AIAgentContext context)
            {
                Reset();
                return;
            }

            if (!bStarted)
            {
                BeginInteraction(context);
            }
            
            if (EatableItem == null)
            {
                Reset();
                return;
            }
            
            if ((EatableItem as IEatable).NutritionValue > 0)
            {
                context.Agent.Inventory.Add(EatableItem);
            }
            else
            {
                if (!bIsRemoved)
                {
                    context.Agent.Inventory.Remove(EatableItem);
                    bIsRemoved = true;
                }
                if (!EatableItem.IsDestroyed)
                {
                    context.Agent.instanceHandler.QueueForDestroy(EatableItem);
                }
            }

            Reset();
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