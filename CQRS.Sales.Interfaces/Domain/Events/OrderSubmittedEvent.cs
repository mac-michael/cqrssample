using CQRS.Base.DDD.Domain;

namespace CQRS.Sales.Interfaces.Domain.Events
{
    public class OrderSubmittedEvent : IDomainEvent
    {
        public int OrderId { get; private set; }

        public OrderSubmittedEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}