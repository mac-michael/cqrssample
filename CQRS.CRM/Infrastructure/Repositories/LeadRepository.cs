using System;
using CQRS.Base.Infrastructure.Attributes;
using CQRS.Base.Infrastructure.NHibernate.Repositories;
using CQRS.CRM.Domain;

namespace CQRS.CRM.Infrastructure.Repositories
{
    [Component]
    public class LeadRepository : GenericRepository<Lead, Guid>, ILeadRepository
    {

    }
}