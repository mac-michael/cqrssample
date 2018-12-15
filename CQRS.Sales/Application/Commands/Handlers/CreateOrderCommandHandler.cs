using CQRS.Base.CQRS.Commands.Handler;
using CQRS.Base.DDD.Application;
using CQRS.Sales.Domain;
using CQRS.Sales.Interfaces.Application.Commands;
using CQRS.Sales.Interfaces.Application.Events;

namespace CQRS.Sales.Application.Commands.Handlers
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        public OrderFactory OrderFactory { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IClientRepository ClientRepository { get; set; }
        public ISystemUser SystemUser { get; set; }

        public IApplicationEventPublisher ApplicationEventPublisher { get; set; }

        public void Handle(CreateOrderCommand command) 
        {
            Client currentClient = ClientRepository.Load(SystemUser.UserId);

            Order order = OrderFactory.CrateOrder(currentClient);
		
            foreach (var productIdWithCount in command.ProductIdsWithCounts)
            {
                int productId = productIdWithCount.Key;
                int count = productIdWithCount.Value;
			
                order.AddProduct(ProductRepository.Load(productId), count);
			
                ApplicationEventPublisher.Publish(new ProductAddedToOrderEvent(productId, SystemUser.UserId, count));
            }

            OrderRepository.Save(order);

            command.OrderId = order.Id;
        }
    }
}