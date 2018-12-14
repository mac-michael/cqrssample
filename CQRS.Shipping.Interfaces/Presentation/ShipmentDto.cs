using CQRS.Shipping.Interfaces.Domain;

namespace CQRS.Shipping.Interfaces.Presentation
{
    public class ShipmentDto
    {
        public int ShipmentId { get; set; }
        public int OrderId { get; set; }
        public ShippingStatus Status { get; set; }

        public ShipmentDto(int shipmentId, int orderId, ShippingStatus status)
        {
            ShipmentId = shipmentId;
            OrderId = orderId;
            Status = status;
        }
    }
}