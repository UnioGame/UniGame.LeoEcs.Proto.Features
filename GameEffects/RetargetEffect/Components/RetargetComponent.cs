﻿namespace UniGame.Ecs.Proto.GameEffects.RetargetEffect.Components
{
	using System;
	using UnityEngine.Serialization;

	/// <summary>
	/// Stores the duration of the retarget effect
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct RetargetComponent
	{
		public float Value;
	}
}