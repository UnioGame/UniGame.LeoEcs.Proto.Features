﻿namespace unigame.ecs.proto.Effects.Systems
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Components;
     
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;
#endif
    
#if ENABLE_IL2CP
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ValidateEffectsListSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        private List<ProtoPackedEntity> _cacheList = new List<ProtoPackedEntity>();

        private EffectAspect _effectAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<EffectsListComponent>().End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var list = ref _effectAspect.List.Get(entity);
                _cacheList.Clear();
                _cacheList.AddRange(list.Effects);
                
                list.Effects.Clear();
                
                foreach (var effectPackedEntity in _cacheList)
                {
                    if(!effectPackedEntity.Unpack(_world, out _))
                        continue;
                    
                    list.Effects.Add(effectPackedEntity);
                }
            }
        }
    }
}