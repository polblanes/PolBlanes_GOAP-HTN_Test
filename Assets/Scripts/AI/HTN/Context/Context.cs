using System;
using System.Collections;
using System.Collections.Generic;
using FluidHTN;
using FluidHTN.Debug;
using FluidHTN.Compounds;
using FluidHTN.Contexts;
using FluidHTN.Factory;
using Interfaces;
using Classes.Items.HTN;
using Behaviours.HTN;
using Classes.Sources.HTN;

using UnityEngine;

namespace HTN
{
    public enum AIWorldState : byte
    {
        Error,

        // Self
        IsHungry,

        // World                    // Inventory                // Target
        IronIsInWorld,              HasAxe,                     HasTargetClose,
        WoodIsInWorld,              HasPickaxe,                 TargetIsTree,
        AxeIsInWorld,               HasIron,                    TargetIsMine,
        PickaxeIsInWorld,           HasWood,                    TargetIsAxe,
        MineIsInWorld,              HasApple,                   TargetIsPickaxe,
        TreeIsInWorld,                                          TargetIsWood,
        AnvilIsInWorld,                                         TargetIsIron,
        AppleIsInWorld,                                         TargetIsAnvil,
                                                                TargetIsApple,
                                                                TargetIsPosition
    }

    public enum AIDestinationTarget : byte
    {
        None,
        Tree,
        Mine
    }

    public class AIWorldKeys
    {
        public static AIWorldState IsInWorld(Type type)
        {
            if (type == typeof(Apple))
                return AIWorldState.AppleIsInWorld;
            
            if (type == typeof(Iron))
                return AIWorldState.IronIsInWorld;
            
            if (type == typeof(Wood))
                return AIWorldState.WoodIsInWorld;
            
            if (type == typeof(IronMineSource))
                return AIWorldState.MineIsInWorld;

            if (type == typeof(TreeSource))
                return AIWorldState.TreeIsInWorld;
            
            if (type == typeof(AnvilSource))
                return AIWorldState.AnvilIsInWorld;
            
            if (type == typeof(Axe))
                return AIWorldState.AxeIsInWorld;
            
            if (type == typeof(Pickaxe))
                return AIWorldState.PickaxeIsInWorld;

            return AIWorldState.Error;
        }

        public static AIWorldState Has(Type type)
        {
            if (type == typeof(Apple))
                return AIWorldState.HasApple;
            
            if (type == typeof(Iron))
                return AIWorldState.HasIron;
            
            if (type == typeof(Wood))
                return AIWorldState.HasWood;
            
            if (type == typeof(Axe))
                return AIWorldState.HasAxe;
            
            if (type == typeof(Pickaxe))
                return AIWorldState.HasPickaxe;

            return AIWorldState.Error;
        }

        public static AIWorldState TargetIs(Type type)
        {
            if (type == typeof(Apple))
                return AIWorldState.TargetIsApple;
            
            if (type == typeof(Iron))
                return AIWorldState.TargetIsIron;
            
            if (type == typeof(Wood))
                return AIWorldState.TargetIsWood;
            
            if (type == typeof(IronMineSource))
                return AIWorldState.TargetIsMine;

            if (type == typeof(TreeSource))
                return AIWorldState.TargetIsTree;
            
            if (type == typeof(AnvilSource))
                return AIWorldState.TargetIsAnvil;
            
            if (type == typeof(Axe))
                return AIWorldState.TargetIsAxe;
            
            if (type == typeof(Pickaxe))
                return AIWorldState.TargetIsPickaxe;

            return AIWorldState.Error;
        }
    }
}

namespace HTN
{
    public class AIGlobalContext : BaseContext
    {
        public override IFactory Factory { get; set; }
        public override List<string> MTRDebug { get; set; }
        public override List<string> LastMTRDebug { get; set; }
        public override bool DebugMTR { get; } = false;
        public override Queue<FluidHTN.Debug.IBaseDecompositionLogEntry> DecompositionLog { get; set; }
        public override bool LogDecomposition { get; } = false;

        public override byte[] WorldState { get; } = new byte[Enum.GetValues(typeof(AIWorldState)).Length];
    }
}

namespace HTN
{
    public partial class AIAgentContext : BaseContext
    {
        public AIAgent Agent { get; }
        public override IFactory Factory { get; set; } = new DefaultFactory();
        public override List<string> MTRDebug { get; set; }
        public override List<string> LastMTRDebug { get; set; }
        public override bool DebugMTR { get; } = true;
        public override Queue<FluidHTN.Debug.IBaseDecompositionLogEntry> DecompositionLog { get; set; }
        public override bool LogDecomposition { get; } = true;

        public override byte[] WorldState { get; } = new byte[Enum.GetValues(typeof(AIWorldState)).Length];

        public bool HasState(AIWorldState state, bool value)
        {
            return HasState((int) state, (byte) (value ? 1 : 0));
        }

        public bool HasState(AIWorldState state, byte value)
        {
            return HasState((int)state, value);
        }

        public bool HasState(AIWorldState state)
        {
            return HasState((int) state, 1);
        }

        public void SetState(AIWorldState state, bool value, EffectType type)
        {
            SetState((int) state, (byte) (value ? 1 : 0), true, type);
        }

        public void SetState(AIWorldState state, byte value, EffectType type)
        {
            byte current = GetState(state);
            SetState((int)state, value, true, type);
            byte after = GetState(state);
        }

        public void IncrementState(AIWorldState state, byte value, EffectType type)
        {
            byte current = GetState((int)state);
            SetState((int)state, (byte)Math.Min(current + value, 255), true, type);
            byte after = GetState(state);

            // if (state != AIWorldState.WoodIsInWorld || type != EffectType.Permanent)
            //     return;

            // UnityEngine.Debug.Log($"{Agent.GetInstanceID()} - SetState:{state} {current} -> {after}");
        }

        public void DecrementState(AIWorldState state, byte value, EffectType type)
        {
            byte current = GetState((int)state);
            SetState((int)state, (byte)Math.Max(current - value, 0), true, type);
            byte after = GetState(state);

            // if (state != AIWorldState.WoodIsInWorld || type != EffectType.Permanent)
            //     return;

            // UnityEngine.Debug.Log($"{Agent.GetInstanceID()} - SetState:{state} {current} -> {after}");
        }

        public byte GetState(AIWorldState state)
        {
            return GetState((int) state);
        }
    }
}