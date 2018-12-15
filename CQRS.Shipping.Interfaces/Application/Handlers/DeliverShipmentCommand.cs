namespace CQRS.Shipping.Interfaces.Application.Handlers
{
    public class DeliverShipmentCommand
    {
        public int ShipmentId { get; private set; }

        public DeliverShipmentCommand(int shipmentId)
        {
            ShipmentId = shipmentId;
        }
    }
}
