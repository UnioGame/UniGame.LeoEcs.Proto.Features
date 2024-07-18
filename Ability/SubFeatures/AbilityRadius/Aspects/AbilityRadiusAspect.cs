﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.AbilityAnimation.Aspects
{
    using System;
    using Characteristics.Attack.Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AbilityRadiusAspect : EcsAspect
    {
        public ProtoPool<ApplyAbilityRadiusRangeRequest> ApplyRadiusRange;
    }
}