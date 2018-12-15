using CQRS.Base.DDD.Domain;
using CQRS.Base.DDD.Domain.Support;

namespace CQRS.Base.Infrastructure.NHibernate.Repositories
{
    public class GenericRepositoryForBaseEntity<TEntity> : GenericRepository<TEntity, int>
        where TEntity : Entity
    {
        public InjectorHelper InjectorHelper { get; set; }

        public override TEntity Load(int id)
        {
            TEntity entity = base.Load(id);
            if ( entity is AggregateRoot)
                InjectorHelper.InjectDependencies((AggregateRoot)(object)entity);

            return entity;
        }

        public override void Delete(int id)
        {
            TEntity entity = Load(id);
            entity.MarkAsRemoved();
        }
    }
}