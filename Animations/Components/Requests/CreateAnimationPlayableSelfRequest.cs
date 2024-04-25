﻿namespace unigame.ecs.proto.Animations.Components.Requests
{
    using System;
    using Code.Animations;
     
    using UnityEngine.Playables;

    /// <summary>
    /// start animation on target entity
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CreateAnimationPlayableSelfRequest : IProtoAutoReset<CreateAnimationPlayableSelfRequest>
    {
        public ProtoPackedEntity Target;
        public PlayableAsset Animation;
        public ProtoPackedEntity Owner;
        public DirectorWrapMode WrapMode;
        
        //optional
        public PlayableBindingData BindingData;
        public float Duration;
        public float Speed;
        
        public void AutoReset(ref CreateAnimationPlayableSelfRequest c)
        {
            c.Target = default;
            c.Animation = null;
            c.Owner = default;
            c.WrapMode = DirectorWrapMode.None;
            c.BindingData = default;
            c.Duration = 0f;
            c.Speed = 0f;
        }
    }
}