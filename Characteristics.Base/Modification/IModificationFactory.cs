﻿namespace unigame.ecs.proto.Characteristics.Base.Modification
{
    using System;

    public interface IModificationFactory
    {
        Type TargetType { get; }
        
        ModificationHandler Create(float value);
    }
}