﻿namespace UniGame.Ecs.Proto.Movement.Systems.Converters
{
    using System;
    using Aspect;
    using Components;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;

    /// <summary>
    /// Система отвечающая за конвертацию вектора скорости в следующую позицию для перемещения через систему NavMesh.
    /// </summary>
    #if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class VelocityNavMeshTargetConvertSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        private NavMeshAspect _navigationAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<VelocityComponent>()
                .Inc<TransformPositionComponent>()
                .Inc<StepMovementComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var velocityComponent = ref _navigationAspect.Velocity.Get(entity);
                ref var transformComponent = ref _navigationAspect.Position.Get(entity);
                ref var stepMovementComponent = ref _navigationAspect.StepMovement.Get(entity);
                
                ref var targetComponent = ref _navigationAspect.MovementTargetPoint
                    .GetOrAddComponent(entity);

                var currentPosition = transformComponent.Position;
                float3 direction = velocityComponent.Value.normalized * stepMovementComponent.Value;
                var nextPosition = currentPosition + direction;
                
                targetComponent.DestinationPosition = nextPosition;
            }
        }
    }
}