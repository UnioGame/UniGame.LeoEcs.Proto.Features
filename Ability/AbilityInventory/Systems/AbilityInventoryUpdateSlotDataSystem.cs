﻿namespace UniGame.Ecs.Proto.AbilityInventory.Systems
{
	using System;
	using Ability.Common.Components;
	using Components;
	using Cysharp.Threading.Tasks;
	using Game.Code.Services.AbilityLoadout.Abstract;
	using Leopotam.EcsLite;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class AbilityInventoryUpdateSlotDataSystem : IProtoInitSystem, IProtoRunSystem
	{
		private readonly IAbilityCatalogService _abilityService;
		
		private ProtoWorld _world;
		private EcsFilter _filter;
		private EcsFilter _eventFilter;
		
		private ProtoPool<AbilityIdComponent> _abilityIdPool;
		private ProtoPool<AbilityEquipChangedEvent> _eventPool;

		public AbilityInventoryUpdateSlotDataSystem(IAbilityCatalogService abilityLoadoutService)
		{
			_abilityService = abilityLoadoutService;
		}

		public void Init(IProtoSystems systems)
		{
			_world = systems.GetWorld();
				
			_filter = _world
				.Filter<AbilityInventoryComponent>()
				.Exc<AbilityInventorySaveCompleteEvent>()
				.End();

			_eventFilter = _world
				.Filter<AbilityEquipChangedEvent>()
				.End();
		}

		public void Run()
		{
			foreach (var eventEntity in _eventFilter)
			{
				ref var eventComponent = ref _eventPool.Get(eventEntity);
				if(!eventComponent.Owner.Unpack(_world,out var ownerEntity))
					continue;

				foreach (var entity in _filter)
				{
					if(!entity.Equals(ownerEntity)) continue;
						
					_abilityService
						.EquipAsync(eventComponent.AbilityId,eventComponent.AbilitySlot)
						.Forget();
					
					var saveEventEntity = _world.NewEntity();
					
					ref var saveEventComponent = ref _world
						.AddComponent<AbilityInventorySaveCompleteEvent>(saveEventEntity);
					saveEventComponent.Value = _world.PackEntity(entity);
				}
			}
			
		}

	}
}