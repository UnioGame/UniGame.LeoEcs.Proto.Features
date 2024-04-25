﻿namespace unigame.ecs.proto.Gameplay.Tutorial.Actions.ShowAllUIAction
{
	using Abstracts;
	using Components;
	using Cysharp.Threading.Tasks;
	 
	using Leopotam.EcsLite.ExtendedSystems;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Gameplay/Tutorial/TutorialAction/Show All UI Action Feature", 
		fileName = "Show All UI Action Feature")]
	public class ShowAllUIActionFeature : TutorialFeature
	{
		public override async UniTask InitializeFeatureAsync(IProtoSystems ecsSystems)
		{
			ecsSystems.DelHere<ShowAllUIActionEvent>();
			// Send event to show all UI
			ecsSystems.Add(new ShowAllUIActionSystem());
		}
	}
}