using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Behaviours.HTN
{
    public abstract class ItemBase : MonoBehaviour, IHoldable
    {
        private ItemCollection collection;
        private InstanceHandler instanceHandler;
        
        [field: SerializeField]
        public bool IsHeld { get; private set; }
        
        [field: SerializeField]
        public bool IsInBox { get; private set; }
        
        [field: SerializeField]
        public bool IsClaimed { get; private set; }

        [field: SerializeField]
        public bool IsDestroyed { get; private set; }

        public string DebugName { get; set; }

        public void Awake()
        {
            this.collection = FindObjectOfType<ItemCollection>();
            this.instanceHandler = FindObjectOfType<InstanceHandler>();
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
            this.IsClaimed = true;
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
            
            foreach (var renderer in this.GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.enabled = true;
            }
        }
    }
}