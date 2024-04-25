namespace unigame.ecs.proto.GameAi.MoveToTarget.Systems
{
    using System;
    using AI.Components;
    using AI.Service;
    using AI.Systems;
    using Cysharp.Threading.Tasks;
    using Components;
    using Data;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Movement.Components;
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
    public class MoveToTargetPlannerSystem : BasePlannerSystem<MoveToTargetActionComponent>, IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        private IProtoSystems _systems;
        
        private ProtoPool<MoveToGoalComponent> _goalPool;
        private ProtoPool<MoveToTargetActionComponent> _moveToTargetActionPool;
        private ProtoPool<MoveToTargetPlannerComponent> _moveToTargetPlannerPool;
        
        public void Init(IProtoSystems systems)
        {
            _systems = systems;
            _world = systems.GetWorld();
            _filter = _world.Filter<AiAgentComponent>()
                .Inc<MoveToTargetPlannerComponent>()
                .Inc<MoveToGoalComponent>()
                .Exc<ImmobilityComponent>()
                .Exc<DisabledComponent>()
                .End();
        }

        public override void Run()
        {
            foreach (var entity in _filter)
            {
                ref var goalComponent = ref _goalPool.Get(entity);
                ref var plannerComponent = ref _moveToTargetPlannerPool.Get(entity);

                var goals = goalComponent.Goals;
                var targetPriority = -1f;
                var goal = new MoveToGoalData
                {
                    Complete = true,
                    Priority = -1,
                };

                foreach (var goalData in goals)
                {
                    if (goalData.Complete)
                        continue;
                    var priority = goalData.Priority;
                    if (priority < targetPriority)
                        continue;

                    goal = goalData;
                    targetPriority = priority;
                }

                ref var component = ref _moveToTargetActionPool.GetOrAddComponent(entity);

                component.Position = goal.Position;
                component.Effects = goal.Effects;

                var plannerData = plannerComponent.PlannerData;
                var plannerPriority = targetPriority > 0
                    ? plannerData.Priority
                    : AiConstants.PriorityNever;

                plannerData.Priority = plannerPriority;

                ApplyPlanningResult(_systems, entity, plannerData);
            }
        }

        protected override UniTask OnInitialize(int id, IProtoSystems systems)
        {
            systems.Add(new ClearMoveToTargetsSystem());
            systems.Add(new SelectByRangePlannerSystem());
            systems.Add(new SelectOutOfRangePlannerSystem());
            systems.Add(new SelectCategoryTargetSystem());
            systems.Add(new SelectPoiPlannerSystem());

            systems.Add(new ResetPoiSystem());

            return UniTask.CompletedTask;
        }
    }
}