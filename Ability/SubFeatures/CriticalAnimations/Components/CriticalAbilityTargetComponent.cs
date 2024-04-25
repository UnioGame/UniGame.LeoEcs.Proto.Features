﻿namespace unigame.ecs.proto.Ability.SubFeatures.CriticalAnimations.Components
{
    using System;
    using Code.Configuration.Runtime.Ability.Description;
     
    using UnityEngine.Serialization;

    /// <summary>
    /// mark ability suitable for critical animation
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CriticalAbilityTargetComponent
    {
        public AbilityId Value;
    }
}