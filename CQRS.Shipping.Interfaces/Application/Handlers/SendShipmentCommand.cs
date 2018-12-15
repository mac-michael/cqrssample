namespace CQRS.Shipping.Interfaces.Application.Handlers
{
    public class SendShipmentCommand
    {
        public int ShipmentId { get; private set; }

        public SendShipmentCommand(int shipmentId)
        {
            ShipmentId = shipmentId;
        }
    }
}