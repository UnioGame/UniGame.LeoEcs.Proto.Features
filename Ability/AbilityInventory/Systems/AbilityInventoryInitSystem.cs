﻿namespace UniGame.Ecs.Proto.AbilityInventory.Systems
{
	using Aspects;
	using Components;
	using Game.Code.Services.AbilityLoadout.Abstract;
	using Leopotam.EcsLite;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;
	using Unity.IL2CPP.CompilerServices;

	/// <summary>
	/// Initialize meta data for all abilities
	/// </summary>
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	[ECSDI]
	public class AbilityInventoryInitSystem : IProtoRunSystem,IProtoInitSystem
	{
		private IAbilityLoadoutService _abilityLoadoutService;
		private AbilityInventoryAspect _inventoryAspect;
		private ProtoWorld _world;
		private EcsFilter _filter;

		public void Init(IProtoSystems systems)
		{
			_world = systems.GetWorld();
			_abilityLoadoutService = _world.GetGlobal<IAbilityLoadoutService>();

			_filter = _world
				.Filter<AbilityInventoryLoadedComponent>()
				.End();
		}
		
		public void Run()
		{
			var first = _filter.First();
			if (first > 0) return;
			
			foreach (var record in _abilityLoadoutService.InventoryAbilities)
			{
				var requestEntity = _world.NewEntity();
				ref var loadMetaRequest = ref _inventoryAspect.LoadMeta.Add(requestEntity);
				loadMetaRequest.AbilityId = record;
			}
			
			var loadedEntity = _world.NewEntity();
			_world.AddComponent<AbilityInventoryLoadedComponent>(loadedEntity);
		}

		
	}
}