﻿namespace Game.Modules.SequenceActions.Components.Requests
{
    using System;
    using System.Collections.Generic;
    using Leopotam.EcsLite;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct StartSequenceActionRequest : IEcsAutoReset<StartSequenceActionRequest>
    {
        public List<SequenceActionData> Actions;
        
        public void AutoReset(ref StartSequenceActionRequest c)
        {
            Actions ??= new List<SequenceActionData>();
            Actions.Clear();
        }
    }
    
    
}