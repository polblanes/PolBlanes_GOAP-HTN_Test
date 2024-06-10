using CrashKonijn.Goap.Behaviours;
using Interfaces;

namespace AI.GOAP.WorldKeys
{
    public class CreatedItem<TCreatable> : WorldKeyBase
        where TCreatable : ICreatable {}
}