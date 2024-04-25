namespace unigame.ecs.proto.Characteristics.CriticalChance.Converters
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using unigame.ecs.proto.Characteristics.Base.Components.Requests;
     
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniModules.UniCore.GizmosTools.Shapes;
    using UnityEngine;
    using Random = UnityEngine.Random;

    [Serializable]
    public class AttackRangeComponentConverter : LeoEcsConverter,ILeoEcsGizmosDrawer
    {
        public float attackRange = 3.2f;
        public float minValue = 3f;
        public float maxValue = 100;
        public Vector2 delta = new Vector2(-0.5f, 0.5f);

        public Color rangeGizmosColor = new Color(0.128f, 0.200f, 0.850f);

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var createCharacteristicRequest = ref world
                .GetOrAddComponent<CreateCharacteristicRequest<AttackRangeComponent>>(entity);

            var deltaValue = Random.Range(delta.x, delta.y);
            
            createCharacteristicRequest.Value = attackRange + deltaValue;
            createCharacteristicRequest.MaxValue = maxValue;
            createCharacteristicRequest.MinValue = minValue;
            createCharacteristicRequest.Owner = entity.PackEntity(world);

            ref var criticalChanceComponent = ref world.GetOrAddComponent<AttackRangeComponent>(entity);
        }
        
        public void DrawGizmos(GameObject target)
        {
            GizmosShape.DrawCircle(target.transform.position,
                target.transform.rotation,attackRange, rangeGizmosColor);
        }
    }
}
