﻿namespace unigame.ecs.proto.Characteristics.Feature
{
    using System;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public abstract class CharacteristicFeature<TFeature> : BaseLeoEcsFeature
        where TFeature : CharacteristicEcsFeature,new()
    {
        private TFeature _feature = new TFeature();
        
        public sealed override UniTask InitializeFeatureAsync(IProtoSystems ecsSystems)
        {
            return _feature.InitializeFeatureAsync(ecsSystems);
        }
    }
    
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public abstract class CharacteristicEcsFeature : EcsFeature
    {
        
    }
}