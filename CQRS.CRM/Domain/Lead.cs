using System;
using CQRS.Base.DDD.Domain.Annotations;

namespace CQRS.CRM.Domain
{
    [DomainAggregateRoot]
    public class Lead
    {
        public Guid Id { get; set; }
        public DateTime Version { get; private set; }

        public string Name { get; set; }
    }
}