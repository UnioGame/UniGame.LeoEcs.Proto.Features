﻿namespace UniGame.Ecs.Proto.TargetSelection
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Selection;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Proto Features/Target/Target Selection", fileName = "Target Selection Feature")]
    public class TargetSelectionFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            
            var targetSelectionSystem = new TargetSelectionSystem();
            world.SetGlobal(targetSelectionSystem);
            
            ecsSystems.Add(targetSelectionSystem);
            //collect all valida target into targets component
            ecsSystems.Add(new InitKDTreeTargetsSystem());
            ecsSystems.Add(new CollectKDTreeTargetsSystem());
            ecsSystems.Add(new SelectAreaTargetsSystem());
            return UniTask.CompletedTask;
        }
    }
}