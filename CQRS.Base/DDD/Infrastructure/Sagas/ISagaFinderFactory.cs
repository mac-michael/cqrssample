namespace CQRS.Base.DDD.Infrastructure.Sagas
{
    public interface ISagaFinderFactory
    {
        IFindSagas<TSagaData>.Using<TMessage> Create<TSagaData, TMessage>();
        TSaga CreateSaga<TSaga>();
    }
}