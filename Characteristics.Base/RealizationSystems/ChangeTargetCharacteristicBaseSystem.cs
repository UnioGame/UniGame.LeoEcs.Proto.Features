﻿namespace unigame.ecs.proto.Characteristics.Base.RealizationSystems
{
    using System;
    using Components;
    using Components.Requests;
    using Components.Requests.OwnerRequests;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;


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
    public class ChangeTargetCharacteristicBaseSystem<TCharacteristic> : IProtoInitSystem, IProtoRunSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        private EcsFilter _changeRequestFilter;
        
        private ProtoPool<ChangeCharacteristicBaseRequest<TCharacteristic>> _requestPool;
        
        private ProtoPool<ChangeCharacteristicBaseRequest> _changePool;
        private ProtoPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _changeRequestFilter = _world
                .Filter<ChangeCharacteristicBaseRequest<TCharacteristic>>()
                .End();

            _requestPool = _world.GetPool<ChangeCharacteristicBaseRequest<TCharacteristic>>();
            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _changePool = _world.GetPool<ChangeCharacteristicBaseRequest>();
        }

        public void Run()
        {
            foreach (var requestEntity in _changeRequestFilter)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_linkPool.Has(characteristicEntity)) continue;

                ref var linkComponent = ref _linkPool.Get(characteristicEntity);
                
                var targetEntity = _world.NewEntity();
                ref var targetRequest = ref _changePool.Add(targetEntity);
                
                targetRequest.Target = linkComponent.Value;
                targetRequest.Source = requestComponent.Source;
                targetRequest.Value = requestComponent.Value;
            }
        }
    }
}