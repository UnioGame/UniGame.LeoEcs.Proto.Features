﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.Target.Systems
{
	using System;
	using Aspects;
	using Components;
	using Game.Ecs.Core.Components;
	using Leopotam.EcsLite;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using TargetSelection;
	using UniGame.LeoEcs.Shared.Extensions;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Select targets for ability
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class SelectTargetsSystem : IProtoInitSystem, IProtoRunSystem
	{
		private ProtoWorld _world;
		private TargetAbilityAspect _aspect;
		private EcsFilter _filter;
		
		private ProtoEntity[] _abilityTargets = new ProtoEntity[TargetSelectionData.MaxTargets];

		public void Init(IProtoSystems systems)
		{
			_world = systems.GetWorld();
			
			_filter = _world
				.Filter<OwnerComponent>()
				.Inc<AbilityTargetsComponent>()
				.End();
		}

		public void Run()
		{
			foreach (var entity in _filter)
			{
				ref var abilityTargets = ref _aspect.AbilityTargets.Get(entity);
				var amount = _world.UnpackAll(abilityTargets.Entities,_abilityTargets,abilityTargets.Count);
				if (amount == 0) continue;

				ref var soloTarget = ref _aspect.SoloTarget.Get(entity);
				var firstTargetEntity = _abilityTargets[0];
				var newTarget = _world.PackEntity(firstTargetEntity);
				
				if (!soloTarget.Value.Unpack(_world, out var targetEntity))
				{
					soloTarget.Value = newTarget;
				}
				
				var isLastTarget = false;

				for (var i = 0; i < amount; i++)
				{
					var abilityTarget = _abilityTargets[i];
					if (!abilityTarget.Equals(targetEntity)) continue;
					isLastTarget = true;
					break;
				}
				
				var lastTarget = soloTarget.Value;
				soloTarget.Value = isLastTarget ? lastTarget : newTarget;
				
				ref var multipleTargets = ref _aspect.MultipleTargets.Get(entity);
				multipleTargets.SetEntities(abilityTargets.Entities,abilityTargets.Count);
			}
		}
	}
}