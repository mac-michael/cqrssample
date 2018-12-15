namespace CQRS.Sales.Interfaces.Application.Commands
{
    public class AddProductToOrderCommand
    {
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }

        public AddProductToOrderCommand(int orderId, int productId, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}