﻿namespace UniGame.Ecs.Proto.Characteristics.Base.Systems
{
    using System;
    using Components;
    using Components.Requests;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;


    /// <summary>
    /// recalculate characteristic by modifications
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class RecalculatePercentValueSystem : IProtoInitSystem, IProtoRunSystem
    {
        private const float HundredPercent = 100.0f;
        
        private ProtoWorld _world;
        private EcsFilter _recalculateRequestFilter;
        private EcsFilter _percentModificationsFilter;
        
        private ProtoPool<ModificationComponent> _modificationPool;
        private ProtoPool<CharacteristicLinkComponent> _characteristicsValueLinkPool;
        private ProtoPool<PercentModificationsValueComponent> _percentModificationsPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _recalculateRequestFilter = _world
                .Filter<RecalculateCharacteristicSelfRequest>()
                .Inc<CharacteristicValueComponent>()
                .Inc<CharacteristicBaseValueComponent>()
                .Inc<PercentModificationsValueComponent>()
                .End();
            
            _percentModificationsFilter = _world
                .Filter<ModificationComponent>()
                .Inc<ModificationPercentComponent>()
                .Inc<CharacteristicLinkComponent>()
                .End();
    
            _modificationPool = _world.GetPool<ModificationComponent>();
            _characteristicsValueLinkPool = _world.GetPool<CharacteristicLinkComponent>();
            _percentModificationsPool = _world.GetPool<PercentModificationsValueComponent>();
        }

        public void Run()
        {
            foreach (var characteristicEntity in _recalculateRequestFilter)
            {
                ref var percentValueComponent = ref _percentModificationsPool.Get(characteristicEntity);

                var percentModification = HundredPercent;
                
                foreach (var modificationEntity in _percentModificationsFilter)
                {
                    ref var linkComponent = ref _characteristicsValueLinkPool.Get(modificationEntity);
                    if(!linkComponent.Link.Unpack(_world,out var characteristicValue))
                        continue;
                    if(!characteristicEntity.Equals(characteristicValue)) continue;
                    
                    ref var modificationComponent = ref _modificationPool.Get(modificationEntity);
                    percentModification += modificationComponent.Counter * modificationComponent.BaseValue;
                }
                
                percentValueComponent.Value = percentModification;
            }
        }
    }
}