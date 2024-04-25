﻿namespace unigame.ecs.proto.GameResources.Components
{
    using System;

    /// <summary>
    /// source id of game resource
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct GameResourceIdComponent
    {
        public string Value;
    }
}