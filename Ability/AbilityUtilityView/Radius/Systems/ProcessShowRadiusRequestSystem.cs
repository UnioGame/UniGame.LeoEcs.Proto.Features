﻿namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.Systems
{
    using Component;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using ViewControl.Components;

    public sealed class ProcessShowRadiusRequestSystem : IProtoRunSystem, IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ShowRadiusRequest>().End();
        }

        public void Run()
        {
            var requestPool = _world.GetPool<ShowRadiusRequest>();
            var radiusState = _world.GetPool<RadiusViewStateComponent>();
            var showRequest = _world.GetPool<ShowViewRequest>();
            var hideViewPool = _world.GetPool<HideViewRequest>();

            foreach (var entity in _filter)
            {
                ref var request = ref requestPool.Get(entity);
                if (!request.Source.Unpack(_world, out var sourceEntity))
                    continue;

                ref var state = ref radiusState.GetOrAddComponent(sourceEntity);
                if (state.RadiusViews.TryGetValue(request.Destination, out var view))
                {
                    if (request.Radius == view)
                        continue;

                    var hideRequestEntity = _world.NewEntity();
                    ref var hideViewRequest = ref hideViewPool.Add(hideRequestEntity);

                    hideViewRequest.View = view;
                    hideViewRequest.Destination = request.Destination;
                }

                state.RadiusViews[request.Destination] = request.Radius;

                var showRequestEntity = _world.NewEntity();
                ref var showViewRequest = ref showRequest.Add(showRequestEntity);

                showViewRequest.Root = request.Root;
                showViewRequest.View = request.Radius;
                showViewRequest.Size = request.Size;
                showViewRequest.Destination = request.Destination;
            }
        }
    }
}