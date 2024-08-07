﻿namespace UniGame.Ecs.Proto.Gameplay.Dodge.Events
{
    using System;
    using Leopotam.EcsProto.QoL;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// attack miss event
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MissedEvent
    {
        public ProtoPackedEntity Source;
        public ProtoPackedEntity Destination;
    }
}