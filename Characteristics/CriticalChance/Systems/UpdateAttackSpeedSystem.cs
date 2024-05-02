﻿namespace UniGame.Ecs.Proto.Characteristics.CriticalChance.Systems
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.Ecs.Proto.Characteristics.Base.Components;
    using UniGame.LeoEcs.Shared.Extensions;


    /// <summary>
    /// update value of attack speed characteristic
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public sealed class UpdateCriticalChanceChangedSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        
        private ProtoPool<CharacteristicComponent<CriticalChanceComponent>> _characteristicPool;
        private ProtoPool<CriticalChanceComponent> _valuePool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CharacteristicChangedComponent<CriticalChanceComponent>>()
                .Inc<CharacteristicComponent<CriticalChanceComponent>>()
                .Inc<CriticalChanceComponent>()
                .End();

            _characteristicPool = _world.GetPool<CharacteristicComponent<CriticalChanceComponent>>();
            _valuePool = _world.GetPool<CriticalChanceComponent>();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var characteristicComponent = ref _characteristicPool.Get(entity);
                ref var valueComponent = ref _valuePool.Get(entity);
                valueComponent.Value = characteristicComponent.Value;
            }
        }
    }
}