﻿namespace UniGame.Ecs.Proto.GameEffects.ShieldEffect.Systems
{
    using Characteristics.Shield.Components;
    using Components;
    using Effects.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    public sealed class ProcessShieldValueEffectSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ShieldEffectComponent>()
                .Inc<EffectComponent>()
                .Exc<DestroyEffectSelfRequest>()
                .End();
        }
        
        public void Run()
        {
            var effectPool = _world.GetPool<EffectComponent>();
            var shieldPool = _world.GetPool<ShieldComponent>();
            var destroyRequestPool = _world.GetPool<DestroyEffectSelfRequest>();

            foreach (var entity in _filter)
            {
                ref var effect = ref effectPool.Get(entity);
                if (!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                if (!shieldPool.Has(destinationEntity))
                {
                    destroyRequestPool.Add(entity);
                }
            }
        }
    }
}