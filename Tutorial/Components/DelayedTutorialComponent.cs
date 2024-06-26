﻿namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Components
{
	using System;
	using Abstracts;

	/// <summary>
	/// Add delay to tutorial action/trigger
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct DelayedTutorialComponent
	{
		public float Delay;
		public float LastApplyingTime;
		public ITutorialEvent Context;
	}
}