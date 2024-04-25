namespace Game.Code.Ai.ActivateAbility
{
    using System;
    using Cysharp.Threading.Tasks;
    using Ecs.Core.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using unigame.ecs.proto.AI.Components;
    using unigame.ecs.proto.AI.Systems;
    using unigame.ecs.proto.GameAi.ActivateAbility;
    using unigame.ecs.proto.GameLayers.Layer.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Show and Hides HealthBars based on UnderTheTargetComponent 
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    [Serializable]
    public class ActivateAbilityPlannerSystem : BasePlannerSystem<ActivateAbilityActionComponent>,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        private IProtoSystems _systems;
        private ProtoPool<ActivateAbilityActionComponent> _actionPool;
        private ProtoPool<ActivateAbilityPlannerComponent> _activateAbilityPool;
        private ProtoPool<AbilityAiActionTargetComponent> _abilityTargetPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _systems = systems;
            
            _filter = _world
                .Filter<AiAgentComponent>()
                .Inc<AbilityAiActionTargetComponent>()
                .Inc<TransformPositionComponent>()
                .Inc<LayerIdComponent>()
                .Inc<ActivateAbilityPlannerComponent>()
                .Exc<PrepareToDeathComponent>()
                .End();
        }
        
        public override void Run()
        {
            foreach (var entity in _filter)
            {
                ref var targetComponent = ref _abilityTargetPool.Get(entity);
                ref var plannerComponent = ref _activateAbilityPool.Get(entity);

                ref var abilityActionComponent = ref _actionPool.GetOrAddComponent(entity);
                abilityActionComponent.Target = targetComponent.AbilityTarget;
                abilityActionComponent.AbilityCellId = targetComponent.AbilityCellId;
                abilityActionComponent.Ability = targetComponent.Ability;
    
                ApplyPlanningResult(_systems, entity, plannerComponent.PlannerData);
            }
        }

        protected override UniTask OnInitialize(int id, IProtoSystems systems)
        {
            systems.DelHere<AbilityAiActionTargetComponent>();
            //systems.Add(new SelectAbilityTargetsDataPlannerSystem());
            systems.Add(new SelectAbilityTargetsByRangePlannerSystem());
            systems.Add(new SelectAbilityTargetsPlannerSystem());
            
            return UniTask.CompletedTask;
        }
    }
}