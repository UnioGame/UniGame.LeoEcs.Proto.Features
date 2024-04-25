﻿using System;

namespace unigame.ecs.proto.GameEffects.ModificationEffect.Components
{
    using System.Collections.Generic;
    using Characteristics.Base.Modification;
    using Leopotam.EcsProto;


    [Serializable]
    public struct ModificationEffectComponent : IProtoAutoReset<ModificationEffectComponent>
    {
        public List<ModificationHandler> ModificationHandlers;
        
        public void AutoReset(ref ModificationEffectComponent c)
        {
            c.ModificationHandlers ??= new List<ModificationHandler>();
            c.ModificationHandlers.Clear();
        }
    }
    
    [Serializable]
    public struct SingleModificationEffectComponent : IProtoAutoReset<SingleModificationEffectComponent>
    {
        public ModificationHandler Value;
        
        public void AutoReset(ref SingleModificationEffectComponent c)
        {
            c.Value = null;
        }
    }
}