using System.Collections.Generic;
using CQRS.Base.DDD.Domain;
using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Base.DDD.Domain.Support;
using CQRS.Sales.Interfaces.Domain;
using CQRS.Sales.Interfaces.Domain.Exceptions;

namespace CQRS.Sales.Domain
{
    [DomainFactory]
    public class OrderFactory
    {
        public RebatePolicyFactory RebatePolicyFactory { get; set; }
        public InjectorHelper Injector { get; set; }

        public Order CrateOrder(Client client)
        {
            CheckIfClientCanPerformPurchase(client);

            Order order = new Order(client.Id, Money.Zero, OrderStatus.Draft);
            Injector.InjectDependencies(order);

            IRebatePolicy rebatePolicy = RebatePolicyFactory.CreateRebatePolicy();
            order.SetRebatePolicy(rebatePolicy);

            AddGratis(order, client);

            return order;
        }

        private void CheckIfClientCanPerformPurchase(Client client)
        {
            if (client.Status != EntityStatus.Active)
                throw new OrderCreationException("Can not perform purchase, because of the Client status: "
                                                 + client.Status, client.Id);
        }

        // TODO introduce Domain Class: GatisProducts that contains product and quantity
        private void AddGratis(Order order, Client client)
        {
            List<Product> gratisProducts = LoadGratis(client);

            foreach (Product product in gratisProducts)
                order.AddProduct(product, 1);
        }

        // TODO load gratis form GratsService/Repo
        private List<Product> LoadGratis(Client client)
        {
            return new List<Product>(0);
        }
    }
}