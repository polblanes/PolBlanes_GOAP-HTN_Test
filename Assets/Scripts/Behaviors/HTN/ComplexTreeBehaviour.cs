using System;
using System.Collections;
using System.Linq;
using Classes.Items.HTN;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviours.HTN
{
    public class ComplexTreeBehaviour : MonoBehaviour
    {
        private Apple apple;
        private ItemCollection itemCollection;
        private ItemFactory itemFactory;

        int AgentAmount = 0;

        private void Awake()
        {
            this.itemCollection = FindObjectOfType<ItemCollection>();
            this.itemFactory = FindObjectOfType<ItemFactory>();
        }

        private void Start()
        {
            SurvivorSpawnBehavior spawner = FindObjectOfType<SurvivorSpawnBehavior>();
            if (spawner != null)
            {
                AgentAmount = spawner.agentAmount;
            }
            this.DropApple();
        }

        private void Update()
        {
            if (this.apple == null)
                return;
            
            if (!this.apple.IsHeld)
                return;

            var count = this.itemCollection.All().Count(x => x is IEatable);

            if (count > AgentAmount * 2.0f)
                return;
            
            this.apple = null;
            this.StartCoroutine(this.GrowApple());
        }

        private IEnumerator GrowApple()
        {
            yield return new WaitForSeconds(3f);
            this.DropApple();
        }

        private void DropApple()
        {
            this.apple = this.itemFactory.Instantiate<Apple>();
            this.apple.transform.position = this.GetRandomPosition();
        }
        
        private Vector3 GetRandomPosition()
        {
            var pos = Random.insideUnitCircle.normalized * Random.Range(1f, 2f);

            return this.transform.position + new Vector3(pos.x, 0f, pos.y);
        }
    }
}