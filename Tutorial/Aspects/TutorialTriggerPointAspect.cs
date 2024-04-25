﻿namespace unigame.ecs.proto.Gameplay.Tutorial.Aspects
{
	using System;
	using Components;
	using Game.Ecs.Core.Components;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Shared.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
	public class TutorialTriggerPointAspect : EcsAspect
	{
		public ProtoWorld World;
        
		public ProtoPool<TransformComponent> Transform;
		public ProtoPool<RunTutorialActionsRequest> RunTutorialActionsRequest;
		public ProtoPool<TutorialActionsComponent> TutorialActions;
		public ProtoPool<OwnerComponent> Owner;
		public ProtoPool<DelayedTutorialComponent> DelayedTutorial;
		public ProtoPool<CompletedDelayedTutorialComponent> CompletedDelayedTutorial;
	}
}