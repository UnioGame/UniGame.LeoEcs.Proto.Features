﻿namespace UniGame.Ecs.Proto.Characteristics.ArmorResist.Aspects
{
	using System;
	using Base.Components;
	using Components;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Armor resist aspect
	/// </summary>
	[Serializable]
	public class ArmorResistAspect : EcsAspect
	{
		// characteristics marker
		public ProtoPool<CharacteristicComponent<ArmorResistComponent>> Characteristic;
		// armor resist value
		public ProtoPool<ArmorResistComponent> ArmorResist;
	}
}