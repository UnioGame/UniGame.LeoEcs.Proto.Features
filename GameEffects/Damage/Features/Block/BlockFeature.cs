namespace UniGame.Ecs.Proto.Gameplay.Dodge
{
    using Cysharp.Threading.Tasks;
    using Damage;
    using Damage.Systems;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Proto Features/Damage/Damage Block Feature",fileName = "Damage Block Feature")]
    public class BlockFeature  : DamageSubFeature
    {
        public sealed override UniTask BeforeDamageSystem(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new CheckDamageBlockSystem());
            
            return UniTask.CompletedTask;
        }
    }
}
