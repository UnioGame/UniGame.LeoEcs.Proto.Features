﻿namespace unigame.ecs.proto.Gameplay.Tutorial.Actions.AnalyticsAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Data;
	 
	using Systems;
	using UniGame.Context.Runtime.Extension;
	using UniGame.Core.Runtime;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Gameplay/Tutorial/TutorialAction/Analytics Action Feature", 
		fileName = "Analytics Action Feature")]
	public class AnalyticsActionFeature : TutorialFeature
	{
		[SerializeReference]
		public ITutorialAnalytics analytics;
		
		public override UniTask InitializeFeatureAsync(IProtoSystems ecsSystems)
		{
			var context = ecsSystems.GetShared<IContext>();
			var service = context.Get<ITutorialAnalytics>() ?? analytics;
			ecsSystems.Add(new SendAnalyticsActionSystem(service));
			
			return UniTask.CompletedTask;
		}
	}
}