﻿namespace UniGame.Ecs.Proto.Ability.Common.Components
{
    using System;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Unity.Collections;

    /// <summary>
    /// Компонент со ссылками на доступные умения у сущности.
    /// </summary>
    [Serializable]
    public struct AbilityMapComponent : IProtoAutoReset<AbilityMapComponent>
    {
        public NativeHashMap<int,ProtoPackedEntity> AbilitySlots;
        public NativeHashSet<ProtoPackedEntity> Abilities;

        public void AutoReset(ref AbilityMapComponent c)
        {
            if (AbilitySlots.IsCreated)
                AbilitySlots.Dispose();
            if (Abilities.IsCreated)
                Abilities.Dispose();
        }
    }
}