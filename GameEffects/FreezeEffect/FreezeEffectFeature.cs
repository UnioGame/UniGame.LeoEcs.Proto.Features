﻿namespace UniGame.Ecs.Proto.GameEffects.FreezeEffect
{
    using Components;
    using Cysharp.Threading.Tasks;
    using Effects.Feature;
     
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Effects/Freeze Effect Feature")]
    public class FreezeEffectFeature : EffectFeatureAsset
    {
        protected override UniTask OnInitializeFeatureAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new CreateFreezeEffectSystem());
            ecsSystems.Add(new ApplyFreezeEffectSystem());
            ecsSystems.DelHere<ApplyFreezeTargetEffectRequest>();
            ecsSystems.Add(new ProcessFreezeEffectSystem());
            ecsSystems.Add(new RemoveFreezeEffectSystem());
            ecsSystems.DelHere<RemoveFreezeTargetEffectRequest>();
            
            return UniTask.CompletedTask;
        }
    }
}