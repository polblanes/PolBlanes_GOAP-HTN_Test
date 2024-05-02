using System;
using System.Linq;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using Classes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviours.GOAP
{
    public class AgentSpawnBehaviour : MonoBehaviour
    {
        private static readonly Vector2 Bounds = new Vector2(15, 8);
        
        private IGoapRunner goapRunner;
        
        [SerializeField]
        private GameObject agentPrefab;

        public Color cleanerColor;
        public Color smithColor;
        public Color minerColor;
        public Color woodCutterColor;

        private void Awake()
        {
            this.goapRunner = FindObjectOfType<GoapRunnerBehaviour>();
            this.agentPrefab.SetActive(false);
        }

        private void Start()
        {
            this.SpawnAgent(SetIds.Cleaner, GOAPComplexAgentBrain.AgentType.Cleaner, this.cleanerColor);
            this.SpawnAgent(SetIds.Cleaner, GOAPComplexAgentBrain.AgentType.Cleaner, this.cleanerColor);
            this.SpawnAgent(SetIds.Cleaner, GOAPComplexAgentBrain.AgentType.Cleaner, this.cleanerColor);
            
            this.SpawnAgent(SetIds.Smith, GOAPComplexAgentBrain.AgentType.Smith, this.smithColor);
            this.SpawnAgent(SetIds.Miner, GOAPComplexAgentBrain.AgentType.Miner, this.minerColor);
            this.SpawnAgent(SetIds.WoodCutter, GOAPComplexAgentBrain.AgentType.WoodCutter, this.woodCutterColor);
        }

        private void SpawnAgent(string setId, GOAPComplexAgentBrain.AgentType agentType, Color color)
        {
            var agent = Instantiate(this.agentPrefab, this.GetRandomPosition(), Quaternion.identity).GetComponent<AgentBehaviour>();
            
            agent.GoapSet = this.goapRunner.GetGoapSet(setId);
            agent.gameObject.SetActive(true);
            
            agent.gameObject.transform.name = $"{agentType} {agent.GetInstanceID()}";

            var brain = agent.GetComponent<GOAPComplexAgentBrain>();
            brain.agentType = agentType;

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