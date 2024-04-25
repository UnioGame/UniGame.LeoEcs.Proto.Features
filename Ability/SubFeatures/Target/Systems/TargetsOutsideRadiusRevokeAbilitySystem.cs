﻿namespace unigame.ecs.proto.Ability.SubFeatures.Target.Systems
{
    using System;
    using Ability.Aspects;
    using Aspects;
    using Characteristics.Radius.Component;
    using Common.Components;
    using Components;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class TargetsOutsideRadiusRevokeAbilitySystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;

        private AbilityAspect _abilityAspect;
        private TargetAbilityAspect _targetAspect;
        
        private ProtoPool<AbilityTargetsComponent> _targetsPool;
        private ProtoPool<AbilityTargetsOutsideEvent> _outsidePool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world.Filter<AbilityValidationSelfRequest>()
                .Inc<TargetableAbilityComponent>()
                .Inc<AbilityTargetsComponent>()
                .Inc<RadiusComponent>()
                .Inc<OwnerComponent>()
                .Exc<NonTargetAbilityComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var radius = ref _abilityAspect.Radius.Get(entity);
                ref var owner = ref _abilityAspect.Owner.Get(entity);
                
                if(!owner.Value.Unpack(_world, out var ownerEntity) ||
                   !_targetAspect.Position.Has(ownerEntity))
                    continue;

                ref var ownerTransform = ref _targetAspect.Position.Get(ownerEntity);
                ref var ownerPosition = ref ownerTransform.Position;
                ref var targets = ref _targetAspect.AbilityTargets.Get(entity);

                var sqrRadius = radius.Value * radius.Value;
                var hasAnyValidTargets = false;
                var amount = targets.Count;

                for (var i = 0; i < amount; i++)
                {
                    ref var packedEntity = ref targets.Entities[i];
                    
                    if(!packedEntity.Unpack(_world, out var targetEntity) || 
                       !_targetAspect.Avatar.Has(targetEntity))
                        continue;

                    ref var targetTransform = ref _targetAspect.Position.Get(targetEntity);
                    ref var position = ref targetTransform.Position;
                    
                    ref var avatar = ref _targetAspect.Avatar.Get(targetEntity);
                    
                    var direction = math.normalize(ownerPosition - position);
                    var closestPoint = position + direction * avatar.Bounds.Radius;
                    var distance = math.distancesq(ownerPosition, closestPoint);
                    
                    if (distance > sqrRadius) continue;
                    
                    hasAnyValidTargets = true;
                    
                    break;
                }

                if(hasAnyValidTargets) continue;
                
                _abilityAspect.Validate.Del(entity);
                _outsidePool.Add(entity);
            }
        }
    }
}