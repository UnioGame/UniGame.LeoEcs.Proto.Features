﻿namespace UniGame.Ecs.Proto.GameEffects.FreezeEffect.Components
{
    using System;
    using Leopotam.EcsProto.QoL;
    
    /// <summary>
    /// Says that the freezing effect is used on the target
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct FreezeTargetEffectComponent
    {
        public ProtoPackedEntity Source;
        // Creating time ability + Duration
        public float DumpTime;
    }
}