﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.AbilitySequence.Systems
{
    using System;
    using Ability.Tools;
    using Aspects;
    using AbilitySequence;
    using Game.Code.Services.AbilityLoadout.Data;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.AddressableTools.Runtime;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// create ability sequence
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CreateAbilitySequenceByReferenceSystem : IProtoInitSystem, IProtoRunSystem
    {
        private AbilityTools _abilityTools;
        private ProtoWorld _world;
        private EcsFilter _createRequestFilter;
        private ILifeTime _worldLifeTime;
        
        private AbilitySequenceAspect _aspect;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _worldLifeTime = _world.GetWorldLifeTime();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            
            _createRequestFilter = _world
                .Filter<CreateAbilitySequenceReferenceSelfRequest>()
                .Exc<CreateAbilitySequenceSelfRequest>()
                .End();
        }

        public void Run()
        {
            foreach (var requestEntity in _createRequestFilter)
            {
                ref var requestComponent = ref _aspect.CreateByReference.Get(requestEntity);
                var reference = requestComponent.Reference;
                var sequence = reference.sequence;
                
                if (sequence.Count <= 0) continue;

                ref var createSequenceRequest = ref _aspect
                    .Create
                    .Add(requestEntity);

                createSequenceRequest.Owner = requestComponent.Owner;
                createSequenceRequest.Name = reference.name;
                
                foreach (var configurationValue in sequence)
                {
                    //load configuration sync
                    var configuration = configurationValue
                        .reference
                        .LoadAssetInstanceForCompletion(_worldLifeTime, true);
                    
                    var abilityEntity = _abilityTools
                        .EquipAbilityByReference(ref requestComponent.Owner, configuration, AbilitySlotId.EmptyAbilitySlot);
                    
                    createSequenceRequest.Abilities.Add(abilityEntity);
                }
                
                _aspect.CreateById.Del(requestEntity);
            }
        }
    }
}