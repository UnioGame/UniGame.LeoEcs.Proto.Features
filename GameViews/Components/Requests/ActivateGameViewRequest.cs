﻿namespace unigame.ecs.proto.Gameplay.LevelProgress.Components
{
    using System;
     
    using UnityEngine;
    using UnityEngine.Serialization;

    /// <summary>
    /// request to activate view fot target
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ActivateGameViewRequest
    {
        public ProtoPackedEntity Source;
        public string View;
    }
}