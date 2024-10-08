﻿namespace UniGame.Ecs.Proto.GameResources.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CompleteGameResourceObjectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private GameResourceAspect _resourceAspect;
        
        private ProtoItExc _filter = It
            .Chain<UnityObjectComponent>()
            .Inc<GameSpawnedResourceComponent>()
            .Exc<GameSpawnCompleteComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var resourceComponent = ref _resourceAspect.Resource.Get(entity);
                ref var objectComponent = ref _resourceAspect.Object.Get(entity);
                ref var spawnedResourceComponent = ref _resourceAspect.SpawnedResource.Get(entity);
                ref var completeComponent = ref _resourceAspect.Complete.Add(entity);

                var eventEntity = _world.NewEntity();
                ref var eventComponent = ref _resourceAspect.SpawnComplete.Add(eventEntity);
                
                eventComponent.SpawnedEntity = entity.PackEntity(_world);
                eventComponent.Resource = objectComponent.Value;
                eventComponent.Source = spawnedResourceComponent.Source;
                eventComponent.ResourceId = resourceComponent.Value;
            }
        }
    }
}