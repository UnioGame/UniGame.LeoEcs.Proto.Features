﻿namespace Game.Code.Services.AbilityLoadout
{
	using Ability.Data;
	using Abstract;
	using Cysharp.Threading.Tasks;
	using Data;
	using UniGame.AddressableTools.Runtime;
	using UniGame.Core.Runtime;
	using UniGame.GameFlow.Runtime.Services;
	using UnityEngine;
	using UnityEngine.AddressableAssets;

	[CreateAssetMenu(menuName = "Game/Services/AbilityLoadoutService Source", fileName = "AbilityLoadoutService Source")]
	public class AbilityLoadoutServiceSource : DataSourceAsset<IAbilityLoadoutService>
	{
		public AssetReferenceT<AbilityDataBase> abilityDatabase;
		public AssetReferenceT<AbilitySlotsData> abilitySlotMap;
		public AssetReferenceT<AbilityRarityData> abilityRarityData;
		
		protected override async UniTask<IAbilityLoadoutService> CreateInternalAsync(IContext context)
		{
			var data = new AbilityLoadoutData();
			
			var abilityDataBase = abilityDatabase
				.LoadAssetInstanceTaskAsync(context.LifeTime, true,x => data.abilityDataBase =x);
			var slotMap = abilitySlotMap
				.LoadAssetInstanceTaskAsync(context.LifeTime, true,x => data.abilitySlotMap = x);
			var rarityMap = abilityRarityData
				.LoadAssetInstanceTaskAsync(context.LifeTime, true,x => data.abilityRarityData = x);
			
			await UniTask.WhenAll(abilityDataBase, slotMap, rarityMap);

			var profileData = new AbilityProfileData();
			context.Publish(profileData);
            
			var service = new AbilityLoadoutService(profileData,data);
			return service;
		}
		
	}
}