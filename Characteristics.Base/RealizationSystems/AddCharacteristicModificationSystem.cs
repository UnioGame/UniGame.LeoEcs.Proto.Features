﻿namespace UniGame.Ecs.Proto.Characteristics.Base.RealizationSystems
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
    /// add modification to target characteristic owner by request
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AddCharacteristicModificationSystem<TCharacteristic> : IProtoInitSystem, IProtoRunSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        private EcsFilter _requestFiler;
        
        private ProtoPool<AddModificationRequest<TCharacteristic>> _requestPool;
        private ProtoPool<AddModificationRequest> _modificationPool;
        private ProtoPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _requestFiler = _world
                .Filter<AddModificationRequest<TCharacteristic>>()
                .End();

            _requestPool = _world.GetPool<AddModificationRequest<TCharacteristic>>();
            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _modificationPool = _world.GetPool<AddModificationRequest>();
            
        }

        public void Run()
        {
            foreach (var requestEntity in _requestFiler)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);

                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_linkPool.Has(characteristicEntity)) continue;
                
                ref var linkComponent = ref _linkPool.Get(characteristicEntity);
                
                var entity = _world.NewEntity();
                ref var modificationComponent = ref _modificationPool.Add(entity);
                
                modificationComponent.Modification = requestComponent.Modification;
                modificationComponent.Source = requestComponent.ModificationSource;
                modificationComponent.Target = linkComponent.Value;
            }
        }
    }
    
    // <summary>
}