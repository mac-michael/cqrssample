using CQRS.Base.DDD.Infrastructure.Events;
using CQRS.Sales.Interfaces.Domain.Events;
using CQRS.Sales.Interfaces.Presentation;
using CQRS.Shipping.Domain;

namespace CQRS.Shipping.Application.Events
{
    [EventListeners]
    public class OrderSubmittedForShippingListener : IEventListener<OrderSubmittedEvent>
    {
        public ShipmentFactory Factory { get; set; }
        public IOrderFinder OrderFinder { get; set; }
        public IShipmentRepository Repository { get; set; }

        [EventListener]
        public void Handle(OrderSubmittedEvent orderSumbiteEvent)
        {
            ClientOrderDetailsDto orderDetails = OrderFinder
                .GetClientOrderDetails(orderSumbiteEvent.OrderId);

            Shipment shipment = Factory.CreateShipment(orderDetails);
            Repository.Save(shipment);
        }
    }
}