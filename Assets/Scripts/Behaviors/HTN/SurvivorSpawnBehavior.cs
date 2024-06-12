using System;
using System.Linq;
using Classes;
using UnityEngine;
using Random = UnityEngine.Random;
using HTN;

namespace Behaviours.HTN
{
    public class SurvivorSpawnBehavior : MonoBehaviour
    {
        private static readonly Vector2 Bounds = new Vector2(15, 8);
        
        [SerializeField]
        private GameObject agentPrefab;

        public int agentAmount;

        public Color agentColor;

        private void Awake()
        {
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
            var agent = Instantiate(this.agentPrefab, this.GetRandomPosition(), Quaternion.identity).GetComponent<AIAgent>();
            
            agent.gameObject.SetActive(true);
            
            agent.gameObject.transform.name = $"Survivor {agent.GetInstanceID()}";

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

