﻿namespace unigame.ecs.proto.AbilityInventory.Systems
{
	using System;
	using Aspects;
	using Components;
	using Leopotam.EcsLite;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;

	/// <summary>
	/// Search ability in inventory
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class EquipAbilityByIdSystem : IProtoInitSystem, IProtoRunSystem
	{
		private AbilityInventoryAspect _inventoryAspect;
		
		private ProtoWorld _world;
		private EcsFilter _filterRequest;
		
		public void Init(IProtoSystems systems)
		{
			_world = systems.GetWorld();

			_filterRequest = _world
				.Filter<EquipAbilityIdSelfRequest>()
				.End();
		}

		public void Run()
		{
			foreach (var abilityEntity in _filterRequest)
			{
				ref var requestComponent = ref _inventoryAspect.EquipById.Get(abilityEntity);
				ref var abilityEquipRequest = ref _world.AddComponent<EquipAbilitySelfRequest>(abilityEntity);
				abilityEquipRequest.AbilityId = requestComponent.AbilityId;
				abilityEquipRequest.IsUserInput = requestComponent.IsUserInput;
				abilityEquipRequest.AbilitySlot = requestComponent.AbilitySlot;
				abilityEquipRequest.Target = requestComponent.Owner;
				abilityEquipRequest.IsDefault = requestComponent.IsDefault;

				_inventoryAspect.EquipById.Del(abilityEntity);
			}
		}
	}
}