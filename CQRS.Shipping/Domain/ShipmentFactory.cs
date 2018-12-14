using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.DDD.Domain.Support;
using CQRS.Sales.Interfaces.Presentation;

namespace CQRS.Shipping.Domain
{
    [DomainFactory]
    public class ShipmentFactory
    {
        public InjectorHelper Helper { get; set; }

        public Shipment CreateShipment(ClientOrderDetailsDto orderDetails)
        {
            Shipment shipment = new Shipment(orderDetails.OrderId);
            Helper.InjectDependencies(shipment);
            return shipment;
        }
    }
}