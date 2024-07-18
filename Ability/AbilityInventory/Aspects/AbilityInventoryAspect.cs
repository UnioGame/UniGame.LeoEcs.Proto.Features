﻿namespace UniGame.Ecs.Proto.AbilityInventory.Aspects
{
    using System;
    using Ability.Common.Components;
    using Ability.Components;
    using Ability.SubFeatures.AbilityAnimation.Components;
    using Components;
    using Equip.Components;
    using Game.Code.Configuration.Runtime.Ability.Description;
    using Game.Code.Services.AbilityLoadout.Data;
    using Game.Ecs.Core.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    [ECSDI]
    public class AbilityInventoryAspect : EcsAspect
    {
        public ProtoWorld world;
        public AbilityMetaAspect abilityMetaAspect;
        
        public ProtoPool<AbilityIdComponent> Id;
        public ProtoPool<AbilityEquipComponent> AbilityEquip;
        public ProtoPool<AbilityBuildingComponent> Building;
        public ProtoPool<AbilityBlockedComponent> Blocked;
        public ProtoPool<AbilityVisualComponent> Visual;
        public ProtoPool<AbilitySlotComponent> Slot;
        public ProtoPool<UserInputAbilityComponent> Input;
        public ProtoPool<OwnerComponent> Owner;
        public ProtoPool<OwnerLinkComponent> OwnerLink;
        public ProtoPool<AbilityActiveAnimationComponent> Animation;
        public ProtoPool<AbilityMetaLinkComponent> MetaLink;
        public ProtoPool<AbilityConfigurationComponent> Configuration;
        public ProtoPool<AbilityBuildingProcessingComponent> Processing;
        public ProtoPool<AbilityLoadingComponent> Loading;
        public ProtoPool<AbilityInventoryCompleteComponent> Complete;
        public ProtoPool<AbilityValidationFailedComponent> Failed;
        public ProtoPool<AbilityInventoryProfileComponent> ProfileTarget;

        //requests
        public ProtoPool<EquipAbilityIdSelfRequest> EquipById;
        public ProtoPool<EquipAbilityIdToChampionRequest> EquipToChampion;
        public ProtoPool<EquipAbilityNameSelfRequest> EquipByName;
        public ProtoPool<EquipAbilityReferenceSelfRequest> EquipByReference;
        public ProtoPool<EquipAbilitySelfRequest> Equip;
        public ProtoPool<LoadAbilityMetaRequest> LoadMeta;
        
        //event
        public ProtoPool<AbilityEquipChangedEvent> EquipChanged;
        
        
        public int Convert(AbilityItemData itemData,int entity)
        {
            var data = itemData.data;
			
            ref var abilityIdComponent = ref abilityMetaAspect.Id.GetOrAddComponent(entity);
            ref var abilityMetaComponent = ref abilityMetaAspect.Meta.GetOrAddComponent(entity);
            ref var abilityConfigurationComponent = ref abilityMetaAspect.ConfigurationReference.GetOrAddComponent(entity);
            ref var visualDescriptionComponent = ref abilityMetaAspect.Visual.GetOrAddComponent(entity);
            ref var nameComponent = ref abilityMetaAspect.Name.GetOrAddComponent(entity);
            ref var abilitySlotTypeComponent = ref Slot.GetOrAddComponent(entity);
			
            abilityConfigurationComponent.AbilityConfiguration = itemData.configurationReference;

            var visualDescription = itemData.visualDescription;
            visualDescriptionComponent.Name = visualDescription.Name;
            visualDescriptionComponent.Description = visualDescription.Description;
            visualDescriptionComponent.ManaCost = visualDescription.manaCost;
            visualDescriptionComponent.Icon = visualDescription.icon;
	
            nameComponent.Value = visualDescription.Name;
            abilityMetaComponent.AbilityId = itemData.id;
            abilityMetaComponent.SlotType = data.slotType;
            abilityMetaComponent.Hide = data.isHidden;
            abilityMetaComponent.IsBlocked = data.isBlock;
			
            if (data.isHidden)
                world.AddComponent<AbilityInventoryHideComponent>(entity);
			
            if (data.isBlock)
                abilityMetaAspect.Blocked.Add(entity);
			
            abilityIdComponent.AbilityId = (AbilityId)itemData.id;
            abilitySlotTypeComponent.SlotType = data.slotType;

            return entity;
        }
    }
}