using Behaviours.HTN;

namespace Interfaces.HTN
{
    public interface IAgentEvents
    {        
        // Targets
        event TargetDelegate OnTargetInRange;
        void TargetInRange(ITarget target);
        
        event TargetDelegate OnTargetOutOfRange;
        void TargetOutOfRange(ITarget target);
        
        event TargetRangeDelegate OnTargetChanged;
        void TargetChanged(ITarget target, bool inRange);

        event TargetDelegate OnMove;
        void Move(ITarget target);
    }
}