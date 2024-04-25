﻿namespace unigame.ecs.proto.Gameplay.Tutorial.Actions.EquipAbilityAction.Components
{
	using System;
	using System.Collections.Generic;
	using Code.Configuration.Runtime.Ability;
	 

	/// <summary>
	/// Mark entity as equip ability action.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct EquipAbilityActionComponent : IProtoAutoReset<EquipAbilityActionComponent>
	{
		public List<AbilityCell> AbilityCells;
		
		public void AutoReset(ref EquipAbilityActionComponent c)
		{
			c.AbilityCells ??= new List<AbilityCell>();
			c.AbilityCells.Clear();
		}
	}
}