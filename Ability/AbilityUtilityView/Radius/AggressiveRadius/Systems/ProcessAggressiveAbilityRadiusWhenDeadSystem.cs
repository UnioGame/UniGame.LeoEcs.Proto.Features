﻿namespace UniGame.Ecs.Proto.Ability
{
    using AbilityUtilityView.Components;
    using AbilityUtilityView.Radius.AggressiveRadius.Components;
    using AbilityUtilityView.Radius.Component;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;


    public sealed class ProcessAggressiveAbilityRadiusWhenDeadSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<DestroyComponent>()
                .Inc<VisibleUtilityViewComponent>()
                .Inc<AggressiveRadiusViewStateComponent>()
                .End();
        }

        public void Run()
        {
            var statePool = _world.GetPool<AggressiveRadiusViewStateComponent>();
            var hideRadiusPool = _world.GetPool<HideRadiusRequest>();

            foreach (var entity in _filter)
            {
                ref var state = ref statePool.Get(entity);
                foreach (var packedEntity in state.Entities)
                {
                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref hideRadiusPool.Add(hideRequestEntity);

                    hideRequest.Source = entity.PackEntity(_world);
                    hideRequest.Destination = packedEntity;
                }
            }
        }
    }
}