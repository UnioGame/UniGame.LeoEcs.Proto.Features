﻿namespace UniGame.Ecs.Proto.TargetSelection.Systems
{
    using System;
    using Aspects;
    using Characteristics.Health;
    using Components;
    using DataStructures.ViliWonka.KDTree;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Selection;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CollectKDTreeTargetsSystem : IProtoInitSystem, IProtoRunSystem
    {
        private static float3 MaxPosition = new float3(float.MaxValue,float.MaxValue,float.MaxValue);
        private static ProtoPackedEntity EmptyEntity = default;
        private static int EmptyInt = default;
        
        private ProtoWorld _world;
        private EcsFilter _targetFilter;
        private EcsFilter _kdDataFilter;
        private TargetSelectionSystem _targetSelection;
        
        private TargetAspect _targetAspect;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _targetSelection = _world.GetGlobal<TargetSelectionSystem>();
            
            _targetFilter = _world
                .Filter<TransformPositionComponent>()
                .Inc<HealthComponent>()
                .Exc<DestroyComponent>()
                .Exc<DisabledComponent>()
                .Exc<PrepareToDeathComponent>()
                .End();

            _kdDataFilter = _world
                .Filter<KDTreeDataComponent>()
                .Inc<KDTreeComponent>()
                .Inc<KDTreeQueryComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var dataEntity in _kdDataFilter)
            {
                ref var dataComponent = ref _targetAspect.Data.Get(dataEntity);
                ref var treeComponent = ref _targetAspect.Tree.Get(dataEntity);

                var data = dataComponent.Values;
                var packedEntities = dataComponent.PackedEntities;
                var kdTree = treeComponent.Value;
                var points = kdTree.points;
                
                var counter = 0;
                
                foreach (var targetEntity in _targetFilter)
                {
                    if(counter >= TargetSelectionData.MaxAgents) break;
                    
                    ref var transformComponent = ref _targetAspect.Position.Get(targetEntity);
                    var position = transformComponent.Position;
                    
                    data[counter] = position;
                    packedEntities[counter] = _world.PackEntity(targetEntity);

                    counter++;
                }
                
                for (var i = counter; i < TargetSelectionData.MaxAgents; i++)
                {
                    data[i] = MaxPosition;
                    packedEntities[i] = EmptyEntity;
                }

                dataComponent.Count = counter;
                
                for (var i = 0; i < counter; i++)   
                    points[i] = data[i];
                
                kdTree.SetCount(counter);
                kdTree.Rebuild();

                if (_targetAspect.Query.Has(dataEntity)) continue;
                
                ref var radiusQueryComponent = ref _targetAspect.Query.Add(dataEntity);
                radiusQueryComponent.Value = new KDQuery();
            }
        }
    }
}