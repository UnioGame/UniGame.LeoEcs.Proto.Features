﻿namespace unigame.ecs.proto.Presets.Systems
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Apply material preset to target system.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class ApplyRenderingSettingsPresetSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _targetFilter;

        private ProtoPool<PresetApplyingDataComponent> _applyingDataPool;
        private ProtoPool<RenderingSettingsPresetComponent> _presetPool;
        private ProtoPool<PresetProgressComponent> _progressPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _targetFilter = _world
                .Filter<RenderingSettingsPresetComponent>()
                .Inc<PresetTargetComponent>()
                .Inc<PresetApplyingComponent>()
                .Inc<PresetApplyingDataComponent>()
                .Inc<PresetProgressComponent>()
                .End();
            
            _progressPool = _world.GetPool<PresetProgressComponent>();
            _applyingDataPool = _world.GetPool<PresetApplyingDataComponent>();
            _presetPool = _world.GetPool<RenderingSettingsPresetComponent>();
        }

        public void Run()
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var applyingDataComponent = ref _applyingDataPool.Get(targetEntity);
                if(!applyingDataComponent.Source.Unpack(_world,out var sourceEntity))
                    continue;

                ref var targetPresetComponent = ref _presetPool.GetOrAddComponent(targetEntity);
                ref var presetComponent = ref _presetPool.GetOrAddComponent(sourceEntity);
                ref var progressComponent = ref _progressPool.GetOrAddComponent(targetEntity);

                var activePreset = targetPresetComponent.Value;
                var sourcePreset = presetComponent.Value;
                
                activePreset.ApplyLerp(activePreset,sourcePreset,progressComponent.Value);
                activePreset.ApplyToRendering();
                //RenderingSettingsPreset.Lerp(activePreset,sourcePreset,progressComponent.Value);
            }
        }
    }
}