﻿namespace UniGame.Ecs.Proto.GameResources.Components
{
    using System;
    using Data;
    using Leopotam.EcsProto;
    using UnityEngine;

    /// <summary>
    /// Компонент хранит данные необходимые для спавна новой сущности в мире
    /// </summary>
    [Serializable]
    public struct GameSpawnDataComponent : IProtoAutoReset<GameSpawnDataComponent>
    {
        /// <summary>
        /// spawn location data
        /// </summary>
        public GamePoint LocationData;

        /// <summary>
        /// spawn parent object
        /// </summary>
        public Transform Parent;
        
        public void AutoReset(ref GameSpawnDataComponent c)
        {
            c.Parent = null;
            c.LocationData = new GamePoint();
        }
    }
}