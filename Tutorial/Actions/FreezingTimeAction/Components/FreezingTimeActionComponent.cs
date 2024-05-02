﻿namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.FreezingTimeAction.Components
{
	using System;

	/// <summary>
	/// Mark entity as freezing time action.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct FreezingTimeActionComponent
	{
		public float Duration;
		public float TimeScale;
	}
}