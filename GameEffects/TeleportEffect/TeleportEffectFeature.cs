﻿namespace UniGame.Ecs.Proto.GameEffects.TeleportEffect
{
    using Systems;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Proto Features/Effects/Teleport Effect Feature")]
    public sealed class TeleportEffectFeature : EffectFeatureAsset
    {
        protected override UniTask OnInitializeFeatureAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessTeleportEffectSystem());

            return UniTask.CompletedTask;
        }
    }
}