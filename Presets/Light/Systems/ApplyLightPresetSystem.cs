﻿namespace unigame.ecs.proto.Presets.Systems
{
    using System;
    using Components;
    using Game.Modules.UnioModules.UniGame.LeoEcsLite.LeoEcs.Shared.Components;
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
    public class ApplyLightPresetSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _targetFilter;

        private ProtoPool<PresetApplyingDataComponent> _applyingDataPool;
        private ProtoPool<LightPresetComponent> _presetPool;
        private ProtoPool<PresetProgressComponent> _progressPool;
        private ProtoPool<LightComponent> _lightPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _targetFilter = _world
                .Filter<LightPresetComponent>()
                .Inc<PresetTargetComponent>()
                .Inc<LightComponent>()
                .Inc<PresetApplyingComponent>()
                .Inc<PresetApplyingDataComponent>()
                .Inc<PresetProgressComponent>()
                .End();
            
            _progressPool = _world.GetPool<PresetProgressComponent>();
            _lightPool = _world.GetPool<LightComponent>();
            _applyingDataPool = _world.GetPool<PresetApplyingDataComponent>();
            _presetPool = _world.GetPool<LightPresetComponent>();
        }

        public void Run()
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var applyingDataComponent = ref _applyingDataPool.Get(targetEntity);
                if(!applyingDataComponent.Source.Unpack(_world,out var sourceEntity))
                    continue;

                ref var targetPresetComponent = ref _presetPool.GetOrAddComponent(targetEntity);
                ref var lightComponent = ref _lightPool.GetOrAddComponent(targetEntity);
                ref var presetComponent = ref _presetPool.GetOrAddComponent(sourceEntity);
                ref var progressComponent = ref _progressPool.GetOrAddComponent(targetEntity);

                ref var activePreset = ref targetPresetComponent.Value;
                ref var sourcePreset = ref presetComponent.Value;

                var light = lightComponent.Value;
                
                LightPresetTools.Lerp(light,ref activePreset,ref sourcePreset,progressComponent.Value);
            }
        }
    }
}