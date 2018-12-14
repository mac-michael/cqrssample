namespace CQRS.Base.DDD.Infrastructure.Sagas
{
    public interface ISagaDataFinderBase<TSagaData>
    {
        void RemoveSagaData(TSagaData data);
        TSagaData CreateNewSagaData();
    }
}