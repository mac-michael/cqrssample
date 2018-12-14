namespace CQRS.Base.DDD.Sagas
{
    public interface ISagaManager
    {
        void ProcessMessage<T>(T message);
    }
}