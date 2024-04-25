﻿namespace unigame.ecs.proto.GameEffects.ModificationEffect.Systems
{
    using Components;
    using Effects.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class ProcessSingleModificationEffectSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        private ProtoPool<EffectComponent> _effectPool;
        private ProtoPool<SingleModificationEffectComponent> _modificationPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<EffectComponent>()
                .Inc<ApplyEffectSelfRequest>()
                .Inc<SingleModificationEffectComponent>()
                .Exc<ModificationEffectComponent>()
                .End();
            
            _effectPool = _world.GetPool<EffectComponent>();
            _modificationPool = _world.GetPool<SingleModificationEffectComponent>();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var modification = ref _modificationPool.Get(entity);
                var modificationHandler = modification.Value;
                
                ref var modificationComponent = ref _world
                    .AddComponent<ModificationEffectComponent>(entity);
                
                modificationComponent.ModificationHandlers.Add(modificationHandler);
            }
        }
    }
}