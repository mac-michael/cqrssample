using CQRS.Base.CQRS.Commands.Handler;
using CQRS.Shipping.Domain;
using CQRS.Shipping.Infrastructure;
using CQRS.Shipping.Interfaces.Application.Handlers;

namespace CQRS.Shipping.Application.Commands.Handlers
{
    public class DeliverShipmentCommandHandler : ICommandHandler<DeliverShipmentCommand>
    {
        public ShipmentRepository Repository { get; set; }

        public void Handle(DeliverShipmentCommand command)
        {
            Shipment shipment = Repository.Load(command.ShipmentId);
            shipment.Deliver();
        }
    }
}