using Interfaces;
using UnityEngine;

namespace Behaviours.GOAP
{
    public class ItemSourceBase<T> : MonoBehaviour, ISource<T>
        where T : IGatherable
    {
    }
}