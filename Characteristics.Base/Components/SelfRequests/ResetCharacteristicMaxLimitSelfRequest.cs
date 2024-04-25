﻿namespace unigame.ecs.proto.Characteristics.Base.Components.Requests.OwnerRequests
{
    using System;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif
    
    /// <summary>
    /// reset characteristic max limit value
    /// </summary>
    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct ResetCharacteristicMaxLimitSelfRequest<T>
        where T : struct
    {
    }
}