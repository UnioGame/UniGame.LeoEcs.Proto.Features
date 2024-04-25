namespace unigame.ecs.proto.GameLayers.Relationship
{
    using System;
    using Code.GameLayers.Layer;
    using Code.GameLayers.Relationship;
    using Cysharp.Threading.Tasks;
     
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    [Serializable]
    public sealed class RelationshipFeature : LeoEcsSystemAsyncFeature
    {
        [SerializeField]
        private LayerIdConfiguration _layerIdConfiguration;
        [SerializeField]
        private RelationshipIdMap _relationshipIdMap;
        [SerializeField]
        private RelationshipId _selfRelationship;
    
        public override UniTask InitializeFeatureAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new RelationshipToolsSystem(_layerIdConfiguration, _relationshipIdMap, _selfRelationship));
            
            return UniTask.CompletedTask;
        }
    }
}
