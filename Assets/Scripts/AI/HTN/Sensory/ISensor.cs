using HTN;

namespace HTN.Sensors
{
    public interface ISensor
    {
        float TickRate { get; }
        float NextTickTime { get; set; }
        void Tick(AIAgentContext context);
        void DrawGizmos(AIAgentContext context);
    }
}