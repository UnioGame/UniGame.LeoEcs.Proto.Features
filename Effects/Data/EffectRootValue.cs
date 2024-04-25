﻿namespace unigame.ecs.proto.Effects.Data
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public struct EffectRootValue
    {
        public EffectRootId id;
        [OnValueChanged(nameof(OnObjectChanged))]
        public GameObject objectValue;
        public string objectName;

        private void OnObjectChanged(GameObject gameObject)
        {
            objectName = gameObject== null ? string.Empty : gameObject.name;
        }
    }
}