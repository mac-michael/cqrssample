namespace CQRS.Base.DDD.Infrastructure.Sagas
{
    public abstract class IFindSagas<TSagaData>
    {
        public interface Using<in TMessage>
        {
            TSagaData FindBy(TMessage message);
        }
    }
}