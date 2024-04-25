﻿namespace unigame.ecs.proto.Ability.SubFeatures.Target.Systems
{
    using System;
    using Aspects;
    using Components;
    using Core.Components;
     
    using TargetSelection;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Collections;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CleanupUnderTargetSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _underTargetFilter;
        private ProtoWorld _world;
        private TargetAbilityAspect _targetAspect;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _underTargetFilter = _world
                .Filter<UnderTheTargetComponent>()
                .Exc<PrepareToDeathComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _underTargetFilter)
            {
                ref var underTargetComponent = ref _targetAspect.UnderTheTarget.Get(entity);
                underTargetComponent.Count = 0;
            }
        }
    }
}