﻿namespace unigame.ecs.proto.Ability.Common.Systems
{
    using Aspects;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using unigame.ecs.proto.Characteristics.Attack.Components;
     
    using Tools;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Add an empty target to an ability
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    public class ApplyAbilityRadiusSystem : IProtoInitSystem, IProtoRunSystem
    {
        private AbilityTools _abilityTools;
        private ProtoWorld _world;
        private EcsFilter _radiusRequestFilter;
        private EcsFilter _effectFilter;

        private AbilityAspect _abilityAspect;

        public ApplyAbilityRadiusSystem(AbilityTools abilityTools)
        {
            _abilityTools = abilityTools;
        }
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _radiusRequestFilter = _world
                .Filter<ApplyAbilityRadiusRangeRequest>()
                .End();
        }

        public void Run()
        {
            foreach (var requestEntity in _radiusRequestFilter)
            {
                ref var request = ref _abilityAspect.ApplyRadiusRange.Get(requestEntity);
                
                if(!request.Target.Unpack(_world,out var targetEntity)) continue;
                
                var abilityEntity = _abilityTools.TryGetAbility(targetEntity, request.AbilitySlot);
                if(abilityEntity < 0) continue;
                
                if(!_abilityAspect.Radius.Has(abilityEntity)) continue;

                ref var abilityRadiusComponent = ref _abilityAspect.Radius.Get(abilityEntity);
                abilityRadiusComponent.Value = request.Value;

                _abilityAspect.ApplyRadiusRange.Del(requestEntity);
            }
        }
    }
}