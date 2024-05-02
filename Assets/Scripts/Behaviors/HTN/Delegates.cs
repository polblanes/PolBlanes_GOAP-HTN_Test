using Interfaces.HTN;

namespace Behaviours.HTN
{
    public delegate void TargetDelegate(ITarget target);
    public delegate void TargetRangeDelegate(ITarget target, bool inRange);
}