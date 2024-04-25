namespace unigame.ecs.proto.GameAi.MoveToTarget.Systems
{
    using System;
    using Components;
    using Core.Death.Components;
     
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using Unity.Mathematics;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SelectByRangePlannerSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        
        private ProtoPool<TransformPositionComponent> _positionPool;
        private ProtoPool<MoveByRangeComponent> _rangePool;
        private ProtoPool<MoveOutOfRangeComponent> _outOfRangePool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<MoveByRangeComponent>()
                .Inc<MoveToTargetPlannerComponent>()
                .Inc<TransformPositionComponent>()
                .Exc<MoveOutOfRangeComponent>()
                .Exc<DisabledComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var rangeComponent = ref _rangePool.Get(entity);
                ref var transformComponent = ref _positionPool.Get(entity);

                ref var position = ref transformComponent.Position;
                var center = rangeComponent.Center;
                var distance = math.distancesq(position, center);
                var sqrRadius = rangeComponent.Radius * rangeComponent.Radius;
                
                if(distance <= sqrRadius) continue;

                _outOfRangePool.Add(entity);
            }
        }

    }
}
