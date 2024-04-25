﻿namespace unigame.ecs.proto.Gameplay.Tutorial.Components
{
	using System;
	using UnityEngine.EventSystems;

	/// <summary>
	/// ADD DESCRIPTION HERE
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct TutorialEventSystemComponent
	{
		public EventSystem Value;
	}
}