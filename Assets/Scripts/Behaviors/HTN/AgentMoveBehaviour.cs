using System;
using Interfaces.HTN;
using UnityEngine;

namespace Behaviours.HTN
{
    public class AgentMoveBehaviour : AgentBehaviorBase
    {
        private ITarget CurrentTarget { get 
        { 
            if (agent == null)
                return null;
            
            return agent.CurrentTarget; 
        }}
        
        public bool bShouldMove;

        public void Update()
        {
            if (!this.bShouldMove)
                return;
            
            if (this.CurrentTarget == null)
                return;
            
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.CurrentTarget.Position.x, this.transform.position.y, this.CurrentTarget.Position.z), Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            if (this.CurrentTarget == null)
                return;
            
            Gizmos.DrawLine(this.transform.position, this.CurrentTarget.Position);
        }
    }
}