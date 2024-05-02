using CrashKonijn.Goap.Interfaces;
using Behaviours.GOAP;
using Interfaces;
using UnityEngine;

namespace AI.GOAP
{
    public class GoapInjector : MonoBehaviour, IGoapInjector
    {
        public ItemFactory itemFactory;
        public ItemCollection itemCollection;
        public InstanceHandler instanceHandler;
        
        public void Inject(IActionBase action)
        {
            if (action is IInjectable injectable)
                injectable.Inject(this);
        }

        public void Inject(IGoalBase goal)
        {
        }

        public void Inject(IWorldSensor worldSensor)
        {
        }

        public void Inject(ITargetSensor targetSensor)
        {
        }
    }
}