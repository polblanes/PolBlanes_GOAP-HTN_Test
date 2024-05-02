using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrashKonijn.Goap.Behaviours;

namespace Behaviours.GOAP
{
    public class AgentBehaviorBase : MonoBehaviour
    {
        protected AgentBehaviour agent;
        protected virtual void Awake()
        {
            agent = GetComponent<AgentBehaviour>();
        }
    }
}