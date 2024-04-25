﻿namespace unigame.ecs.proto.Gameplay.Tutorial.Triggers.ActionTrigger.Aspects
{
	using System;
	using Components;
	 
	using Tutorial.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	[Serializable]
	public class ActionTriggerAspect : EcsAspect
	{
		public ProtoPool<ActionTriggerComponent> ActionTrigger;
		public ProtoPool<ActionTriggerRequest> ActionTriggerRequest;
		public ProtoPool<CompletedActionTriggerComponent> CompletedActionTrigger;
		public ProtoPool<RunTutorialActionsRequest> RunTutorialActionsRequest;
	}
}