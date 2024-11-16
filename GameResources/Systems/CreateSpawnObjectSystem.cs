﻿namespace UniGame.Ecs.Proto.GameResources.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using LeoEcs.Bootstrap.Runtime.Abstract;
    using LeoEcs.Converter.Runtime;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using Runtime.ObjectPool.Extensions;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    [Serializable]
    [ECSDI]
    public class CreateSpawnObjectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;

        private GameResourceAspect _resourceAspect;
        private OwnershipAspect _ownershipAspect;
        private UnityAspect _unityAspect;

        private ProtoIt _spawnRequestFilter = It
            .Chain<GameResourceSpawnComponent>()
            .Inc<ResourceInstanceSpawnRequest>()
            .End();

        public void Run()
        {
            foreach (var requestEntity in _spawnRequestFilter)
            {
                ref var requestComponent = ref _resourceAspect.InstanceSpawnRequest.Get(requestEntity);
                ref var resourceSpawnComponent = ref _resourceAspect.SpawnResource.Get(requestEntity);

                var resourceInstance = requestComponent.Value.Spawn();

                var instanceGameObject = (GameObject)resourceInstance;
                if (resourceSpawnComponent.Parent)
                {
                    instanceGameObject.transform.SetParent(resourceSpawnComponent.Parent);
                }

                instanceGameObject.transform.position = resourceSpawnComponent.LocationData.Position;
                instanceGameObject.transform.rotation = resourceSpawnComponent.LocationData.Rotation;
                instanceGameObject.transform.localScale = resourceSpawnComponent.LocationData.Scale;
                
                ref var gameObjectComponent = ref _unityAspect.GameObject.Add(requestEntity);
                gameObjectComponent.Value = instanceGameObject;
                ref var transformComponent = ref _unityAspect.Transform.Add(requestEntity);
                transformComponent.Value = instanceGameObject.transform;
                
                if (instanceGameObject.TryGetComponent<ILeoEcsMonoConverter>(out var converter))
                {
                    converter.Convert(_world, requestEntity);
                }
                
                _resourceAspect.SpawnResource.Del(requestEntity);
            }
        }
    }
}