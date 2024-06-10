using System.Collections;
using System.Collections.Generic;
using FluidHTN;
using UnityEngine;
using HTN;

namespace HTN.Domain
{
    [CreateAssetMenu(fileName = "SurvivorDomain", menuName = "HTN/Domains/Survivor")]
    public class SurvivorDomainDefinition : AIDomainDefinition
    {
        public override Domain<AIAgentContext> Create()
        {
            return new AIDomainBuilder("Survivor")
                .Select("Survivor")
                    .SubDomain_Eat()
                    .SubDomain_GetAxe()
                    .SubDomain_GetPickaxe()
                .End()
                .Build();
        }
    }
}