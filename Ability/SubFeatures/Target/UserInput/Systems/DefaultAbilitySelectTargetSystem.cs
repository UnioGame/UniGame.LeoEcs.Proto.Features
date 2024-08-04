﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.Target.UserInput.Systems
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Common.Components;
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.TargetSelection;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Selection.Components;
    using TargetSelection;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class DefaultAbilitySelectTargetSystem : IProtoRunSystem
    {
        private ProtoWorld _world;

        private TargetAbilityAspect _targetAspect;
        
        private EntityFloat[] _distances = new EntityFloat[TargetSelectionData.MaxTargets];
        private ProtoEntity[] _unpackedEntities  = new ProtoEntity[TargetSelectionData.MaxTargets];
        private ProtoPackedEntity[] _packedEntities  = new ProtoPackedEntity[TargetSelectionData.MaxTargets];
        private EntityFloatComparer _comparer = new();
        private SortedDictionary<float,int> _sortedDistances = new();
        
        private ProtoItExc _filter= It
            .Chain<TargetableAbilityComponent>()
            .Inc<AbilityInHandComponent>()
            .Inc<DefaultAbilityComponent>()
            .Inc<OwnerComponent>()
            .Inc<SelectedTargetsComponent>()
            .Exc<PrepareToDeathComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _targetAspect.Owner.Get(entity);
                
                if (!owner.Value.Unpack(_world, out var ownerEntity)) continue;
                
                ref var targets = ref _targetAspect.SelectedTargets.Get(entity);
                ref var chosenTarget = ref _targetAspect.AbilityTargets.Get(entity);

                if (_targetAspect.Disabled.Has(ownerEntity) || 
                    _targetAspect.PrepareToDeath.Has(ownerEntity))
                {
                    chosenTarget.SetEmpty();
                    continue;
                }

                ref var positionComponent = ref _targetAspect.Position.Get(ownerEntity);
                var sourcePosition = positionComponent.Position;

                var amount = _world.UnpackAll(targets.Entities, _unpackedEntities, targets.Count);
                if (amount <= 0) continue;

                _sortedDistances.Clear();
                
                var counter = 0;
                for (var i = 0; i < amount; i++)
                {
                    var targetEntity = _unpackedEntities[i];
                    ref var transformComponent = ref _targetAspect.Position.Get(targetEntity);

                    var toPosition = transformComponent.Position;
                    var sqrDistance = Vector3.SqrMagnitude(sourcePosition - toPosition);
                    _distances[counter] = new EntityFloat(targetEntity, sqrDistance);
                    counter++;
                }

                Array.Sort(_distances,_comparer);

                for (var i = 0; i < counter; i++)
                {
                    var distance = _distances[i];
                    _packedEntities[i] = _world.PackEntity(distance.entity);
                }

                chosenTarget.SetEntities(_packedEntities,counter);
            }

        }

    }

}