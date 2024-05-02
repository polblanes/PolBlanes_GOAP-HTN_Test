using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTN;

namespace Behaviours.HTN
{
    public class AgentBehaviorBase : MonoBehaviour
    {
        protected AIAgent agent;
        protected virtual void Awake()
        {
            agent = GetComponent<AIAgent>();
        }
    }
}