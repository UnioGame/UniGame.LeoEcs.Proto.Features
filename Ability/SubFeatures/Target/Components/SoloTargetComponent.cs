﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.Target.Components
{
	using System;
	using Leopotam.EcsProto.QoL;


	/// <summary>
	/// Storage for target entity.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct SoloTargetComponent
	{
		public ProtoPackedEntity Value;
	}
}