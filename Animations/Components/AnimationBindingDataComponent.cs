﻿namespace unigame.ecs.proto.Animations.Components
{
    using System;
    using Code.Animations;

    /// <summary>
    /// AnimationBindingData Value
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AnimationBindingDataComponent
    {
        public PlayableBindingData Value;
    }
}