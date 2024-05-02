﻿namespace UniGame.Ecs.Proto.Animations.Components
{
    using System;
    using Leopotam.EcsProto.QoL;


    /// <summary>
    /// target for animation
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AnimationTargetComponent
    {
        public ProtoPackedEntity Value;
    }
}