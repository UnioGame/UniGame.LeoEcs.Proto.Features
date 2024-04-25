﻿namespace unigame.ecs.proto.Shaders
{
	using Cysharp.Threading.Tasks;
	 
	using Systems;
	using UniGame.LeoEcs.Bootstrap.Runtime;
	using UniGame.LeoEcs.Bootstrap.Runtime.Config;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Shaders Feature", fileName = "Shaders Feature Configuration")]
	public class ShadersFeature : BaseLeoEcsFeature
	{
		public override UniTask InitializeFeatureAsync(IProtoSystems ecsSystems)
		{
			// Sets shader materials to the position of the player.
			ecsSystems.Add(new ChampionGlobalMaskSystem());

			return UniTask.CompletedTask;
		}
	}
}