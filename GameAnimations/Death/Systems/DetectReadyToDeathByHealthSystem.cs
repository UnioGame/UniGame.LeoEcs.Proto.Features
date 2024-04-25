﻿namespace unigame.ecs.proto.Gameplay.Death.Systems
{
    using System;
    using Characteristics.Base.Components;
    using Core.Components;
    using Characteristics.Health.Components;
    using unigame.ecs.proto.Core.Death.Components;
     
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    
    
    
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class DetectReadyToDeathByHealthSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private EcsFilter _filterDestinations;
        private ProtoWorld _world;
        
        private ProtoPool<CharacteristicComponent<HealthComponent>> _characteristicPool;
        private ProtoPool<PrepareToDeathComponent> _preparePool;
        private ProtoPool<PrepareToDeathEvent> _prepareEventPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CharacteristicChangedComponent<HealthComponent>>()
                .Inc<CharacteristicComponent<HealthComponent>>()
                .Inc<HealthComponent>()
                .Exc<KillRequest>()
                .Exc<PrepareToDeathComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var healthComponent = ref _characteristicPool.Get(entity);
                
                if(healthComponent.Value > 0.0f) continue;

                ref var prepareToDeath = ref _preparePool.GetOrAddComponent(entity);
                prepareToDeath.Source = _world.PackEntity(entity);
                
                var eventEntity = _world.NewEntity();
                ref var prepareToDeathEvent = ref _prepareEventPool.GetOrAddComponent(eventEntity);
                prepareToDeathEvent.Source = _world.PackEntity(entity);
            }
        }
    }
}