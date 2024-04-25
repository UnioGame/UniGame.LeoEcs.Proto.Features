﻿namespace unigame.ecs.proto.Ability.AbilityUtilityView.Radius.AggressiveRadius.Components
{
    using System.Collections.Generic;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;


    public struct AggressiveRadiusViewStateComponent : IProtoAutoReset<AggressiveRadiusViewStateComponent>
    {
        private List<ProtoPackedEntity> _entities;
        private List<ProtoPackedEntity> _previousEntities;

        public IReadOnlyList<ProtoPackedEntity> Entities => _entities;
        public IReadOnlyList<ProtoPackedEntity> PreviousEntities => _previousEntities;

        public void SetEntities(IEnumerable<ProtoPackedEntity> entities)
        {
            _previousEntities.Clear();
            _previousEntities.AddRange(Entities);
            
            _entities.Clear();
            _entities.AddRange(entities);
        }

        public void AutoReset(ref AggressiveRadiusViewStateComponent c)
        {
            c._entities ??= new List<ProtoPackedEntity>();
            c._entities.Clear();
            
            c._previousEntities ??= new List<ProtoPackedEntity>();
            c._previousEntities.Clear();
        }
    }
}