namespace CQRS.Sales.Domain
{
    public interface IOrderRepository
    {
        void Save(Order order);
        Order Load(int orderId);
    }
}