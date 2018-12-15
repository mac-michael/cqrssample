using CQRS.Base.DDD.Sagas;

namespace CQRS.Sales.Saga
{
    [SagaData]
    public class OrderShipmentStatusTrackerData
    {
        public int Id { get; private set; }

        public int? OrderId { get; set; }
        public int? ShipmentId { get; set; }

        public bool ShipmentReceived { get; set; }
    }
}
