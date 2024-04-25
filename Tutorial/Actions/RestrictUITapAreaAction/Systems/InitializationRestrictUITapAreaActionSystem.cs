﻿namespace unigame.ecs.proto.Gameplay.Tutorial.Actions.RestrictUITapAreaAction.Systems
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Aspects;
	using Components;
	 
	using UniGame.Core.Runtime.Extension;
	using UniGame.Runtime.ObjectPool.Extensions;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

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
	public class InitializationRestrictUITapAreaActionSystem : IProtoInitSystem, IProtoRunSystem
	{
		private ProtoWorld _world;
		private RestrictUITapAreaActionAspect _aspect;
		private EcsFilter _filter;
		private List<ProtoPackedEntity> _restrictTapAreas;

		public void Init(IProtoSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<RestrictUITapAreaActionComponent>()
				.Exc<RestrictUITapAreaActionReadyComponent>()
				.End();
			_restrictTapAreas = new List<ProtoPackedEntity>();
		}

		public void Run()
		{
			foreach (var entity in _filter)
			{
				_restrictTapAreas.Clear();
				ref var restrictTapAreaComponent = ref _aspect.RestrictUITapAreaAction.Get(entity);

				var areas = restrictTapAreaComponent.Areas;
				
				for (var i = 0; i < areas.Count; i++)
				{
					var restrictEntity = _world.NewEntity();
					var area = areas[i];
					if (i == 0)
					{
						_aspect.ActivateRestrictUITapArea.Add(restrictEntity);
					}
					else
					{
						_restrictTapAreas.Add(_world.PackEntity(restrictEntity));
					}
					
					ref var restrictTapArea = ref _aspect.RestrictUITapArea.Add(restrictEntity);
					restrictTapArea.Value = area;
				}

				ref var restrictUITapAreas = ref _aspect.RestrictUITapAreas.Add(entity);
				restrictUITapAreas.Value = new Queue<ProtoPackedEntity>(_restrictTapAreas);
				_aspect.RestrictUITapAreaActionReady.Add(entity);
			}
		}
	}
}