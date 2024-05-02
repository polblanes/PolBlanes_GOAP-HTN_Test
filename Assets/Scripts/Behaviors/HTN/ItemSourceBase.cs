using Interfaces;
using UnityEngine;

namespace Behaviours.HTN
{
    public class ItemSourceBase<T> : MonoBehaviour, ISource<T>
        where T : IGatherable
    {
    }
}