﻿namespace unigame.ecs.proto.Gameplay.Tutorial.Configurations
{
	using System.Collections.Generic;
	using Abstracts;
	using Components;
	using Game.Ecs.Core.Components;
	using Leopotam.EcsProto;
	using Sirenix.OdinInspector;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;
    
	[InlineProperty]
	[CreateAssetMenu(menuName = "Game/Tutorial/Tutorial Trigger Point Configuration", fileName = "Tutorial Trigger Point Configuration")]
	public class TutorialTriggerPointConfiguration : ScriptableObject
	{
		#region Inspector
        
		[BoxGroup("Start")]
		[SerializeReference]
		public ITutorialTrigger StartTrigger;
		
		[BoxGroup("Start")]
		[SerializeReference]
		public List<ITutorialAction> StartTriggerActions;

		[BoxGroup("Final")]
		[SerializeReference]
		public ITutorialTrigger FinalTrigger;
		
		[BoxGroup("Final")]
		[SerializeReference]
		public List<ITutorialAction> FinalTriggerActions;

		#endregion
		
		public bool Apply(ProtoWorld world, int entity)
		{
			var startTriggerEntity = world.NewEntity();
			var finalTriggerEntity = world.NewEntity();
			
			ref var startTriggerOwner = ref world.AddComponent<OwnerComponent>(startTriggerEntity);
			startTriggerOwner.Value = world.PackedEntity(entity);
			
			ref var startActionsComponent = ref world.AddComponent<TutorialActionsComponent>(startTriggerEntity);
			
			ref var finalTriggerOwner = ref world.AddComponent<OwnerComponent>(finalTriggerEntity);
			finalTriggerOwner.Value = world.PackedEntity(entity);
			
			ref var finalActionsComponent = ref world.AddComponent<TutorialActionsComponent>(finalTriggerEntity);

			StartTrigger ??= new EmptyTrigger();
			StartTrigger.ComposeEntity(world, startTriggerEntity);
			startActionsComponent.Actions.AddRange(StartTriggerActions);
            
			FinalTrigger ??= new EmptyTrigger();
			FinalTrigger.ComposeEntity(world, finalTriggerEntity);
            finalActionsComponent.Actions.AddRange(FinalTriggerActions);
			
			return true;
		}
	}
}