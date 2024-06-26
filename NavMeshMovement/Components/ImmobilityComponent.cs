﻿namespace UniGame.Ecs.Proto.Movement.Components
{
    using Leopotam.EcsProto;


    public struct ImmobilityComponent : IProtoAutoReset<ImmobilityComponent>
    {
        public int BlockSourceCounter;
        
        public void AutoReset(ref ImmobilityComponent c)
        {
            c.BlockSourceCounter = 0;
        }
    }
}