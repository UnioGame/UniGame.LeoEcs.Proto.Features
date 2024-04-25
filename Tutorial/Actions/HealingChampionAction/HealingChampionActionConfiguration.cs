﻿namespace unigame.ecs.proto.Gameplay.Tutorial.Actions.HealingChampionAction
{
	using Abstracts;
	using Components;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Shared.Extensions;

	public class HealingChampionActionConfiguration : TutorialAction
	{
		#region Ispector

		public float HealPeriod = 0.1f;
		public float HealDuration = 2f;
		public float HealOverMax = 300f;

		#endregion
		
		protected override void Composer(ProtoWorld world, ProtoEntity entity)
		{
			ref var healingAction = ref world.AddComponent<HealingChampionActionComponent>(entity);
			healingAction.HealPeriod = HealPeriod;
			healingAction.HealDuration = HealDuration;
			healingAction.HealOverMax = HealOverMax;
		}
	}
}