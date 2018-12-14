using CQRS.Base.CQRS.Commands.Handler;
using CQRS.Sales.Domain;
using CQRS.Sales.Interfaces.Application.Commands;

namespace CQRS.Sales.Application.Commands.Handlers
{
    public class AddProductToOrderCommandHandler : ICommandHandler<AddProductToOrderCommand>
    {
        public IOrderRepository OrderRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }

        public void Handle(AddProductToOrderCommand command)
        {
            Product product = ProductRepository.Load(command.ProductId);
            Order order = OrderRepository.Load(command.OrderId);

            order.AddProduct(product, command.Quantity);

            //OrderRepository.Save(order);
        }
    }
}
