﻿namespace UniGame.Ecs.Proto.GameResources.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Ecs.Core.Components;
    using LeoEcs.Bootstrap.Runtime.Abstract;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Runtime.ObjectPool.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class PoolObjectSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private UnityAspect _unityAspect;

        private ProtoIt _poolableFilter = It
            .Chain<PoolableComponent>()
            .Inc<GameObjectComponent>()
            .Inc<PrepareToDeathComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var poolableEntity in _poolableFilter)
            {
                ref var gameObjectComponent = ref _unityAspect.GameObject.Get(poolableEntity);
                gameObjectComponent.Value.Despawn();
            }
        }
    }
}