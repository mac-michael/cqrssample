using NHibernate;

namespace CQRS.Base.Infrastructure.NHibernate
{
    public interface IEntityManager
    {
        ISession CurrentSession { get; }
    }
}