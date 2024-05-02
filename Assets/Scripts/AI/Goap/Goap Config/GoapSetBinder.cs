
using CrashKonijn.Goap.Behaviours;
using UnityEngine;

namespace AI.GOAP
{
    public class GoapSetBinder : MonoBehaviour 
    {
        public void Awake() {
            var runner = FindObjectOfType<GoapRunnerBehaviour>();
            var agent = GetComponent<AgentBehaviour>();
            agent.GoapSet = runner.GetGoapSet("GettingStartedSet");
        }
    }
}