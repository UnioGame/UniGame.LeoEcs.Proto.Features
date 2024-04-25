﻿namespace unigame.ecs.proto.Ability.Common.Components
{
    /// <summary>
    /// Запрос "положить" в руку умение из конкретной ячейки.
    /// </summary>
    public struct SetInHandAbilityBySlotSelfRequest
    {
        public int AbilityCellId;
    }
}