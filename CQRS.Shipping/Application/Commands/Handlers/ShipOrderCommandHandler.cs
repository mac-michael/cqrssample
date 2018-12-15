using CQRS.Base.CQRS.Commands.Handler;
using CQRS.Shipping.Domain;
using CQRS.Shipping.Infrastructure;
using CQRS.Shipping.Interfaces.Application.Handlers;

namespace CQRS.Shipping.Application.Commands.Handlers
{
    public class ShipOrderCommandHandler : ICommandHandler<SendShipmentCommand>
    {
        public ShipmentRepository Repository { get; set; }

        public void Handle(SendShipmentCommand command)
        {
            Shipment shipment = Repository.Load(command.ShipmentId);
            shipment.Ship();
        }
    }
}