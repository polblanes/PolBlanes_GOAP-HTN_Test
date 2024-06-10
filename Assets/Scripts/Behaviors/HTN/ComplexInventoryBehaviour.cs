using System;
using System.Collections.Generic;
using System.Linq;
using Classes.Items.GOAP;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Behaviours.HTN
{
    public class ComplexInventoryBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<ItemBase> items = new();

        public Action<IHoldable> OnItemAdded;
        public Action<IHoldable> OnItemRemoved;

        void Update()
        {
            foreach(var item in items)
            {
                if (!item.ShouldDestroy())
                    continue;
                
                Remove(item);
            }
        }
        
        public void Add<T>(T item)
            where T : ItemBase
        {
            item.Pickup();
            
            if (this.items.Contains(item))
                return;

            item.gameObject.transform.parent = this.transform;
            this.items.Add(item);
            item.Claim();
            OnItemAdded?.Invoke(item);
        }
        
        public void Hold<T>(T item)
            where T : ItemBase
        {
            item.Pickup(true);

            item.gameObject.transform.position = this.transform.position + new Vector3(0f, 0.1f, -0.2f);
            item.gameObject.transform.parent = this.transform;

            if (!this.items.Contains(item))
                this.items.Add(item);
        }

        public T[] Get<T>()
        {
            return this.items.Where(x => x is T).Cast<T>().ToArray();
        }
        
        public void Remove<T>(T item)
            where T : ItemBase
        {
            this.items.Remove(item);
            item.Drop();
            
            if (!item.IsDestroyed && item != null && item.gameObject != null)
            {
                item.gameObject.transform.parent = null;
            }

            OnItemRemoved?.Invoke(item);            
        }
        
        public bool Has<T>()
            where T : ItemBase
        {
            return this.items.Any(x => x is T);
        }
        
        public int Count<T>()
            where T : ItemBase
        {
            return this.items.Count(x => x is T);
        }
        
        public bool Has(Type type, int amount)
        {
            return this.items.Count(x => x.GetType().IsInstanceOfType(type)) >= amount;
        }
    }
}