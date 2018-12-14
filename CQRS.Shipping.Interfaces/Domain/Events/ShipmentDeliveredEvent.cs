using CQRS.Base.DDD.Domain;

namespace CQRS.Shipping.Interfaces.Domain.Events
{
    public class ShipmentDeliveredEvent : IDomainEvent
    {
        public int ShipmentId { get; private set; }

        public ShipmentDeliveredEvent(int shipmentId)
        {
            ShipmentId = shipmentId;
        }
    }
}