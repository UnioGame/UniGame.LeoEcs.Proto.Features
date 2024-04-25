﻿namespace unigame.ecs.proto.Animations.Aspects
{
    using System;
    using Characteristics.Duration.Components;
    using Components;
    using Components.Requests;
    using Core.Components;
     
    using UniGame.LeoEcs.Timer.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class AnimationTimelineAspect : EcsAspect
    {
        //animation duration
        public ProtoPool<DurationComponent> Duration;
        public ProtoPool<OwnerComponent> Owner;
        //playable director source entity
        public ProtoPool<AnimationTargetComponent> Target;
        //playable data
        public ProtoPool<AnimationPlayableComponent> Animation;
        
        public ProtoPool<AnimationWrapModeComponent> WrapMode;
        public ProtoPool<AnimationPaybackSpeedComponent> Speed;
        public ProtoPool<AnimationStartTimeComponent> StartTime;
        
        //optional
        public ProtoPool<AnimationBindingDataComponent> Binding;
        //animation execution cooldown
        public ProtoPool<CooldownComponent> Cooldown;
        public ProtoPool<CooldownStateComponent> CooldownState;
        public ProtoPool<AnimationLinkComponent> Link;
        
        //animation is active and can be played
        public ProtoPool<AnimationReadyComponent> Ready;
        //animation current playing
        public ProtoPool<AnimationPlayingComponent> Playing;
        //kill animation entity on complete
        public ProtoPool<AnimationKillOnComplete> KillOnComplete;
        
        public ProtoPool<AnimationCompleteComponent> Complete;
        
        public ProtoPool<CreateAnimationLinkSelfRequest> CreateLinkSelfAnimation;
        public ProtoPool<CreateAnimationPlayableSelfRequest> CreateSelfAnimation;
        
        //start exists animation entity
        public ProtoPool<PlayAnimationSelfRequest> PlaySelf;
        //stop animation
        public ProtoPool<StopAnimationSelfRequest> StopSelf;
    }
}