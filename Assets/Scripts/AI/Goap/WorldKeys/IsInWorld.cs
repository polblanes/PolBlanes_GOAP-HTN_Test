using CrashKonijn.Goap.Behaviours;
using Interfaces;

namespace AI.GOAP.WorldKeys
{
    public class IsInWorld<THoldable> : WorldKeyBase
        where THoldable : IHoldable {}
}