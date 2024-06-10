using FluidHTN;
using FluidHTN.Operators;
using Unity.VisualScripting;
using UnityEngine;
using Interfaces.HTN;
using Classes.HTN;
using Interfaces;

namespace HTN.Operators
{
    public class FindClosestOperator<T> : SetTargetTransformOperator
        where T : MonoBehaviour
    {
        protected T[] collection;

        public virtual void GetCollection() 
        {
            this.collection = GameObject.FindObjectsOfType<T>();
        }

        public override ITarget GetTarget(IContext ctx)
        {
            return GetClosest(ctx);
        }

        public ITarget GetClosest(IContext ctx)
        {
            if (ctx is not AIAgentContext context)
                return null;

            GetCollection();

            var closest = this.collection.Closest(context.Agent.transform.position);
            
            if (closest == null)
                return null;

            AITargetType targetType = AITargetType.Object;
            IHoldable Item = closest as IHoldable;
            if (Item != null)
            {
                targetType = AITargetType.Item;
                Item.Claim();
            }

            
            return new TransformTarget(closest.transform, targetType);
        }
    }
}