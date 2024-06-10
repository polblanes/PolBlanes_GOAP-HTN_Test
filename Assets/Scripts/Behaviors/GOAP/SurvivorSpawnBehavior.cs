using System;
using System.Linq;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using Classes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviours.GOAP
{
    public class SurvivorSpawnBehavior : MonoBehaviour
    {
        private static readonly Vector2 Bounds = new Vector2(15, 8);
        
        private IGoapRunner goapRunner;

        public int agentAmount;
        
        [SerializeField]
        private GameObject agentPrefab;

        public Color agentColor;

        private void Awake()
        {
            this.goapRunner = FindObjectOfType<GoapRunnerBehaviour>();
            this.agentPrefab.SetActive(false);
        }

        private void Start()
        {
            for (int agentsToSpawn = agentAmount; agentsToSpawn > 0; agentsToSpawn--)
            {
                this.SpawnAgent(SetIds.Survivor, this.agentColor);
            }
        }

        private void SpawnAgent(string setId, Color color)
        {
            var agent = Instantiate(this.agentPrefab, this.GetRandomPosition(), Quaternion.identity).GetComponent<AgentBehaviour>();
            
            agent.GoapSet = this.goapRunner.GetGoapSet(setId);
            agent.gameObject.SetActive(true);
            
            agent.gameObject.transform.name = $"{GOAPComplexAgentBrain.AgentType.Survivor} {agent.GetInstanceID()}";

            var brain = agent.GetComponent<GOAPComplexAgentBrain>();
            brain.agentType = GOAPComplexAgentBrain.AgentType.Survivor;

            var spriteRenderer = agent.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = color;
        }
        
        private Vector3 GetRandomPosition()
        {
            var randomX = Random.Range(-Bounds.x, Bounds.x);
            var randomY = Random.Range(-Bounds.y, Bounds.y);
            
            return new Vector3(randomX, 0f, randomY);
        }
    }
}

