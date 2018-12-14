using CQRS.Base.DDD.Domain;
using CQRS.Base.DDD.Domain.Exceptions;
using CQRS.Shipping.Interfaces.Domain;
using CQRS.Shipping.Interfaces.Domain.Events;

namespace CQRS.Shipping.Domain
{
    public class Shipment : AggregateRoot
    {
        public int OrderId { get; private set; }

        public ShippingStatus ShipmentStatus { get; private set; }

        private Shipment()
        {
        }

        public Shipment(int orderId)
        {
            OrderId = orderId;
            ShipmentStatus = ShippingStatus.Waiting;
        }

        /**
         * Shipment has been sent to the customer.
         */

        public void Ship()
        {
            if (ShipmentStatus != ShippingStatus.Waiting)
            {
                throw new IllegalStateException("Cannot ship in status " + ShipmentStatus);
            }
            ShipmentStatus = ShippingStatus.Sent;
            EventPublisher.Publish(new OrderShippedEvent(OrderId, Id));
        }

        /**
         * Shipment has been confirmed received by the customer.
         */

        public void Deliver()
        {
            if (ShipmentStatus != ShippingStatus.Sent)
            {
                throw new IllegalStateException("cannot deliver in status " + ShipmentStatus);
            }
            ShipmentStatus = ShippingStatus.Delivered;
            EventPublisher.Publish(new ShipmentDeliveredEvent(Id));
        }
    }
}