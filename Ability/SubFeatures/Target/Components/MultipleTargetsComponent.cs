﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.Target.Components
{
	using System;
	using System.Runtime.CompilerServices;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using TargetSelection;
	using Unity.Mathematics;
	
	/// <summary>
	/// Storage for target entities.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct MultipleTargetsComponent : IProtoAutoReset<MultipleTargetsComponent>
	{
		public static readonly ProtoPackedEntity Empty = default;
		
		public ProtoPackedEntity[] Entities;
		public int Count;
		public int PreviousCount;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetEmpty()
		{
			SetEntities(Array.Empty<ProtoPackedEntity>(),0);
		}
        
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetEntities(ProtoPackedEntity[] entities,int count)
		{
			PreviousCount = Count;
			Count = math.min(count, TargetSelectionData.MaxTargets);

			for (int i = 0; i < Count; i++)
				Entities[i] = entities[i];

			MarkEmpty(Count);
		}
		
		public void AutoReset(ref MultipleTargetsComponent c)
		{
			c.Entities ??= new ProtoPackedEntity[TargetSelectionData.MaxTargets];
			c.Count = 0;
			c.PreviousCount = 0;
			c.MarkEmpty(0);
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void MarkEmpty(int start)
		{
			if (Count == PreviousCount) return;
			
			for (var i = start; i < TargetSelectionData.MaxTargets; i++)
				Entities[i] = Empty;
		}
	}
}