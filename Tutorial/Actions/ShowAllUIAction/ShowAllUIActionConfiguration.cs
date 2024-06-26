﻿namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.ShowAllUIAction
{
	using Abstracts;
	using Components;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Shared.Extensions;

	public class ShowAllUIActionConfiguration : TutorialAction
	{
		protected override void Composer(ProtoWorld world, ProtoEntity entity)
		{
			world.AddComponent<ShowAllUIActionComponent>(entity);
		}
	}
}