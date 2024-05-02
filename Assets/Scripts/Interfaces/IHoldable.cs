using UnityEngine;

namespace Interfaces
{
    public interface IHoldable
    {
        string DebugName { get; set; }
        GameObject gameObject { get; }
        bool IsHeld { get; }
        bool IsInBox { get; }
        bool IsClaimed { get; }
        bool IsDestroyed { get; }

        void Claim();
        void Pickup(bool visible = false);
        void Drop(bool inBox = false);

        void MarkForDestroy();
    }
}