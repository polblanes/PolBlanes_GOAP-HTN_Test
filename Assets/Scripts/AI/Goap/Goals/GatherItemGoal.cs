using CrashKonijn.Goap.Behaviours;
using Interfaces;

namespace AI.GOAP.Goals
{
    public class GatherItemGoal<TGatherable> : GoalBase
        where TGatherable : IGatherable
    {
    }
}