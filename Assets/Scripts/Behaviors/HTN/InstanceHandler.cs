using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Behaviours.HTN
{
    public class InstanceHandler : MonoBehaviour
    {
        public Action<IHoldable> OnItemDestroyed;
        private List<GameObject> queue = new();

        private void LateUpdate()
        {
            foreach (var item in this.queue)
            {
                Destroy(item.gameObject);
            }
            
            this.queue.Clear();
        }

        public void QueueForDestroy(IHoldable item)
        {
            if (item == null)
                return;

            item.MarkForDestroy();

            if (item.gameObject == null)
                return;
            
            OnItemDestroyed?.Invoke(item);
            this.QueueForDestroy(item.gameObject);
        }

        public void QueueForDestroy(GameObject item)
        {
            if (this.queue.Contains(item))
            {
                Debug.LogError("Item already queued for destruction");
                return;
            }
            
            if (item == null)
                return;
            
            this.queue.Add(item);
            item.gameObject.SetActive(false);
        }
    }
}