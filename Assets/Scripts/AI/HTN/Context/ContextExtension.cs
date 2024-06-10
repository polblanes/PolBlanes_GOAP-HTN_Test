using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AI;

using HTN.Sensors;
using Interfaces.HTN;
using FluidHTN;

namespace HTN
{
    public partial class AIAgentContext
    {
        public AISenses Senses { get; }
        public Transform Head { get; }

        public ITarget CurrentTarget { get; set; }

        public Vector3 Position => Agent.transform.position;
        public Vector3 Forward => Agent.transform.forward;

        public float Time { get; set; }
        public float DeltaTime { get; set; }
        public float GenericTimer { get; set; }

        /// <summary>
        /// We can use this to prevent sensory updates from causing an unwanted replan.
        /// This is used in the attack operator to ensure we always complete an attack
        /// once its started, so that its effect is played, ensuring our attacker gets
        /// tired.
        /// This still feels a bit hacky. I'd prefer something more deliberate, since
        /// so far in the example its fairly unique to attacks. So I'm not sure we
        /// need this kind of wide-spread blocking of sensory.
        /// </summary>
        public bool CanSense { get; set; }

        public AIAgentContext(AIAgent agent, AISenses senses, Transform head, Animator animator, NavMeshAgent navAgent)
        {
            Agent = agent;
            Senses = senses;
            Head = head;
            CanSense = true;

            base.Init();
        }
    }
}