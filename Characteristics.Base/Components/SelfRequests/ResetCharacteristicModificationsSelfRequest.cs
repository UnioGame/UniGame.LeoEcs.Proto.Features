﻿namespace UniGame.Ecs.Proto.Characteristics.Base
{
    using System;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif
    
    /// <summary>
    /// reset characteristic value
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct ResetCharacteristicModificationsSelfRequest<T>
    {
    }
}