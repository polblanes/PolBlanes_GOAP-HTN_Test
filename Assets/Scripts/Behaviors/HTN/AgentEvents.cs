using Interfaces.HTN;

namespace Behaviours.HTN
{
    public class AgentEvents : IAgentEvents
    {
        // Targets
        public event TargetDelegate OnTargetInRange;
        public void TargetInRange(ITarget target)
        {
            this.OnTargetInRange?.Invoke(target);
        }

        public event TargetDelegate OnTargetOutOfRange;
        public void TargetOutOfRange(ITarget target)
        {
            this.OnTargetOutOfRange?.Invoke(target);
        }

        public event TargetRangeDelegate OnTargetChanged;
        public void TargetChanged(ITarget target, bool inRange)
        {
            this.OnTargetChanged?.Invoke(target, inRange);
        }

        public event TargetDelegate OnMove;
        public void Move(ITarget target)
        {
            this.OnMove?.Invoke(target);
        }
    }
}