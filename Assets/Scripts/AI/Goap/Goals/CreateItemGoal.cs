using CrashKonijn.Goap.Behaviours;
using Interfaces;

namespace AI.GOAP.Goals
{
    public class CreateItemGoal<THoldable> : GoalBase
        where THoldable : ICreatable
    {
    }
}