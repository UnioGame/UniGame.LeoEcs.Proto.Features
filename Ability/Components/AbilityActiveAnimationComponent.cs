﻿namespace unigame.ecs.proto.Ability.SubFeatures.AbilityAnimation.Components
{
    using System;
    using Leopotam.EcsProto.QoL;


    /// <summary>
    /// Value is target animation link
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityActiveAnimationComponent
    {
        public ProtoPackedEntity Value;
    }
}