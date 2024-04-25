﻿namespace unigame.ecs.proto.AI.Abstract
{
    using System;
    using System.Threading;
    using Configurations;
    using Leopotam.EcsProto;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public abstract class PlannerConverter : GameObjectConverter,IPlannerConverter, IEntityConverter
    {
        [SerializeField]
        public AiAgentActionId id;
        
        public AiAgentActionId Id => id;

        public void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            if (enabled == false)
                return;
            
            Apply(world, entity);
            OnApply(target,world,entity);
        }
        
        protected virtual void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            
        }

        public virtual void Apply(ProtoWorld world, ProtoEntity entity)
        {
            
        }
    }


    [Serializable]
    public class PlannerConverter<TComponent> : PlannerConverter
        where TComponent : struct, IApplyableComponent<TComponent>
    {
        [InlineProperty]
        [SerializeField]
        [HideLabel]
        public TComponent data;
    
        protected sealed override void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var component = ref world.GetOrAddComponent<TComponent>(entity);
            data.Apply(ref component);
            OnApplyComponents(target, world, entity);
        }
        
        protected virtual void OnApplyComponents(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
        }

    }
}