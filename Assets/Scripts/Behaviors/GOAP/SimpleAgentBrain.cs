﻿using CrashKonijn.Goap.Behaviours;
using AI.GOAP.Goals;
using UnityEngine;

namespace Behaviours.GOAP
{
    public class SimpleAgentBrain : MonoBehaviour
    {
        private AgentBehaviour agent;
        private HungerBehaviour hunger;

        private void Awake()
        {
            this.agent = this.GetComponent<AgentBehaviour>();
            this.hunger = this.GetComponent<HungerBehaviour>();
        }

        private void Start()
        {
            this.agent.SetGoal<WanderGoal>(false);
        }

        private void Update()
        {
            if (this.hunger.hunger > 80)
            {
                this.agent.SetGoal<FixHungerGoal>(false);
                return;
            }
            
            if (this.hunger.hunger < 20)
                this.agent.SetGoal<WanderGoal>(true);
        }
    }
}