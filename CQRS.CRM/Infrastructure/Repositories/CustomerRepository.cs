using CQRS.Base.Infrastructure.NHibernate.Repositories;
using CQRS.CRM.Domain;

namespace CQRS.CRM.Infrastructure.Repositories
{
    internal class CustomerRepository : GenericRepositoryForBaseEntity<Customer>, ICustomerRepository
    {
    }
}