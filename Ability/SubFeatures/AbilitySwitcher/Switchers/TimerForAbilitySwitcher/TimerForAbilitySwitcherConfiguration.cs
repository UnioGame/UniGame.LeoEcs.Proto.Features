﻿namespace unigame.ecs.proto.Ability.SubFeatures.AbilitySwitcher.Switchers.TimerForAbilitySwitcher
{
	using System;
	using Abstracts;
	using Components;
	using Leopotam.EcsProto;
	using Sirenix.OdinInspector;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;
	using UnityEngine.Serialization;

	[Serializable]
	public class TimerForAbilitySwitcherConfiguration : IAbilitySwitcherConfiguration
	{
		#region Inspector

		[MinValue(0)]
		public float delay;
		public bool isUnscaledTime;

		#endregion
		public void Compose(ProtoWorld world, ProtoEntity abilityEntity)
		{
			ref var timer = ref world.AddComponent<TimerForAbilitySwitcherComponent>(abilityEntity);
			timer.Delay = delay;
			var time = isUnscaledTime ? Time.unscaledTime : Time.time;
			timer.DumpTime = time + delay;
		}
	}
}