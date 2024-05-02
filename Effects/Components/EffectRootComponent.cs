﻿namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using Data;
    using Leopotam.EcsProto;


    /// <summary>
    /// effect root component
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectRootComponent : IProtoAutoReset<EffectRootComponent>
    {
        public EffectRootValue[] Value;
        
        public void AutoReset(ref EffectRootComponent c)
        {
            c.Value = Array.Empty<EffectRootValue>();
        }
    }
}