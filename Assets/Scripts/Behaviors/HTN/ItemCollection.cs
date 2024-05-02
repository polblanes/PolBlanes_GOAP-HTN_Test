using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Behaviours.HTN
{
    public class ItemCollection : MonoBehaviour
    {
        public List<ItemBase> items { get => new List<ItemBase>(_items); }
        private List<ItemBase> _items = new();

        void Update()
        {
            foreach(var item in items)
            {
                if (!item.ShouldDestroy())
                    continue;
                
                Remove(item);
            }
        }
        
        public void Add(ItemBase item)
        {
            this._items.Add(item);
        }
        
        public void Remove(ItemBase item)
        {
            this._items.Remove(item);
        }
        
        public IHoldable[] All()
        {
            return this._items.ToArray();
        }

        public IHoldable[] Filtered(bool canBeHeld, bool canBeInBox, bool canBeClaimed = false)
        {
            if (canBeInBox && canBeHeld && canBeClaimed)
                return this.All();

            var items = this._items as IEnumerable<ItemBase>;
            
            if (!canBeHeld)
                items = items.Where(x => x.IsHeld == false);
            
            if (!canBeInBox)
                items = items.Where(x => x.IsInBox == false);
            
            if (!canBeClaimed)
                items = items.Where(x => x.IsClaimed == false);

            return items.ToArray();
        }

        public T[] Get<T>()
            where T : IHoldable
        {
            return this._items.Where(x => x is T).Cast<T>().ToArray();
        }
        
        public T[] GetFiltered<T>(bool canBeHeld, bool canBeInBox, bool canBeClaimed)
            where T : IHoldable
        {
            return this.Filtered(canBeHeld, canBeInBox, canBeClaimed).Where(x => x is T).Cast<T>().ToArray();
        }

        public bool Any<T>()
            where T : IHoldable
        {
            return this._items.Any(x => x is T);
        }

        public int Count(bool canBeInBox, bool canBeHeld)
        {
            return this.Filtered(canBeInBox, canBeHeld).Length;
        }

        public IHoldable Closest(Vector3 position, bool canBeInBox, bool canBeHeld, bool canBeClaimed)
        {
            return this.Filtered(canBeInBox, canBeHeld, canBeClaimed)
                .OrderBy(x => Vector3.Distance(x.gameObject.transform.position, position))
                .FirstOrDefault();
        }
    }
}