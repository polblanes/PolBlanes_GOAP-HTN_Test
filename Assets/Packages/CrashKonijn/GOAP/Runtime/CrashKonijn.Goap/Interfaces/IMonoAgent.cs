using System.Collections.Generic;
using System.Numerics;

namespace CrashKonijn.Goap.Interfaces
{
    public interface IMonoAgent : IAgent, IMonoBehaviour
    {
        public void OnGoapRunnerNewPlan(List<IActionBase> plan);

        public void OnGoapRunnerReplan();

        public void OnGoapRunnerGoalComplete();

        public void OnGoapRunnerPlanningStarted();
    }
}