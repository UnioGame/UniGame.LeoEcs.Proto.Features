﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.AbilitySwitcher.Aspects
{
	using System;
	using Ability.Components.Requests;
	using Common.Components;
	using Components;
	using Game.Ecs.Core.Components;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Ability switcher aspect
	/// </summary>
	[Serializable]
	public class AbilitySwitcherAspect : EcsAspect
	{
		public ProtoPool<OwnerComponent> Owner;
		
		// requests
		public ProtoPool<AbilitySwitcherRequest> AbilitySwitchRequest;
		public ProtoPool<CompleteAbilitySelfRequest> CompleteAbilitySelfRequest;
		public ProtoPool<RestartAbilityCooldownSelfRequest> RestartAbilityCooldownSelfRequest;
		public ProtoPool<ResetAbilityCooldownSelfRequest> ResetAbilityCooldownSelfRequest;
	}
}