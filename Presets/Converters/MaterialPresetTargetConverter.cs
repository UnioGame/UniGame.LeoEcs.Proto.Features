﻿namespace UniGame.Ecs.Proto.Presets.Converters
{
    using System;
    using System.Linq;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public sealed class MaterialPresetTargetConverter : LeoEcsConverter
    {
        public string targetId;
        public MeshRenderer renderer;
        
        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var dataComponent = ref world.GetOrAddComponent<MaterialPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var presetTargetComponent = ref world.GetOrAddComponent<PresetTargetComponent>(entity);
            
            var material = renderer.materials.FirstOrDefault();
            dataComponent.Value = material;
            idComponent.Value = targetId.GetHashCode();
        }
    }
}