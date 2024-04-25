﻿namespace unigame.ecs.proto.GameEffects.FreezeEffect.Components
{
    using Leopotam.EcsProto.QoL;


    public struct ApplyFreezeTargetEffectRequest
    {
        public ProtoPackedEntity Source;
        public ProtoPackedEntity Destination;
        public float DumpTime;
    }
}