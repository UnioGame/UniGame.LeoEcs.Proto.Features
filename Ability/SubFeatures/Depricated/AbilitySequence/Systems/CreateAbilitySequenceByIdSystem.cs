﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.AbilitySequence.Systems
{
    using System;
    using Ability.Aspects;
    using Ability.Tools;
    using Aspects;
    using AbilitySequence;
    using Game.Code.Configuration.Runtime.Ability.Description;
    using Game.Code.Services.AbilityLoadout.Data;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
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
    public class CreateAbilitySequenceByIdSystem : IProtoRunSystem
    {
        private AbilityAspect _tools;
        private ProtoWorld _world;
        
        private AbilitySequenceAspect _aspect;

        private ProtoItExc _createRequestFilter= It
            .Chain<CreateAbilitySequenceByIdSelfRequest>()
            .Exc<CreateAbilitySequenceSelfRequest>()
            .End();

        public void Run()
        {
            foreach (var requestEntity in _createRequestFilter)
            {
                ref var requestComponent = ref _aspect.CreateById.Get(requestEntity);

                if (requestComponent.Abilities.Count <= 0)
                {
                    _aspect.CreateById.Del(requestEntity);
                    continue;
                }

                ref var createSequenceRequest = ref _aspect
                    .Create
                    .Add(requestEntity);

                createSequenceRequest.Owner = requestComponent.Owner;
                createSequenceRequest.Name = requestComponent.Name;
                
                foreach (AbilityId abilityId in requestComponent.Abilities)
                {
                    var abilityEntity = _tools.EquipAbilityById(ref requestComponent.Owner, abilityId, AbilitySlotId.EmptyAbilitySlot);
                    createSequenceRequest.Abilities.Add(abilityEntity);
                }
            }
        }
    }
}