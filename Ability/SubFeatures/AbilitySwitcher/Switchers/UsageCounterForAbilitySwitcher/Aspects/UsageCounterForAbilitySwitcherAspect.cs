﻿namespace unigame.ecs.proto.Ability.SubFeatures.AbilitySwitcher.Switchers.UsageCounterForAbilitySwitcher.Aspects
{
	using System;
	using AbilitySwitcher.Components;
	using Common.Components;
	using Components;
	using Core.Components;
	 
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Usage counter for ability switcher aspect
	/// </summary>
	[Serializable]
	public class UsageCounterForAbilitySwitcherAspect : EcsAspect
	{
		// Owner is required to get ability entity
		public ProtoPool<OwnerComponent> Owner;
		// Says that ability can be used after count of usages
		public ProtoPool<UsageCounterForAbilitySwitcherComponent> UsageCounterForAbilitySwitcher;
		
		// requests
		// Request to apply ability to self
		public ProtoPool<ApplyAbilitySelfRequest> ApplyAbilitySelfRequest;
		// Request to switch ability
		public ProtoPool<AbilitySwitcherRequest> AbilitySwitchRequest;
	}
}