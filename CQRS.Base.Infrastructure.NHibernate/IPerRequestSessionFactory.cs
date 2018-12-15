using NHibernate;

namespace CQRS.Base.Infrastructure.NHibernate
{
    public interface IPerRequestSessionFactory
    {
        ISession Create();
    }
}