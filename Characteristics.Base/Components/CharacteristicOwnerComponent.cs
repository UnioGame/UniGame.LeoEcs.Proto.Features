﻿namespace unigame.ecs.proto.Characteristics.Base.Components
{
    using System;
    using Leopotam.EcsProto.QoL;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

    /// <summary>
    /// link to characteristics owner container
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct CharacteristicOwnerComponent<TCharacteristic>
        where TCharacteristic : struct
    {
        public ProtoPackedEntity Link;
    }
}