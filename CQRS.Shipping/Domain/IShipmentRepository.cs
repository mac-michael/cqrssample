namespace CQRS.Shipping.Domain
{
    public interface IShipmentRepository
    {
        void Save(Shipment order);
        Shipment Load(int orderId);
    }
}