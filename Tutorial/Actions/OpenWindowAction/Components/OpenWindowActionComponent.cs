﻿namespace unigame.ecs.proto.Gameplay.Tutorial.Actions.OpenWindowAction.Components
{
	using System;
	using UniGame.UiSystem.Runtime.Settings;
	using UniModules.UniGame.UiSystem.Runtime;

	/// <summary>
	/// Mark entity as open window action.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct OpenWindowActionComponent
	{
		public ViewId View;
		public ViewType LayoutType;
	}
}