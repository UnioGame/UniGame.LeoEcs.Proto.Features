﻿namespace UniGame.Ecs.Proto.Ability.UserInput.Systems
{
    using Common.Components;
    using Game.Ecs.Input.Components;
    using Game.Ecs.Input.Components.Requests;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public sealed class ClearActiveTimeSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AbilityMapComponent>()
                .Inc<UserInputTargetComponent>()
                .Exc<AbilityUpInputRequest>()
                .End();
        }
        
        public void Run()
        {
            var activateTimePool = _world.GetPool<AbilityActiveTimeComponent>();
            var abilityMapPool = _world.GetPool<AbilityMapComponent>();
            
            foreach (var entity in _filter)
            {
                ref var abilityMap = ref abilityMapPool.Get(entity);
                foreach (var packedEntity in abilityMap.Abilities)
                {
                    if(!packedEntity.Unpack(_world, out var abilityEntity) || !activateTimePool.Has(abilityEntity))
                        continue;

                    ref var activateTime = ref activateTimePool.Get(abilityEntity);
                    activateTime.Time = 0.0f;
                }
            }
        }
    }
}