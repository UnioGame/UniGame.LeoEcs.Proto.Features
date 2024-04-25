﻿namespace unigame.ecs.proto.Ability.SubFeatures.AbilitySequence.Systems
{
    using System;
    using Aspects;
    using Components;
    using AbilitySequence;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// activate ability sequence when it ready and request exist
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ActivateAbilitySequenceByNameSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _sequenceFilter;
        private EcsFilter _requestFilter;

        private AbilitySequenceAspect _aspect;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _requestFilter = _world
                .Filter<ActivateAbilitySequenceByNameRequest>()
                .End();
            
            _sequenceFilter = _world
                .Filter<AbilitySequenceReadyComponent>()
                .Inc<AbilitySequenceComponent>()
                .Inc<NameComponent>()
                .Inc<OwnerComponent>()
                .End();
        }

        public void Run()
        {
            foreach (var requestEntity in _requestFilter)
            {
                ref var requestComponent = ref _aspect.ActivateByName.Get(requestEntity);
                if(string.IsNullOrEmpty(requestComponent.Name))
                    continue;
                
                foreach (var sequenceEntity in _sequenceFilter)
                {
                    ref var ownerComponent = ref _aspect.Owner.Get(sequenceEntity);
                    if(!ownerComponent.Value.Equals(requestComponent.Owner)) continue;

                    ref var nameComponent = ref _aspect.Name.Get(sequenceEntity);
                    if(!nameComponent.Value.Equals(requestComponent.Name)) continue;

                    _aspect.Activate.GetOrAddComponent(sequenceEntity);
                }
            }
        }
    }
}