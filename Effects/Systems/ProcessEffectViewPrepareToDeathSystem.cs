﻿namespace UniGame.Ecs.Proto.Effects.Systems
{
	using System;
	using Aspects;
	using Components;
	using Game.Ecs.Core.Components;
	using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
	using Leopotam.EcsLite;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Shared.Extensions;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Send destroy request to effect view.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class ProcessEffectViewPrepareToDeathSystem : IProtoInitSystem, IProtoRunSystem
	{
		private ProtoWorld _world;
		private EcsFilter _prepareDeathFilter;
		private EcsFilter _viewFilter;

		private OwnershipAspect _ownershipAspect;
		private EffectAspect _effectAspect;

		public void Init(IProtoSystems systems)
		{
			_world = systems.GetWorld();
			_prepareDeathFilter = _world
				.Filter<PrepareToDeathComponent>()
				.End();
			_viewFilter = _world
				.Filter<EffectViewComponent>()
				.End();
		}

		public void Run()
		{
			foreach (var eventEntity in _prepareDeathFilter)
			{
				ref var prepareToDeathEvent = ref _ownershipAspect.PrepareToDeath.Get(eventEntity);
				
				foreach (var viewEntity in _viewFilter)
				{
					ref var ownerLinkComponent = ref _ownershipAspect.OwnerLink.Get(viewEntity);
					if (!ownerLinkComponent.Value.Equals(prepareToDeathEvent.Source))
						continue;
					
					_effectAspect.DestroyEffectViewSelfRequest.TryAdd(viewEntity);
				}
			}
		}
	}
}