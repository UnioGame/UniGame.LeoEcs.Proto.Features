﻿namespace UniGame.Ecs.Proto.GameEffects.CriticalEffect.Systems
{
	using System;
	using System.Linq;
	using Aspect;
	using Components;
	using Leopotam.EcsLite;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.Core.Runtime.Extension;
	using UniGame.LeoEcs.Shared.Extensions;
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
	public class CriticalEffectSystem : IProtoInitSystem, IProtoRunSystem
	{
		private ProtoWorld _world;
		private CriticalEffectAspect _aspect;
		private EcsFilter _filter;

		public void Init(IProtoSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<CriticalEffectComponent>()
				.Exc<CriticalEffectReadyComponent>()
				.End();
		}

		public void Run()
		{
			foreach (var entity in _filter)
			{
				ref var effectComponent = ref _aspect.Effect.Get(entity);
				if (!effectComponent.Destination.Unpack(_world, out var destinationEntity))
					continue;
				_aspect.CriticalAttackMarker.GetOrAddComponent(destinationEntity);
				_aspect.CriticalEffectReady.Add(entity);
			}
		}
	}
}