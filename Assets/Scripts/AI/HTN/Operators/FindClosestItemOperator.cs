using FluidHTN;
using FluidHTN.Operators;
using Unity.VisualScripting;
using UnityEngine;
using Classes.HTN;
using Interfaces;
using Interfaces.HTN;
using Behaviours.HTN;
using System.Collections.Generic;
using System;

namespace HTN.Operators
{
    public class FindClosestItemOperator<T> : FindClosestOperator<T>
        where T : MonoBehaviour, IHoldable
    {
        
        public override void GetCollection()
        {
            T[] tempCollection = GameObject.FindObjectsOfType<T>();

            List<T> UnclaimedItems = new List<T>(); 
            foreach (T element in tempCollection)
            {                
                if (element.IsClaimed)
                    continue;

                UnclaimedItems.Add(element);
            }  

            collection = UnclaimedItems.ToArray();
        }
    }
}