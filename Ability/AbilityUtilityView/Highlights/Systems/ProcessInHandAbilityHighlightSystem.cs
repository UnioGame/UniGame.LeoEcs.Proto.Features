﻿namespace unigame.ecs.proto.Ability.AbilityUtilityView.Highlights.Systems
{
    using System;
    using AbilityUtilityView.Components;
    using Common.Components;
    using Components;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using SubFeatures.Target.Components;
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
    public sealed class ProcessInHandAbilityHighlightSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        
        private ProtoPool<AbilityTargetsComponent> _abilityTargetPool;
        private ProtoPool<OwnerComponent> _ownerPool;
        private ProtoPool<VisibleUtilityViewComponent> _visiblePool;
        private ProtoPool<ShowHighlightRequest> _showHighlightPool;
        private ProtoPool<HideHighlightRequest> _hideHighlightPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AbilityInHandComponent>()
                .Inc<AbilityTargetsComponent>()
                .Inc<OwnerComponent>()
                .End();
            
            _abilityTargetPool = _world.GetPool<AbilityTargetsComponent>();
            _ownerPool = _world.GetPool<OwnerComponent>();
            _visiblePool = _world.GetPool<VisibleUtilityViewComponent>();
            _showHighlightPool = _world.GetPool<ShowHighlightRequest>();
            _hideHighlightPool = _world.GetPool<HideHighlightRequest>();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _ownerPool.Get(entity);
                
                if(!owner.Value.Unpack(_world, out var ownerEntity))
                    continue;
                
                if(!_visiblePool.Has(ownerEntity))
                    continue;
                
                ref var chosenTarget = ref _abilityTargetPool.Get(entity);
                var count = chosenTarget.Count;

                for (int i = 0; i < count; i++)
                {
                    var packedEntity = chosenTarget.Entities[i];
                    var showRequestEntity = _world.NewEntity();
                    ref var showRequest = ref _showHighlightPool.Add(showRequestEntity);
                
                    showRequest.Source = _world.PackEntity(entity);
                    showRequest.Destination = packedEntity;   
                }

                var previousCount = chosenTarget.PreviousCount;
                for (int i = 0; i < previousCount; i++)
                {
                    var packedEntity = chosenTarget.PreviousEntities[i];
                    var targetFound = false;
                    foreach (var chosenTargetEntity in chosenTarget.Entities)
                    {
                        if(!chosenTargetEntity.EqualsTo(packedEntity)) continue;
                        targetFound = true;
                        break;
                    }
                    
                    if(targetFound) continue;
                    
                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref _hideHighlightPool.Add(hideRequestEntity);
                
                    hideRequest.Source = _world.PackEntity(entity);
                    hideRequest.Destination = packedEntity;
                }
            }
        }
    }
}