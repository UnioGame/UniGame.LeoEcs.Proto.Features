﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.CriticalAnimations.Behaviours
{
    using System;
    using Abstract;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// select animation one by one from variants 
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class LinearAnimationSelectionBehavior : IAbilityAnimationBehavior
    {
        public void Compose(ProtoWorld world, ProtoEntity abilityEntity)
        {
            world.GetOrAddComponent<LinearAbilityAnimationSelectionComponent>(abilityEntity);
        }
    }
}