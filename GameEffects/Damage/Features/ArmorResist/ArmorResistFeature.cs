﻿namespace UniGame.Ecs.Proto.Gameplay.ArmorResist
{
	using Cysharp.Threading.Tasks;
	using Damage;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Proto Features/Damage/Armor Resist Feature", fileName = "Armor Resist Feature")]
	public sealed class ArmorResistFeature : DamageSubFeature
	{
		public sealed override UniTask BeforeDamageSystem(IProtoSystems ecsSystems)
		{
			// recalculate damage by armor resist
			ecsSystems.Add(new RecalculatedDamageArmorResistSystem());
			
			return UniTask.CompletedTask;
		}
	}
}