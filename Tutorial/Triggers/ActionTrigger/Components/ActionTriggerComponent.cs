﻿namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Triggers.ActionTrigger.Components
{
	using System;
	using ActionTools;

	/// <summary>
	/// Mark entity as action trigger point.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct ActionTriggerComponent
	{
		public ActionId ActionId;
	}
}