﻿namespace unigame.ecs.proto.Gameplay.Tutorial.Actions.RestrictUITapAreaAction.Systems
{
	using System;
	using Aspects;
	using Components;
	using Leopotam.EcsLite;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;

	/// <summary>
	/// ADD DESCRIPTION HERE
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class SelectionNextTapAreaSystem : IProtoInitSystem, IProtoRunSystem
	{
		private ProtoWorld _world;
		private RestrictUITapAreaActionAspect _aspect;
		private EcsFilter _activeRestrictTapAreaFilter;
		private EcsFilter _restrictTapAreasFilter;

		public void Init(IProtoSystems systems)
		{
			_world = systems.GetWorld();
			_activeRestrictTapAreaFilter = _world
				.Filter<RestrictUITapAreaComponent>()
				.Inc<ActivateRestrictUITapAreaComponent>()
				.Inc<CompletedRestrictUITapAreaComponent>()
				.End();

			_restrictTapAreasFilter = _world
				.Filter<RestrictUITapAreasComponent>()
				.Exc<CompletedRestrictUITapAreaActionComponent>()
				.End();
		}

		public void Run()
		{
			foreach (var entity in _activeRestrictTapAreaFilter)
			{
				if (_restrictTapAreasFilter.GetEntitiesCount() == 0)
					continue;
				var areasEntity = _restrictTapAreasFilter.GetRawEntities().FirstOrDefault();
				ref var areasActionComponent = ref _aspect.RestrictUITapAreaAction.Get(areasEntity);
				ref var areasComponent = ref _aspect.RestrictUITapAreas.Get(areasEntity);		
				_aspect.ActivateRestrictUITapArea.Del(entity);
				
				if (areasComponent.Value.Count == 0)
				{
					_aspect.CompletedRestrictUITapAreaAction.Add(areasEntity);
					var requestEntity = _world.NewEntity();
					ref var request = ref _aspect.ActionTriggerRequest.Add(requestEntity);
					request.ActionId = areasActionComponent.ActionId;
					continue;
				}
				
				var area = areasComponent.Value.Dequeue();
				if (!area.Unpack(_world, out var areaEntity))
					continue;
				_aspect.ActivateRestrictUITapArea.Add(areaEntity);
			}
		}
	}
}