﻿namespace UniGame.Ecs.Proto.Presets.DirectionalLight.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class DirectionalLightAspect : EcsAspect
    {
        public ProtoPool<DirectionalLightSettingsPresetComponent> LightSettingsPreset;
    }
}