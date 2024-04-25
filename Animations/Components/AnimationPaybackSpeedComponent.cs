﻿namespace unigame.ecs.proto.Animations.Components
{
    using System;
     

    /// <summary>
    /// play speed value
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AnimationPaybackSpeedComponent : IProtoAutoReset<AnimationPaybackSpeedComponent>
    {
        public float Value;
        
        public void AutoReset(ref AnimationPaybackSpeedComponent c)
        {
            c.Value = 1f;
        }
    }
}