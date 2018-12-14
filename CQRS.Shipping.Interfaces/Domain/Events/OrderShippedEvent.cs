using CQRS.Base.DDD.Domain;

namespace CQRS.Shipping.Interfaces.Domain.Events
{
    public class OrderShippedEvent : IDomainEvent
    {
        public OrderShippedEvent(int orderId, int shipmentId)
        {
            OrderId = orderId;
            ShipmentId = shipmentId;
        }

        public int OrderId { get; private set; }
        public int ShipmentId { get; private set; }
    }
}
