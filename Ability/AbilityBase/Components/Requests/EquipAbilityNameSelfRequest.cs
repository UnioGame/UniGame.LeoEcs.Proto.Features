﻿namespace UniGame.Ecs.Proto.AbilityInventory.Components
{
    using System;
    using Leopotam.EcsProto.QoL;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// Search ability in inventory
    /// </summary>
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public struct EquipAbilityNameSelfRequest
    {
        public string AbilityName;
        public int AbilitySlot;
        public bool IsUserInput;
        public bool IsDefault;
        public ProtoPackedEntity Owner;
    }
}