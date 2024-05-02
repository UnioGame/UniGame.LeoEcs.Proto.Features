﻿namespace UniGame.Ecs.Proto.Gameplay.LevelProgress.Components
{
    using System;
    using Leopotam.EcsProto.QoL;

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