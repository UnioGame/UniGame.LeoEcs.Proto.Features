﻿namespace UniGame.Ecs.Proto.GameResources.Systems
{
    using System;
    using Aspects;
    using Components;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.Core.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    [Serializable]
    [ECSDI]
    public class LoadTaskObserverSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;

        private GameResourceAspect _gameResourceAspect;
        private OwnershipAspect _ownershipAspect;

        private ProtoItExc _loadTaskComponent = It
            .Chain<GameResourceLoadTaskComponent>()
            .Exc<PrepareToDeathComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var taskEntity in _loadTaskComponent)
            {
                ref var taskComponent = ref _gameResourceAspect.LoadTask.Get(taskEntity);
                switch (taskComponent.Value.Status)
                {
                    case UniTaskStatus.Canceled:
                        _ownershipAspect.Kill(taskEntity);
                        continue;
                    case UniTaskStatus.Faulted:
                        _ownershipAspect.Kill(taskEntity);
                        continue;
                    case UniTaskStatus.Pending:
                        continue;
                    case UniTaskStatus.Succeeded:
                        ref var instanceSpawnRequest = ref _gameResourceAspect.InstanceSpawnRequest.Add(taskEntity);
                        instanceSpawnRequest.Value = (UnityEngine.Object)taskComponent.Value.AsValueTask().Result.Result;
                        _gameResourceAspect.LoadTask.Del(taskEntity);
                        break;
                }
            }
        }
    }
}