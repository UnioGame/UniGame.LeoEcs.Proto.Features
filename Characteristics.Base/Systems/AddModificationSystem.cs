﻿namespace unigame.ecs.proto.Characteristics.Base.Systems
{
    using System;
    using Components;
    using Components.Requests;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// add new modification to characteristic
    /// if modification already exist - update it
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AddModificationSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _addModificationFilter;
        private EcsFilter _modificationsFilter;
        
        private ProtoPool<CharacteristicLinkComponent> _characteristicLinkPool;
        private ProtoPool<AddModificationRequest> _requestPool;
        private ProtoPool<ModificationComponent> _modificationPool;
        private ProtoPool<CharacteristicValueComponent> _characteristicPool;
        private ProtoPool<ModificationSourceLinkComponent> _sourceLinkPool;
        private ProtoPool<RecalculateCharacteristicSelfRequest> _recalculatePool;
        private ProtoPool<CreateModificationRequest> _createPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _addModificationFilter = _world
                .Filter<AddModificationRequest>()
                .End();

            _modificationsFilter = _world
                .Filter<ModificationComponent>()
                .Inc<CharacteristicLinkComponent>()
                .End();

            _characteristicLinkPool = _world.GetPool<CharacteristicLinkComponent>();
            _requestPool = _world.GetPool<AddModificationRequest>();
            _createPool = _world.GetPool<CreateModificationRequest>();
            _modificationPool = _world.GetPool<ModificationComponent>();
            _characteristicPool = _world.GetPool<CharacteristicValueComponent>();
            _sourceLinkPool = _world.GetPool<ModificationSourceLinkComponent>();
            _recalculatePool = _world.GetPool<RecalculateCharacteristicSelfRequest>();
        }

        public void Run()
        {
            foreach (var requestEntity in _addModificationFilter)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                if (!requestComponent.Target.Unpack(_world, out var targetCharacteristicEntity))
                    continue;

                if (!requestComponent.Source.Unpack(_world, out var targetSourceEntity))
                    targetSourceEntity = ProtoEntity.FromIdx(-1);

                //check is target is characteristic entity
                if(!_characteristicPool.Has(targetCharacteristicEntity)) continue;
                
                var foundedCharacteristicEntity = ProtoEntity.FromIdx(-1);

                foreach (var modificationEntity in _modificationsFilter)
                {
                    ref var linkComponent = ref _characteristicLinkPool.Get(modificationEntity);
                    if (!linkComponent.Link.Unpack(_world, out var characteristicEntity)) continue;
                    
                    if (!characteristicEntity.Equals(targetCharacteristicEntity)) continue;

                    ref var sourceComponent = ref _sourceLinkPool.Get(modificationEntity);
                    if (!sourceComponent.Value.Unpack(_world, out var sourceEntity)) continue;
                    
                    if(!sourceEntity.Equals(targetSourceEntity)) continue;
                    
                    foundedCharacteristicEntity = characteristicEntity;

                    ref var modificationComponent = ref _modificationPool.Get(modificationEntity);
                    
                    if (!modificationComponent.AllowedSummation) break;

                    modificationComponent.Counter++;

                    _recalculatePool.GetOrAddComponent(foundedCharacteristicEntity);
                    
                    break;
                }
                
                if ((int)foundedCharacteristicEntity > 0) continue;

                var createEntity = _world.NewEntity();
                ref var createComponent = ref _createPool.Add(createEntity);
                createComponent.Target = requestComponent.Target;
                createComponent.ModificationSource = requestComponent.Source;
                createComponent.Modification = requestComponent.Modification;
            }
        }
    }
}