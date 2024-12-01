﻿namespace UniGame.Ecs.Proto.Presets.SpotLightSettings.Systems
{
    using UniGame.Ecs.Proto.Presets.Components;
    using Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using System;
    using Aspects;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Presets.Aspects;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    
    /// <summary>
    /// Apply spot light preset in game.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ApplySpotLightSettingsPresetSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private PresetsAspect _presetsAspect;
        private SpotLightSettingsAspect _spotLightSettingsAspect;
        

        private ProtoIt _targetFilter = It
            .Chain<SpotLightSettingsPresetComponent>()
            .Inc<PresetTargetComponent>()
            .Inc<PresetApplyingComponent>()
            .Inc<PresetApplyingDataComponent>()
            .Inc<PresetProgressComponent>()
            .End();
        
        public void Run()
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var applyingDataComponent = ref _presetsAspect.PresetApplyingData.Get(targetEntity);
                if (!applyingDataComponent.Source.Unpack(_world, out var sourceEntity))
                    continue;
                
                ref var targetPresetComponent = ref _spotLightSettingsAspect.SettingsPreset.GetOrAddComponent(targetEntity);
                ref var presetComponent = ref _spotLightSettingsAspect.SettingsPreset.GetOrAddComponent(sourceEntity);
                ref var progressComponent = ref _presetsAspect.PresetProgress.GetOrAddComponent(targetEntity);

                var activePreset = targetPresetComponent.Value;
                var sourcePreset = presetComponent.Value;

                activePreset.ApplyLerp(activePreset, sourcePreset, progressComponent.Value);
                activePreset.ApplyToSpotLight();
            }
        }
    }
}