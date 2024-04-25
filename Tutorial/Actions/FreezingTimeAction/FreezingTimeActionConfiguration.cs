﻿namespace unigame.ecs.proto.Gameplay.Tutorial.Actions.FreezingTimeAction
{
	using System;
	using Abstracts;
	using Components;
	 
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine.Serialization;

	
	public class FreezingTimeActionConfiguration : TutorialAction
	{
		#region Inspector
        
		public float duration = 1f;
		public float timeScale = 1f;

		#endregion
		
		protected override void Composer(ProtoWorld world, int entity)
		{
			ref var freezingTimeActionComponent = ref world.AddComponent<FreezingTimeActionComponent>(entity);
			freezingTimeActionComponent.Duration = duration;
			freezingTimeActionComponent.TimeScale = timeScale;
		}
	}
}