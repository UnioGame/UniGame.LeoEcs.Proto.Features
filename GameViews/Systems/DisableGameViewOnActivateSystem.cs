﻿namespace unigame.ecs.proto.Gameplay.LevelProgress.Systems
{
    using System;
    using System.Linq;
    using Aspects;
    using Components;
     
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;


#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class DisableGameViewOnActivateSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _activateFilter;
        private ParentGameViewAspect _viewAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _activateFilter = _world
                .Filter<ActivateGameViewRequest>()
                .End();
        }

        public void Run()
        {
            foreach (var requestEntity in _activateFilter)
            {
                ref var activateRequest = ref _viewAspect.Activate.Get(requestEntity);
                //disable active view
                var disableEntity = _world.NewEntity();
                ref var disableRequest = ref _viewAspect.Disable.Add(disableEntity);
                disableRequest.Value = activateRequest.Source;
            }
        }
    }
}