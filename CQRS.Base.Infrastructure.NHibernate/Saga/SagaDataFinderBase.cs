using System.Linq;
using CQRS.Base.DDD.Infrastructure.Sagas;
using NHibernate.Linq;

namespace CQRS.Base.Infrastructure.NHibernate.Saga
{
    public class SagaDataFinderBase<TSagaData> : ISagaDataFinderBase<TSagaData>
        where TSagaData : class, new()
    {
        public IEntityManager EntityManager { get; set; }

        protected IQueryable<TSagaData> SagaData
        {
            get { return EntityManager.CurrentSession.Query<TSagaData>(); }
        }

        public virtual void RemoveSagaData(TSagaData data)
        {
            var mergedData = EntityManager.CurrentSession.Merge(data);
            EntityManager.CurrentSession.Delete(mergedData);
        }

        public virtual TSagaData CreateNewSagaData()
        {
            var data = new TSagaData();
            EntityManager.CurrentSession.Save(data);
            return data;
        }
    }
}