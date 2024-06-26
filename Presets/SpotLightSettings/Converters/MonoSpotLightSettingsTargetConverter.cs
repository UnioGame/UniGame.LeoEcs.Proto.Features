﻿namespace UniGame.Ecs.Proto.Presets.SpotLightSettings.Converters
{
    using System;
    using Abstract;
    using Assets;
    using UniGame.Ecs.Proto.Presets.Components;
    using Components;
    using Leopotam.EcsProto;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Converters;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [ExecuteInEditMode]
    public sealed class MonoSpotLightSettingsTargetConverter : MonoLeoEcsConverter<SpotLightSettingsTargetConverter>
    {
        
    }

    [Serializable]
    public sealed class SpotLightSettingsTargetConverter : LeoEcsConverter, IPresetAction
    {
        [ShowIf(nameof(IsEnabled))]
        public string id = nameof(SpotLightSettingsPresetComponent);
        [ShowIf(nameof(IsEnabled))]
        public bool showButtons;

        [ShowIf(nameof(IsEnabled))]
        [HideLabel]
        public SpotLightPresets sourcePreset;

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var sourceComponent = ref world.GetOrAddComponent<SpotLightSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var presetTargetComponent = ref world.GetOrAddComponent<PresetTargetComponent>(entity);

            idComponent.Value = id.GetHashCode();
            sourceComponent.Value = sourcePreset;
        }

        [Button]
        [ShowIf("@this.showButtons && this.IsEnabled")]
        public void Bake()
        {
            sourcePreset.BakeSpotLight();
        }

        [Button]
        [ShowIf("@this.showButtons && this.IsEnabled")]
        public void ApplyToTarget()
        {
            sourcePreset.ApplyToSpotLight();
        }
    }
}