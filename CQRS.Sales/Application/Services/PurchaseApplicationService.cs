using CQRS.Base.DDD.Application;
using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Base.DDD.Domain.SharedKernel.Specification;
using CQRS.Sales.Domain;
using CQRS.Sales.Domain.Policies.Tax;
using CQRS.Sales.Domain.Specification.Order;
using CQRS.Sales.Interfaces.Application.Events;
using CQRS.Sales.Interfaces.Domain.Exceptions;

namespace CQRS.Sales.Application.Services
{
    public class PurchaseApplicationService
    {
        public IOrderRepository OrderRepository { get;  set; }
        public OrderFactory OrderFactory { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IInvoiceRepository InvoiceRepository { get; set; }
        public InvoicingService InvoicingService { get; set; }
        public ISystemUser SystemUser { get; set; }
        public IApplicationEventPublisher EventPublisher { get; set; }

        // Sample usage of factory and repository
        public void CreateNewOrder()
        {
            Client client = LoadClient(SystemUser.UserId);
            Order order = OrderFactory.CrateOrder(client);

            OrderRepository.Save(order);
        }

        // Sample call of the domain logic
        // Sample publishing Application (not Domain) Event
        public void AddProductToOrder(int productId, int orderId, int quantity)
        {
            Order order = OrderRepository.Load(orderId);
            Product product = ProductRepository.Load(productId);

            // Domain logic
            order.AddProduct(product, quantity);

            //OrderRepository.Save(order);

            // if we want to Spy Clients:)
            EventPublisher.Publish(new ProductAddedToOrderEvent(product.Id, SystemUser.UserId, quantity));
        }

        // Sample of the separation of domain logic in aggregate and domain logic in domain service
        public void ApproveOrder(int orderId)
        {
            Order order = OrderRepository.Load(orderId);

            ISpecification<Order> orderSpecification = GenerateSpecification(SystemUser);
            if (!orderSpecification.IsSatisfiedBy(order))
                throw new OrderOperationException("Order does not meet specification", order.Id);

            // Domain logic
            order.Submit();
        
            // Domain service
            Invoice invoice = InvoicingService.Issuance(order, GenerateTaxPolicy(SystemUser));

            InvoiceRepository.Save(invoice);
            OrderRepository.Save(order);
        }

        // Assembling Spec contains Business Logic, therefore it may be moved to
        // domain Building Block - OrderSpecificationFactory
        private ISpecification<Order> GenerateSpecification(ISystemUser systemUser)
        {
            ISpecification<Order> specification = 
                new ConjunctionSpecification<Order>( //
                    new DestinationSpecification(Locale.China).Not(), // do not send to China
                    new ItemsCountSpecification(100), // max 100 items
                    new DebtorSpecification() // not debts or max 1000 => debtors can
                        // buy for max 1000
                        .Or(new TotalCostSpecification(new Money(1000.0))));

            // vip can buy some nice stuff
            if (!IsVip(systemUser))
            {

                specification = specification.And(new RestrictedProductsSpecification());
            }

            return specification;
        }

        private bool IsVip(ISystemUser systemUser)
        {
            // TODO 
            return false;
        }

        private ITaxPolicy GenerateTaxPolicy(ISystemUser systemUser)
        {
            // TODO determine tax based on user's location
            return new DefaultTaxPolicy();
        }

        private Client LoadClient(int userId)
        {
            // TODO Auto-generated method stub
            return new Client();
        }
    }
}
