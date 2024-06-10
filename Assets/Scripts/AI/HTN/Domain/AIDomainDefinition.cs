using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluidHTN;

namespace HTN.Domain
{
    public abstract class AIDomainDefinition : ScriptableObject
    {
        public abstract Domain<AIAgentContext> Create();
    }
}