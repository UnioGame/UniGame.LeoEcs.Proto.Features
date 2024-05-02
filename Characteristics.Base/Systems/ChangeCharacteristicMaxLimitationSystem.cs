﻿namespace UniGame.Ecs.Proto.Characteristics.Base.Systems
{
    using System;
    using Components;
    using Components.Requests;
    using Game.Modules.UnioModules.UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// changed base value of characteristics
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class ChangeCharacteristicMaxLimitationSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _changeRequestFilter;
        
        private ProtoPool<ChangeMaxLimitRequest> _requestPool;
        
        private ProtoPool<MinValueComponent> _minPool;
        private ProtoPool<MaxValueComponent> _maxPool;
        private ProtoPool<CharacteristicBaseValueComponent> _baseValuePool;
        private ProtoPool<RecalculateCharacteristicSelfRequest> _recalculatePool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _changeRequestFilter = _world
                .Filter<ChangeMaxLimitRequest>()
                .End();

            _requestPool = _world.GetPool<ChangeMaxLimitRequest>();
            _minPool = _world.GetPool<MinValueComponent>();
            _baseValuePool = _world.GetPool<CharacteristicBaseValueComponent>();
            _maxPool = _world.GetPool<MaxValueComponent>();
            _recalculatePool = _world.GetPool<RecalculateCharacteristicSelfRequest>();
        }

        public void Run()
        {

            foreach (var requestEntity in _changeRequestFilter)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                var value = requestComponent.Value;
                
                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_baseValuePool.Has(characteristicEntity))
                    continue;
                
                ref var minComponent = ref _minPool.Get(characteristicEntity);
                ref var maxComponent = ref _maxPool.Get(characteristicEntity);
                ref var baseValueComponent = ref _baseValuePool.Get(characteristicEntity);

                maxComponent.Value = value <= minComponent.Value ? minComponent.Value : value;

                baseValueComponent.Value = Mathf.Clamp(baseValueComponent.Value, minComponent.Value, maxComponent.Value);
                
                _recalculatePool.GetOrAddComponent(characteristicEntity);
            }
            
        }
    }
}