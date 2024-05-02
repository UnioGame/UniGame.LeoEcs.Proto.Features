﻿namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Components
{
	using System;

	/// <summary>
	/// Mark delay tutorial action/trigger as completed
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct CompletedDelayedTutorialComponent
	{
		
	}
}