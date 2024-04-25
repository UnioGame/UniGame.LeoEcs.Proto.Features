﻿namespace unigame.ecs.proto.Gameplay.LevelProgress.Components
{
    using System;
     

    /// <summary>
    /// request to disable active view
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct DisableActiveGameViewRequest
    {
        public ProtoPackedEntity Value;
    }
}