using System;
using Interfaces;
using UnityEngine;

namespace Classes.Sources.HTN
{
    public class BoxSource : MonoBehaviour
    {
        public Type ItemType { get; set; }
        
        public void Place(IHoldable item)
        {
            item.Drop(true);
            item.gameObject.transform.position = this.transform.position + new Vector3(0f, 0.1f, 0f);
        }
    }
}