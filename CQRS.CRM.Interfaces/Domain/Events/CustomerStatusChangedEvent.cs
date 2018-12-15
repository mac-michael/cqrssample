using CQRS.Base.DDD.Domain;
using CQRS.Base.DDD.Domain.Annotations;

namespace CQRS.CRM.Interfaces.Domain.Events
{
    [DomainEvent]
    public class CustomerStatusChangedEvent : IDomainEvent
    {
        public int CustomerId { get; private set; }
        public CustomerStatus Status { get; private set; }

        public CustomerStatusChangedEvent(int customerId, CustomerStatus status)
        {
            CustomerId = customerId;
            Status = status;
        }
    }
}
