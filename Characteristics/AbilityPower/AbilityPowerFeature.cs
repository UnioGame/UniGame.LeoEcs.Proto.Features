﻿namespace UniGame.Ecs.Proto.Characteristics.AbilityPower
{
    using System;
    using Base;
    using Base.Aspects;
    using Components;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsProto;
    using Systems;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
    /// <summary>
    /// provides a feature to increase the damage of abilities,
    /// allows you to change the strength of abilities by AbilityPowerComponent
    /// </summary>
    [CreateAssetMenu(menuName = "Proto Features/Characteristics/AbilityPower Feature")]
    public sealed class AbilityPowerFeature : CharacteristicFeature<AbilityPowerEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class AbilityPowerEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<AbilityPowerComponent>();
            // update ability power value
            ecsSystems.Add(new RecalculateAbilityPowerSystem());

            return UniTask.CompletedTask;
        }
    }
    
    [Serializable]
    public sealed class AbilityPowerCharacteristicAspect : GameCharacteristicAspect<AbilityPowerComponent>
    {
        //requests
        public ProtoPool<RecalculateAbilityPowerRequest> RecalculateAbilityPowerRequest;
    }
    
}