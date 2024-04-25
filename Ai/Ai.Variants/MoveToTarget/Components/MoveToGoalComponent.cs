﻿namespace unigame.ecs.proto.GameAi.MoveToTarget.Components
{
    using System.Collections.Generic;
    using Data;
     

    public struct MoveToGoalComponent : IProtoAutoReset<MoveToGoalComponent>
    {
        public List<MoveToGoalData> Goals;
        
        public void AutoReset(ref MoveToGoalComponent c)
        {
            c.Goals ??= new List<MoveToGoalData>(8);
            c.Goals.Clear();
        }
    }
}