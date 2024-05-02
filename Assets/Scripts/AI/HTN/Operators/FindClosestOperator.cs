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
        bool bInitialized = false;

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
            if (ctx is not AIContext context)
                return null;

            if (!bInitialized)
            {
                GetCollection();
            }

            var closest = this.collection.Closest(context.Agent.transform.position);
            
            if (closest == null)
                return null;

            AITargetType targetType = AITargetType.Object;
            
            return new TransformTarget(closest.transform, targetType);
        }
    }
}