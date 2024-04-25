﻿namespace unigame.ecs.proto.Animations.Data
{
    using System;
    using System.Collections.Generic;
     
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UnityEngine;
    using UnityEngine.Playables;
    using Object = UnityEngine.Object;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AnimationToolSystem : IProtoInitSystem
    {
        private ProtoWorld _world;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

    }
}