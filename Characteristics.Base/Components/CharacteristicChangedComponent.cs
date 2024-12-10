﻿namespace UniGame.Ecs.Proto.Characteristics.Base.Components
{
    using System;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

    /// <summary>
    /// characteristic changed marker
    /// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CharacteristicChangedComponent
    {
        public float Value;
        public float PreviousValue;
    }
    
    /// <summary>
    /// characteristic changed marker
    /// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CharacteristicChangedComponent<TCharacteristic>
    {
        public float Value;
        public float PreviousValue;
    }
}