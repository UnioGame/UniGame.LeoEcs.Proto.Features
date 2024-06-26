﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.ComeToTarget
{
    using System;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Systems;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UserInput.Systems;

    [Serializable]
    [CreateAssetMenu(menuName = "Proto Features/Ability/ComeToTarget SubFeature",fileName = "ComeToTarget SubFeature")]
    public sealed class ComeToTargetSubFeature : AbilitySubFeature
    {
        public override UniTask<IProtoSystems> OnUtilitySystems(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new ComeToTargetByUserInputSystem());
            ecsSystems.Add(new UpdateComePointFromTargetSystem());
            ecsSystems.Add(new RevokeUpdateComePointByMovementSystem());
            ecsSystems.Add(new RevokeUpdateComePointByAbilityInHandSystem());
            ecsSystems.Add(new ApplyDeferredAbilitySystem());
            return UniTask.FromResult(ecsSystems);
        }
    }
}