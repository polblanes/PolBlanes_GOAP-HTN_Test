using FluidHTN;
using FluidHTN.Operators;
using Unity.VisualScripting;
using Interfaces;
using Interfaces.HTN;
using Behaviours.HTN;
using UnityEngine;
using Classes.HTN;

namespace HTN.Operators
{
    public class FindWanderTargetOperator : SetTargetOperator
    {
        private static readonly Vector2 Bounds = new Vector2(15, 8);

        public override ITarget GetTarget(IContext ctx)
        {
            var random = this.GetRandomPosition(ctx);
            
            return new PositionTarget(random);
        }

        private Vector3 GetRandomPosition(IContext ctx)
        {
            if (ctx is not AIContext context)
                return Vector3.zero;

            var random =  Random.insideUnitCircle * 5f;
            var position = context.Agent.transform.position + new Vector3(random.x, 0f, random.y);
            
            if (position.x > -Bounds.x && position.x < Bounds.x && position.z > -Bounds.y && position.z < Bounds.y)
                return position;

            return this.GetRandomPosition(ctx);
        }
    }
}