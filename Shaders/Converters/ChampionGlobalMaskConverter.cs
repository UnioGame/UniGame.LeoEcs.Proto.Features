﻿namespace UniGame.Ecs.Proto.Shaders.Converters
{
	using System;
	using System.Collections.Generic;
	using Components;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Converter.Runtime;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;
	using Object = UnityEngine.Object;

	[Serializable]
	public class ChampionGlobalMaskConverter : LeoEcsConverter
	{
		#region inspector
		public List<string> variables = new List<string>();
		public List<Material> materials = new List<Material>();
		#endregion
		
		
		public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
		{
			var playerGlobalMaskPool = world.GetPool<ChampionGlobalMaskComponent>();
			ref var playerGlobalMaskComponent = ref playerGlobalMaskPool.Add(entity);
			playerGlobalMaskComponent.Variables = variables;

			foreach (var material in materials)
			{
				var instance = Object.Instantiate(material);
				playerGlobalMaskComponent.Materials.Add(instance);
			}
		}
	}
}