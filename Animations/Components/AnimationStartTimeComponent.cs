﻿namespace unigame.ecs.proto.Animations.Components
{
    using System;

    /// <summary>
    /// animation start time
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AnimationStartTimeComponent
    {
        public float Value;
    }
}