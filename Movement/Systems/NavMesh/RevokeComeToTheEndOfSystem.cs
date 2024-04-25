﻿namespace unigame.ecs.proto.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
     
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class RevokeComeToTheEndOfSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        private NavigationAspect _navigationAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world.Filter<ComePointComponent>()
                .Inc<RevokeComeToEndOfRequest>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                _navigationAspect.ComePoint.Del(entity);
                
                if(_navigationAspect.MovementTargetPoint.Has(entity))
                    _navigationAspect.MovementTargetPoint.Del(entity);
                
                if (!_navigationAspect.NavMeshAgentStop.Has(entity))
                    _navigationAspect.NavMeshAgentStop.Add(entity);
            }
        }
    }
}