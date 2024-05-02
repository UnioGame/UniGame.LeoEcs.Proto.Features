namespace UniGame.Ecs.Proto.Presets
{
    using UniGame.Ecs.Proto.Presets.Directional_Light.Systems;
    using UniGame.Ecs.Proto.Presets.FogShaderSettings.Systems;
    using UniGame.Ecs.Proto.Presets.SpotLightSettings.Systems;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Proto Features/Game Presets Feature", fileName = "Game Presets Feature")]
    public class PresetsFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            //find active preset and target by id
            ecsSystems.Add(new FindPresetTargetsSystem());
            ecsSystems.Add(new CalculatePresetProgressSystem());

            //apply material preset to target
            ecsSystems.Add(new ApplyMaterialPresetToTargetSystem());

            //apply rendering settings preset to target
            ecsSystems.Add(new ApplyRenderingSettingsPresetSystem());

            //apply fog shader preset in game.
            ecsSystems.Add(new ApplyFogShaderSettingsPresetSystem());

            //apply spot light preset in game.
            ecsSystems.Add(new ApplySpotLightSettingsPresetSystem());
            
            //apply directional light preset in game.
            ecsSystems.Add(new ApplyDirectionalLightSettingsPresetSystem());

            //apply light preset to target
            ecsSystems.Add(new ApplyLightPresetSystem());

            //disable already activated presets
            //for new activation ActivePresetSourceComponent should be added on preset
            ecsSystems.Add(new DisableActivatedPresetsSystem());

            //if progress is completed when remove active status
            ecsSystems.Add(new CompletePresetProgressSystem());

            return UniTask.CompletedTask;
        }
    }
}