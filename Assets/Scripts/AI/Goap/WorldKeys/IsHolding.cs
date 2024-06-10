using CrashKonijn.Goap.Behaviours;
using Interfaces;

namespace AI.GOAP.WorldKeys
{
    public class IsHolding<THoldable> : WorldKeyBase
        where THoldable : IHoldable {}
}