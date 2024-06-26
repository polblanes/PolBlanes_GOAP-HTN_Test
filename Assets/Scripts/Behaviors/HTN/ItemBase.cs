﻿using Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace Behaviours.HTN
{
    public abstract class ItemBase : MonoBehaviour, IHoldable
    {
        private ItemCollection collection;
        private InstanceHandler instanceHandler;
        private GlobalItemManager itemManager;
        
        [field: SerializeField]
        public bool IsHeld { get; private set; }
        
        [field: SerializeField]
        public bool IsInBox { get; private set; }
        
        [field: SerializeField]
        public bool IsClaimed { get; private set; }

        [field: SerializeField]
        public bool IsDestroyed { get; private set; }

        public string DebugName { get; set; }

        public Action<IHoldable> OnItemClaimed;
        public Action<IHoldable> OnItemPickedUp;
        public Action<IHoldable> OnItemDropped;

        public void Awake()
        {
            this.collection = FindObjectOfType<ItemCollection>();
            this.instanceHandler = FindObjectOfType<InstanceHandler>();
            this.itemManager = FindObjectOfType<GlobalItemManager>();
        }

        public void Update()
        {
            if (!ShouldDestroy())
                return;

            this.instanceHandler.QueueForDestroy(this);
        }

        public void OnEnable()
        {
            this.collection.Add(this);
        }

        public void OnDisable()
        {
            this.collection.Remove(this);
        }

        public void Claim()
        {
            if (IsClaimed)
                return;
                
            this.IsClaimed = true;
            itemManager.OnItemClaimed?.Invoke(this);
        }

        public void MarkForDestroy()
        {
            this.IsDestroyed = true;
        }

        public virtual bool ShouldDestroy()
        {
            return false;
        }

        public void Pickup(bool visible = false)
        {
            this.IsHeld = true;
            this.IsInBox = false;
            this.IsClaimed = true;

            itemManager.OnItemPickedUp?.Invoke(this);

            if (this == null || this.gameObject == null)
                return;
            
            foreach (var renderer in this.GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.enabled = visible;
            }
        }

        public void Drop(bool inBox = false)
        {
            this.IsHeld = false;
            this.IsInBox = inBox;
            this.IsClaimed = false;

            itemManager.OnItemDropped?.Invoke(this);
            
            foreach (var renderer in this.GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.enabled = true;
            }
        }
    }
}