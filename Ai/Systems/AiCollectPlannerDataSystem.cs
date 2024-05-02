namespace UniGame.Ecs.Proto.AI.Systems
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine.Pool;

    /// <summary>
    /// collect ai agents info
    /// </summary>
    [Serializable]
    public class AiCollectPlannerDataSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AiAgentComponent>()
                .Exc<AiAgentSelfControlComponent>()
                .End();
        }
        
        public void Run()
        {
            var agentsPool = _world.GetPool<AiAgentComponent>();
            var aiDataPool = _world.GetPool<AiAgentPlanningComponent>();

            foreach (var agentEntity in _filter)
            {
                ref var agentComponent = ref agentsPool.Get(agentEntity);
                ref var dataComponent = ref aiDataPool.Add(agentEntity);
                
                var aiPlan = dataComponent.AiPlan;
                dataComponent.AiPlan = aiPlan ?? ListPool<AiAgentPlanningData>.Get();
                aiPlan = dataComponent.AiPlan;
                
                var agentPlan = agentComponent.PlannerData;

                for (var id = 0; id < agentPlan.Length; id++)
                {
                    var data = agentPlan[id];

                    var actionPriority = data.Priority;
                    if(actionPriority < 0) continue;

                    var planItem = new AiAgentPlanningData
                    {
                        Priority = data.Priority,
                        ActionId = id,
                    };
                    
                    aiPlan.Add(planItem);
                }
            }
        }

    }
}
