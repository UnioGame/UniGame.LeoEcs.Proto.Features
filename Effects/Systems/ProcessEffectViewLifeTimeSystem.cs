﻿namespace unigame.ecs.proto.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// Send destroy request to effect view when lifetime is over.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessEffectViewLifeTimeSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;

        private EffectAspect _effectAspect;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<EffectViewComponent>().End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var view = ref _effectAspect.View.Get(entity);
                if(view.DeadTime > Time.time && !Mathf.Approximately(view.DeadTime, Time.time))
                    continue;

                _effectAspect.DestroyView.TryAdd(entity);
            }
        }
    }
}