using NHibernate;

namespace CQRS.Base.Infrastructure.NHibernate.Repositories
{
    public class GenericRepository<TEntity, TKey> where TEntity : class
    {
        public IEntityManager EntityManager { get; set; }

        public virtual TEntity Load(TKey id)
        {
            return EntityManager.CurrentSession.Get<TEntity>(id);
        }

        public virtual void Delete(TKey id)
        {
            EntityManager.CurrentSession.Delete(Load(id));
        }

        public virtual void Save(TEntity entity)
        {
            EntityManager.CurrentSession.SaveOrUpdate(entity);
            //EntityManager.CurrentSession.Lock(entity, LockMode.Write);
        }
    }
}
