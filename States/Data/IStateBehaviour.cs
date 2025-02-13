namespace Game.Ecs.State.Data
{
    using Leopotam.EcsProto;

    public interface IStateBehaviour
    {
        void Enter(ProtoEntity entity,ProtoWorld world);
        void Update(ProtoEntity entity,ProtoWorld world);
        void Exit(ProtoEntity entity,ProtoWorld world);       
    }
}