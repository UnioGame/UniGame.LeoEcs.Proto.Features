﻿namespace unigame.ecs.proto.AbilityInventory.Systems
{
    using System;
    using Ability.Common.Components;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// check is meta data loaded for ability
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CheckEquipAbilityLoadingSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _filterRequest;
        
        private AbilityInventoryAspect _abilityInventory;
        private AbilityMetaAspect _metaAspect;
        private EcsFilter _metaFilter;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
			
            _filterRequest = _world
                .Filter<EquipAbilitySelfRequest>()
                .Inc<AbilityLoadingComponent>()
                .Exc<AbilityBuildingComponent>()
                .End();

            _metaFilter = _world
                .Filter<AbilityMetaComponent>()
                .Inc<AbilityIdComponent>()
                .End();
        }

        public void Run()
        {
            foreach (var requestEntity in _filterRequest)
            {
                ref var requestComponent = ref _abilityInventory.Equip.Get(requestEntity);
                var metaExists = false;
                
                foreach (var metaEntity in _metaFilter)
                {
                    ref var metaIdComponent = ref _metaAspect.Id.Get(metaEntity);
                    metaExists = metaIdComponent.AbilityId == requestComponent.AbilityId;
                    if(metaExists) break;
                }

                if (!metaExists) continue;
                
                _abilityInventory.Loading.Del(requestEntity);
            }
        }
    }
    

}