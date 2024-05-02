using CrashKonijn.Goap.Interfaces;
using Behaviours.GOAP;

namespace AI.GOAP
{
    public class AgentDebugger : IAgentDebugger
    {
        public string GetInfo(IMonoAgent agent, IComponentReference references)
        {
            var hunger = references.GetCachedComponent<HungerBehaviour>();
            
            return $"Hunger: {hunger.hunger}";
        }
    }
}