using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.Infrastructure.NHibernate.Repositories;
using CQRS.Sales.Domain;

namespace CQRS.Sales.Infrastructure.Repositories
{
    [DomainRepositoryImplementation]
    public class ClientRepository : GenericRepositoryForBaseEntity<Client>, IClientRepository
    {
    }
}
