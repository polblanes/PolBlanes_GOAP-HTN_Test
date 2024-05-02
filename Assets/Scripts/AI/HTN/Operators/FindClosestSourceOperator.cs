using FluidHTN;
using FluidHTN.Operators;
using Unity.VisualScripting;
using Interfaces;
using Interfaces.HTN;
using Behaviours.HTN;
using UnityEngine;

namespace HTN.Operators
{
    public class FindClosestSourceOperator<T> : FindClosestOperator<ItemSourceBase<T>>
        where T : IGatherable
    {
        protected override AITargetType TargetType { get => AITargetType.Source; }
    }
}