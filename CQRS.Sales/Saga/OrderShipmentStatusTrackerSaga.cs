using CQRS.Base.DDD.Infrastructure.Sagas;
using CQRS.Base.DDD.Sagas;
using CQRS.Sales.Domain;
using CQRS.Sales.Interfaces.Domain.Events;
using CQRS.Shipping.Interfaces.Domain.Events;

namespace CQRS.Sales.Saga
{
    public class OrderShipmentStatusTrackerSaga : Saga<OrderShipmentStatusTrackerData>,
        ISagaAction<OrderCreatedEvent>, 
        ISagaAction<OrderSubmittedEvent>,
        ISagaAction<OrderShippedEvent>,
        ISagaAction<ShipmentDeliveredEvent>
    {
        public IOrderRepository OrderRepository { get; set; }

        public void Handle(OrderCreatedEvent @event)
        {
            Data.OrderId = @event.OrderId;
            CompleteIfPossible();
        }

        public void Handle(OrderSubmittedEvent @event)
        {
            Data.OrderId = @event.OrderId;
            // do some business
            CompleteIfPossible();
        }

        public void Handle(OrderShippedEvent @event)
        {
            Data.OrderId = @event.OrderId;
            Data.ShipmentId = @event.ShipmentId;
            CompleteIfPossible();
        }

        public void Handle(ShipmentDeliveredEvent @event)
        {
            Data.ShipmentId = @event.ShipmentId;
            Data.ShipmentReceived = true;
            CompleteIfPossible();
        }

        private void CompleteIfPossible()
        {
            if (Data.OrderId != null && Data.ShipmentId != null && Data.ShipmentReceived)
            {
                Order shippedOrder = OrderRepository.Load(Data.OrderId.Value);
                shippedOrder.Archive();
                //OrderRepository.Save(shippedOrder);
                MarkAsCompleted();
            }
        }
    }
}