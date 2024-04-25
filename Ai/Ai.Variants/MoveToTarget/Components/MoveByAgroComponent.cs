﻿namespace unigame.ecs.proto.GameAi.MoveToTarget.Components
{
    using System;
    using Game.Code.GameLayers.Category;
    using Game.Code.GameLayers.Relationship;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;

    [Serializable]
    public struct MoveByAgroComponent : IApplyableComponent<MoveByAgroComponent>
    {
        [SerializeField]
        public MoveByAgroFilterData[] FilterData;

        public void Apply(ref MoveByAgroComponent component)
        {
            component.FilterData = FilterData;
        }
    }
    
    [Serializable]
    public struct MoveByAgroFilterData
    {
        [RelationshipIdMask]
        public RelationshipId RelationshipId;
        public CategoryId CategoryId;
        public float SensorDistance;
        public float AgroEffectMultiplier;
    }
}