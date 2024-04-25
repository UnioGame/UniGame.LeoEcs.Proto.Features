﻿namespace unigame.ecs.proto.Animations.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
     
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
    public sealed class HandlePlayOnPlayableSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        
        private AnimationTimelineAspect _animationAspect;
        private PlayableAnimatorAspect _animatorAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<PlayAnimationSelfRequest>()
                .Inc<AnimationPlayableComponent>()
                .Inc<AnimationTargetComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var animatorEntity in _filter)
            {
                ref var targetComponent = ref _animationAspect.Target.GetOrAddComponent(animatorEntity);
                if(!targetComponent.Value.Unpack(_world,out var targetEntity)) continue;
                
                ref var stopRequest = ref _animatorAspect.StopSelf.GetOrAddComponent(targetEntity);
            }
        }
    }
}