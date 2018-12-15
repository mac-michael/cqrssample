using System.Linq;
using CQRS.Base.DDD.Infrastructure.Sagas;
using CQRS.Base.Infrastructure.Attributes;
using CQRS.Base.Infrastructure.NHibernate.Saga;
using CQRS.Sales.Interfaces.Domain.Events;
using CQRS.Shipping.Interfaces.Domain.Events;

namespace CQRS.Sales.Saga
{
    [Component]
    public class OrderShipmentStatusTrackerSagaFinder :
        SagaDataFinderBase<OrderShipmentStatusTrackerData>,
        IFindSagas<OrderShipmentStatusTrackerData>.Using<OrderCreatedEvent>,
        IFindSagas<OrderShipmentStatusTrackerData>.Using<OrderSubmittedEvent>,
        IFindSagas<OrderShipmentStatusTrackerData>.Using<OrderShippedEvent>,
        IFindSagas<OrderShipmentStatusTrackerData>.Using<ShipmentDeliveredEvent>
    {
        public OrderShipmentStatusTrackerData FindBy(OrderCreatedEvent message)
        {
            return FindByOrderId(message.OrderId);
        }

        public OrderShipmentStatusTrackerData FindBy(OrderSubmittedEvent message)
        {
            return FindByOrderId(message.OrderId);
        }

        public OrderShipmentStatusTrackerData FindBy(OrderShippedEvent message)
        {
            return FindByShipmentId(message.ShipmentId);
        }

        public OrderShipmentStatusTrackerData FindBy(ShipmentDeliveredEvent message)
        {
            return FindByShipmentId(message.ShipmentId);
        }

        private OrderShipmentStatusTrackerData FindByOrderId(int orderId)
        {
            return SagaData.FirstOrDefault(d => d.OrderId == orderId);
        }

        private OrderShipmentStatusTrackerData FindByShipmentId(int shipmentId)
        {
            return SagaData.Where(d => d.ShipmentId == shipmentId).FirstOrDefault();
        }
    }
}