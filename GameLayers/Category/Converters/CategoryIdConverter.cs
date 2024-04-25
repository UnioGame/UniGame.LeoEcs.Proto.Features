namespace unigame.ecs.proto.GameLayers.Category.Converters
{
    using System;
    using Components;
    using Game.Code.GameLayers.Category;
    using UnityEngine;

    [Serializable]
    public sealed class CategoryIdConverter : GameObjectConverter
    {
        [SerializeField]
        public CategoryId categoryId;

        protected override void OnApply(GameObject target, ProtoWorld world, int entity)
        {
            ref var category = ref world.AddComponent<CategoryIdComponent>(entity);
            category.Value = categoryId;
        }
    }
}
