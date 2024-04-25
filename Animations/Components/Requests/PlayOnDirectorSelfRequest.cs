﻿namespace unigame.ecs.proto.Animations.Components.Requests
{
    using System;
     

    /// <summary>
    /// play playable on playable director self
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct PlayOnDirectorSelfRequest
    {
        public ProtoPackedEntity Animation;
    }
}