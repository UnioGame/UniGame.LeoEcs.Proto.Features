﻿namespace unigame.ecs.proto.Presets.SpotLightSettings.Systems
{
    using unigame.ecs.proto.Presets.Components;
    using Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using System;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
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
    public class ApplySpotLightSettingsPresetSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _targetFilter;

        private ProtoPool<PresetApplyingDataComponent> _applyingDataPool;
        private ProtoPool<SpotLightSettingsPresetComponent> _presetPool;
        private ProtoPool<PresetProgressComponent> _progressPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _targetFilter = _world
                .Filter<SpotLightSettingsPresetComponent>()
                .Inc<PresetTargetComponent>()
                .Inc<PresetApplyingComponent>()
                .Inc<PresetApplyingDataComponent>()
                .Inc<PresetProgressComponent>()
                .End();

            _progressPool = _world.GetPool<PresetProgressComponent>();
            _applyingDataPool = _world.GetPool<PresetApplyingDataComponent>();
            _presetPool = _world.GetPool<SpotLightSettingsPresetComponent>();
        }

        public void Run()
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var applyingDataComponent = ref _applyingDataPool.Get(targetEntity);
                if (!applyingDataComponent.Source.Unpack(_world, out var sourceEntity))
                    continue;
                
                ref var targetPresetComponent = ref _presetPool.GetOrAddComponent(targetEntity);
                ref var presetComponent = ref _presetPool.GetOrAddComponent(sourceEntity);
                ref var progressComponent = ref _progressPool.GetOrAddComponent(targetEntity);

                var activePreset = targetPresetComponent.Value;
                var sourcePreset = presetComponent.Value;

                activePreset.ApplyLerp(activePreset, sourcePreset, progressComponent.Value);
                activePreset.ApplyToSpotLight();
            }
        }
    }
}