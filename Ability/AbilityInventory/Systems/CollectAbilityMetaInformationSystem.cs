﻿namespace unigame.ecs.proto.AbilityInventory.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto.QoL;
    using ProtoWorldExtensions = UniGame.LeoEcs.Shared.Extensions.ProtoWorldExtensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CollectAbilityMetaInformationSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _filterRequest;
        
        private AbilityInventoryAspect _abilityInventory;
        private AbilityMetaAspect _metaAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
			
            _filterRequest = _world
                .Filter<EquipAbilitySelfRequest>()
                .Inc<AbilityMetaLinkComponent>()
                .Inc<AbilityConfigurationComponent>()
                .Exc<AbilityLoadingComponent>()
                .Exc<AbilityBuildingComponent>()
                .End();
        }

        public void Run()
        {
            foreach (var abilityEntity in _filterRequest)
            {
                ref var requestComponent = ref _abilityInventory.Equip.Get(abilityEntity);
                ref var linkComponent = ref _abilityInventory.MetaLink.Get(abilityEntity);
                ref var linkedEntity = ref linkComponent.Value;
                
                if (!linkedEntity.Unpack(_world,out var metaEntity))
                {
                    _world.DelEntity(abilityEntity);
                    continue;
                }
                
                ref var metaIdComponent = ref _metaAspect.Id.Get(metaEntity);
                ref var metaComponent = ref _metaAspect.Meta.Get(metaEntity);
                
                requestComponent.IsBlocked = metaComponent.IsBlocked;
                requestComponent.Hide = metaComponent.Hide;
                    
                ref var idComponent = ref _abilityInventory.Id.GetOrAddComponent(abilityEntity);
                idComponent.AbilityId = metaIdComponent.AbilityId;
                    
                ref var metaLinkComponent = ref _abilityInventory.MetaLink.GetOrAddComponent(abilityEntity);
                metaLinkComponent.Value = _world.PackEntity(metaEntity);
                    
                //copy visual to ability
                if(_metaAspect.Visual.Has(metaEntity))
                    _metaAspect.Visual.Copy(metaEntity,abilityEntity);
                
                ref var abilityEquipComponent = ref _abilityInventory.AbilityEquip.Add(abilityEntity);
                ref var abilityBuildingComponent = ref _abilityInventory.Building.Add(abilityEntity);
            }
        }
    }
}