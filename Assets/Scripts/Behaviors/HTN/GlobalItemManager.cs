using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Behaviours.HTN
{
    public class GlobalItemManager : MonoBehaviour
    {
        public Action<IHoldable> OnItemClaimed;
        public Action<IHoldable> OnItemPickedUp;
        public Action<IHoldable> OnItemDropped;
    }
}