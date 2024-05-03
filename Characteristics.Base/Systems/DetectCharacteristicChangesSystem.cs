﻿namespace UniGame.Ecs.Proto.Characteristics.Base.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Events;
    using Game.Ecs.Core.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;


    /// <summary>
    /// detect characteristic changes and create event for it
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class DetectCharacteristicChangesSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private CharacteristicsAspect _characteristicsAspect;
        private ModificationsAspect _modificationsAspect;
        
        private ProtoIt _changeRequestFilter= It
            .Chain<CharacteristicChangedComponent>()
            .Inc<CharacteristicValueComponent>()
            .Inc<MinValueComponent>()
            .Inc<MaxValueComponent>()
            .Inc<CharacteristicBaseValueComponent>()
            .End();

        public void Run()
        {
            foreach (var changesEntity in _changeRequestFilter)
            {
                ref var changedComponent = ref _characteristicsAspect.Changed.Get(changesEntity);
                ref var previousValue = ref _characteristicsAspect.PreviousValue.Get(changesEntity);
                ref var ownerComponent = ref _characteristicsAspect.Owner.Get(changesEntity);
                
                var eventEntity = _world.NewEntity();
                ref var eventComponent = ref _characteristicsAspect.OnValueChanged.Add(eventEntity);
                eventComponent.Owner = ownerComponent.Value;
                eventComponent.Value = changedComponent.Value;
                eventComponent.PreviousValue = previousValue.Value;
                eventComponent.Characteristic = _world.PackEntity(changesEntity);
            }
        }
    }
}