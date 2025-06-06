﻿namespace UniGame.Ecs.Proto.Animations.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
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
    public sealed class CompletePlayableAnimatorSystem : IProtoRunSystem, IProtoInitSystem
    {
        private ProtoIt _filter;
        private ProtoWorld _world;

        private AnimationTimelineAspect _animationAspect;
        private PlayableAnimatorAspect _animatorAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<StopAnimationSelfRequest>()
                .Inc<AnimatorPlayingComponent>()
                .Inc<PlayableDirectorComponent>()
                .End();
        }

        public void Run()
        {
            foreach (var animatorEntity in _filter)
            {
                ref var playingTargetComponent = ref _animatorAspect.PlayingTarget.Get(animatorEntity);
                ref var directorComponent = ref _animatorAspect.Director.Get(animatorEntity);

                if (playingTargetComponent.Value.Unpack(_world, out var animationEntity))
                {
                    _animationAspect.StopSelf.GetOrAddComponent(animationEntity);
                }

                var playableDirector = directorComponent.Value;
                if (playableDirector == null) continue;

                var playableAsset = playableDirector.playableAsset;
                var currentTime = playableDirector.time;

                if (playableAsset != null && currentTime < playableAsset.duration)
                {
                    playableDirector.time = playableAsset.duration;
                    //playableDirector.Evaluate();
                }

                playableDirector.Stop();
                playableDirector.playableAsset = null;

                _animatorAspect.PlayingTarget.Del(animatorEntity);
            }
        }
    }
}