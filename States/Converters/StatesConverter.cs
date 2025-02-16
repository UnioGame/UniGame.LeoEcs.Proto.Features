namespace Game.Ecs.State.Converters
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Components;
    using Components.Requests;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using Data;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    /// <summary>
    /// Converter that can be used to apply a state to a GameObject.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class StatesConverter : GameObjectConverter
    {
#if ODIN_INSPECTOR
        [BoxGroup(nameof(states))]
#endif
        public List<StateId> states = new();
        
#if ODIN_INSPECTOR
        [ValueDropdown(nameof(GetStates))]
        [BoxGroup(nameof(states))]
#endif
        public StateId activeState = StateId.Empty;
        
#if ODIN_INSPECTOR
        [PropertySpace]
        [BoxGroup(nameof(behaviours))]
#endif
        public bool addBehaviour = true;
        
#if ODIN_INSPECTOR
        [BoxGroup(nameof(behaviours))]
        [ShowIf(nameof(addBehaviour))]
        [ListDrawerSettings(ListElementLabelName = "@Name")]
#endif
        public List<StateBehaviourData> behaviours = new();

        protected override void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            GameStatesAspect.CreateStatesEntity(entity, world);

            ref var statesMapComponent = ref world.GetOrAddComponent<StatesMapComponent>(entity);
            ref var stateComponent = ref world.GetOrAddComponent<StateComponent>(entity);
            ref var request = ref world.GetOrAddComponent<ChangeStateSelfRequest>(entity);
            
            foreach (var state in states)
                statesMapComponent.States.Add(state);
            
            //set active state with request
            stateComponent.Id = 0;
            request.StateId = activeState;
            
            if(addBehaviour)
                GameStatesAspect.AddStatesBehaviours(entity, world, behaviours);
        }

#if ODIN_INSPECTOR
        
        public IEnumerable<ValueDropdownItem<StateId>> GetStates()
        {
            foreach (var state in states)
            {
                yield return new ValueDropdownItem<StateId>(StateId.GetStateName(state), state);
            }
        }
        
#endif
    }
}