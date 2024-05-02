﻿namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Tools;
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
    public sealed class ProcessSetInHandAbilityBySlotRequestSystem : IProtoRunSystem,IProtoInitSystem
    {
        private AbilityTools _abilityTools;
        private EcsFilter _filter;
        private ProtoWorld _world;
        
        private ProtoPool<SetInHandAbilityBySlotSelfRequest> _setInHandPool;
        private ProtoPool<SetInHandAbilitySelfRequest> _requestPool;
        private ProtoPool<AbilityMapComponent> _abilityMapPool;

        public ProcessSetInHandAbilityBySlotRequestSystem(AbilityTools abilityTools)
        {
            _abilityTools = abilityTools;
        }
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<SetInHandAbilityBySlotSelfRequest>()
                .Inc<AbilityMapComponent>()
                .Inc<AbilityInHandLinkComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var setInHand = ref _setInHandPool.Get(entity);
                ref var abilityMap = ref _abilityMapPool.Get(entity);
                
                // var isAnyAbilityUsing = _abilityTools.IsAnyAbilityInUse(_world, entity);
                // if(isAnyAbilityUsing)
                //     continue;

                var slot = setInHand.AbilityCellId;
                if(slot < 0 || slot >= abilityMap.AbilityEntities.Count)
                    continue;
                
                var packedAbilityEntity = abilityMap.AbilityEntities[slot];
                ref var request = ref _requestPool.GetOrAddComponent(entity);
                request.Value = packedAbilityEntity;
            }
        }
    }
}