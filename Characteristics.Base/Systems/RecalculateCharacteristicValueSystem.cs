﻿namespace unigame.ecs.proto.Characteristics.Base.Systems
{
    using System;
    using Components;
    using Components.Requests;
    using unigame.ecs.proto.Core.Components;
    using Game.Modules.UnioModules.UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

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
    public class RecalculateCharacteristicValueSystem : IProtoInitSystem, IProtoRunSystem
    {
        private const float HundredPercent = 100.0f;
        
        private ProtoWorld _world;
        private EcsFilter _recalculateRequestFilter;
        
        private ProtoPool<CharacteristicValueComponent> _characteristicsPool;
        
        private ProtoPool<CharacteristicChangedComponent> _characteristicsChangedPool;
        private ProtoPool<MinValueComponent> _minPool;
        private ProtoPool<MaxValueComponent> _maxPool;
        private ProtoPool<CharacteristicPreviousValueComponent> _previousValuePool;
        private ProtoPool<PercentModificationsValueComponent> _percentValuePool;
        private ProtoPool<MaxLimitModificationsValueComponent> _maxLimitValuePool;
        private ProtoPool<BaseModificationsValueComponent> _baseModificationsValuePool;
        private ProtoPool<CharacteristicDefaultValueComponent> _defaulValuePool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _recalculateRequestFilter = _world
                .Filter<RecalculateCharacteristicSelfRequest>()
                .Inc<CharacteristicValueComponent>()
                .Inc<CharacteristicBaseValueComponent>()
                .Inc<MaxLimitModificationsValueComponent>()
                .Inc<BaseModificationsValueComponent>()
                .Inc<PercentModificationsValueComponent>()
                .Inc<MaxValueComponent>()
                .End();

            _characteristicsPool = _world.GetPool<CharacteristicValueComponent>();
            _defaulValuePool = _world.GetPool<CharacteristicDefaultValueComponent>();
            
            _minPool = _world.GetPool<MinValueComponent>();
            _maxPool = _world.GetPool<MaxValueComponent>();
            
            _characteristicsChangedPool = _world.GetPool<CharacteristicChangedComponent>();
            _previousValuePool = _world.GetPool<CharacteristicPreviousValueComponent>();
            _percentValuePool = _world.GetPool<PercentModificationsValueComponent>();
            _maxLimitValuePool = _world.GetPool<MaxLimitModificationsValueComponent>();
            _baseModificationsValuePool = _world.GetPool<BaseModificationsValueComponent>();
        }

        public void Run()
        {
            foreach (var characteristicEntity in _recalculateRequestFilter)
            {
                ref var characteristicComponent = ref _characteristicsPool.Get(characteristicEntity);
                ref var defaultValueComponent = ref _defaulValuePool.Get(characteristicEntity);
                ref var minComponent = ref _minPool.Get(characteristicEntity);
                ref var maxComponent = ref _maxPool.Get(characteristicEntity);
                ref var previousValueComponent = ref _previousValuePool.Get(characteristicEntity);
                ref var percentValueComponent = ref _percentValuePool.Get(characteristicEntity);
                ref var maxLimitValueComponent = ref _maxLimitValuePool.Get(characteristicEntity);
                ref var baseModificationsValueComponent = ref _baseModificationsValuePool.Get(characteristicEntity);

                var previousValue = characteristicComponent.Value;
                var previousMaxLimit = maxComponent.Value;

                var maxValue = defaultValueComponent.MaxValue + maxLimitValueComponent.Value;
                maxComponent.Value = maxValue;
                
                var newValue = baseModificationsValueComponent.Value;
                newValue *= percentValueComponent.Value / HundredPercent;
                newValue = Mathf.Clamp(newValue, minComponent.Value,maxComponent.Value);

                if(Mathf.Approximately(previousMaxLimit , maxValue) && 
                   Mathf.Approximately(previousValue , newValue)) continue;

                previousValueComponent.Value = previousValue;
                characteristicComponent.Value = newValue;
                
                ref var changedComponent = ref _characteristicsChangedPool.GetOrAddComponent(characteristicEntity);
                changedComponent.PreviousValue = previousValue;
                changedComponent.Value = newValue;
            }
        }
    }
}