using Behaviours.GOAP;
using CrashKonijn.Goap.Behaviours;
using UnityEngine;

namespace AI
{
    public class AgentBrain : AgentBehaviorBase
    {
        private void Start()
        {
            this.agent.SetGoal<Goals.Wander>(false);
        }
    }
}
