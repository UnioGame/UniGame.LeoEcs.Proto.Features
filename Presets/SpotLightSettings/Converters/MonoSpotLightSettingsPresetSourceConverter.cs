﻿namespace UniGame.Ecs.Proto.Presets.SpotLightSettings.Converters
{
    using Leopotam.EcsProto;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;
    
    public sealed class MonoSpotLightSettingsPresetSourceConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        [InlineProperty]
        [HideLabel]
        public SpotLightSettingsSourceConverter spotLightConverter = new SpotLightSettingsSourceConverter();

        public sealed override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            spotLightConverter.Apply(world, entity);
        }
    }
}