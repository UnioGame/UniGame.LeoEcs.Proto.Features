﻿namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.OpenWindowAction.Aspects
{
	using Components;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	public class OpenWindowActionAspect : EcsAspect
	{
		public ProtoPool<OpenWindowActionComponent> OpenWindowAction;
		public ProtoPool<CompletedOpenWindowActionComponent> CompletedOpenWindowAction;
	}
}