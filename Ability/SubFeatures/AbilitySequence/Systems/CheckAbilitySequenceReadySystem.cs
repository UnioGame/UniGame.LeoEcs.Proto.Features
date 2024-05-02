﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.AbilitySequence.Systems
{
    using System;
    using Ability.Tools;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// is sequence ability unequipped when try to equip it
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CheckAbilitySequenceReadySystem : IProtoInitSystem, IProtoRunSystem
    {
        private AbilityTools _tools;
        private ProtoWorld _world;
        private EcsFilter _abilityFilter;

        private AbilitySequenceAspect _sequence;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _tools = _world.GetGlobal<AbilityTools>();
            
            _abilityFilter = _world
                .Filter<AbilitySequenceComponent>()
                .Inc<AbilitySequenceAwaitComponent>()
                .End();
        }

        public void Run()
        {
            foreach (var entity in _abilityFilter)
            {
                ref var dataComponent = ref _sequence.Sequence.Get(entity);
                ref var ownerComponent = ref _sequence.Owner.Get(entity);
                var isAllAbilitiesActive = true;
                
                foreach (var abilityEntity in dataComponent.Abilities)
                {
                    var active = _tools.IsActiveAbilityEntity(ref ownerComponent.Value, abilityEntity) > 0;
                    if(active) continue;
                    isAllAbilitiesActive = false;
                    break;
                }
                
                if(isAllAbilitiesActive) continue;
                
                _sequence.Await.Del(entity);
                _sequence.Ready.Add(entity);
            }
        }
    }
}