using CQRS.Base.DDD.Domain;

namespace CQRS.Sales.Interfaces.Domain.Events
{
    public class OrderCreatedEvent : IDomainEvent
    {
        public int OrderId { get; private set; }

        public OrderCreatedEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}