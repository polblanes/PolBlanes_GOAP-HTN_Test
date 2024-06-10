using System;
using FluidHTN;

namespace HTN.Effects
{
    public class DecrementWorldStateEffect : IEffect
    {
        public string Name { get; }
        public EffectType Type { get; }
        public AIWorldState State { get; }
        public byte Value { get; }

        public DecrementWorldStateEffect(AIWorldState state, EffectType type)
        {
            Name = $"DecrementState({state})";
            Type = type;
            State = state;
            Value = 1;
        }

        public DecrementWorldStateEffect(AIWorldState state, byte value, EffectType type)
        {
            Name = $"DecrementState({state})";
            Type = type;
            State = state;
            Value = value;
        }

        public void Apply(IContext ctx)
        {
            if (ctx is AIAgentContext c)
            {
                var currentValue = c.GetState(State);
                byte AmountToDecrement = Value;

                if (currentValue < Value)
                {
                    AmountToDecrement = currentValue;
                }

                c.SetState(State, (byte)(currentValue - AmountToDecrement), Type);
                if (ctx.LogDecomposition) ctx.Log(Name, $"DecrementWorldStateEffect.Apply({State}:{currentValue}-{Value}:{Type})", ctx.CurrentDecompositionDepth+1, this);
                return;
            }

            throw new Exception("Unexpected context type!");
        }
    }
}