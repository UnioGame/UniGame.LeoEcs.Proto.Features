﻿/*namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Area.Systems
{
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using SubFeatures.Area.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public sealed class ShowAreaSystem : IProtoRunSystem, IProtoInitSystem
    {
        private ProtoIt _filter;
        private ProtoWorld _world;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AreaLocalPositionComponent>()
                .Inc<AreaRadiusComponent>()
                .Inc<AreaRadiusViewComponent>()
                .Exc<AreaInstanceComponent>()
                .End();
        }
        
        public void Run()
        {
            var areaViewPool = _world.GetPool<AreaRadiusViewComponent>();
            var areaRadiusPool = _world.GetPool<AreaRadiusComponent>();
            var areaInstancePool = _world.GetPool<AreaInstanceComponent>();

            foreach (var entity in _filter)
            {
                ref var areaRadius = ref areaRadiusPool.Get(entity);
                ref var areaView = ref areaViewPool.Get(entity);

                ref var areaInstance = ref areaInstancePool.Add(entity);

                var size = areaRadius.Value * 2.0f;
                areaInstance.Instance = Object.Instantiate(areaView.View);
                areaInstance.Instance.transform.localScale = new Vector3(size, size, size);
            }
        }
    }
}*/