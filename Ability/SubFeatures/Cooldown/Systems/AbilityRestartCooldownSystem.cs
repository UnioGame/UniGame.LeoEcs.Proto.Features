﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.Cooldown.Systems
{
    using System;
    using Aspects;
    using Common.Components;
    using LeoEcs.Bootstrap.Runtime.Abstract;
    using LeoEcs.Timer.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AbilityRestartCooldownSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private TimerAspect _timerAspect;
        private AbilityAspect _abilityAspect;

        private ProtoIt _abilityFilter = It
            .Chain<AbilityStartUsingSelfEvent>()
            .Inc<CooldownComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var abilityEntity in _abilityFilter)
            {
                _timerAspect.Restart.GetOrAddComponent(abilityEntity);
                _abilityAspect.Active.Del(abilityEntity);
                GameLog.Log($"Ability {abilityEntity}: cooldown restarted");
            }
        }
    }
}