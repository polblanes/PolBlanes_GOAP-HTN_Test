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
        public override Domain<AIContext> Create()
        {
            return new AIDomainBuilder("Survivor")
                .Select("Survivor")
                    .SubDomain_Eat()
                    .SubDomain_CraftAxe()
                    .SubDomain_CraftPickaxe()
                    .SubDomain_GatherIronWithPickaxe()
                    .SubDomain_GatherWoodWithAxe()
                    .SubDomain_Wander()
                .End()
                .Build();
        }
    }
}