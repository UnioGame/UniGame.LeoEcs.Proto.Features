namespace unigame.ecs.proto.AI.Systems
{
    using System;
    using Components;
    using Abstract;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Service;
     
    using Tools;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public abstract class BasePlannerSystem<TComponent>: 
        IAiPlannerSystem,IProtoRunSystem
        where TComponent : struct
    {
        protected int _id;

        public int Id => _id;
        
        public async UniTask Initialize(int id,IProtoSystems ecsSystems)
        {
            _id = id;
            await OnInitialize(id, ecsSystems);
            ecsSystems.Add(this);
        }

        public abstract void Run();

        public bool IsPlannerEnabledForEntity(ProtoWorld world, ProtoEntity entity) => 
            AiSystemsTools.IsPlannerEnabledForEntity(world, entity, _id);
        
        public void ApplyPlanningResult(IProtoSystems systems, ProtoEntity entity, AiPlannerData data)
        {
            var world = systems.GetWorld();
            var aiAgentPool = world.GetPool<AiAgentComponent>();
            
            ref var aiAgentComponent = ref aiAgentPool.Get(entity);
            
            var resultData = aiAgentComponent.PlannerData;
            resultData[_id] = data;
        }
        
        public void RemoveComponent(IProtoSystems systems, ProtoEntity entity)
        {
            systems.TryRemoveComponent<TComponent>(entity);
        }

        protected virtual UniTask OnInitialize(int id, IProtoSystems systems) => UniTask.CompletedTask;

    }
}
