using CQRS.Base.CQRS.Commands.Handler;
using CQRS.Base.DDD.Application;
using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Base.DDD.Domain.SharedKernel.Specification;
using CQRS.Sales.Domain;
using CQRS.Sales.Domain.Policies.Tax;
using CQRS.Sales.Domain.Specification.Order;
using CQRS.Sales.Interfaces.Application.Commands;
using CQRS.Sales.Interfaces.Domain.Exceptions;

namespace CQRS.Sales.Application.Commands.Handlers
{
    public class SubmitOrderCommandHandler : ICommandHandler<SubmitOrderCommand>
    {
        public IOrderRepository OrderRepository { get; set; }
        public IInvoiceRepository InvoiceRepository { get; set; }
        public InvoicingService InvoicingService { get; set; }
        public SystemUser SystemUser { get; set; }

        public void Handle(SubmitOrderCommand command)
        {
            Order order = OrderRepository.Load(command.OrderId);

            ISpecification<Order> orderSpecification = GenerateSpecification(SystemUser);
            if (!orderSpecification.IsSatisfiedBy(order))
                throw new OrderOperationException("Order does not meet specification", order.Id);

            //Domain logic
            order.Submit();
        
            //Domain service
            Invoice invoice = InvoicingService.Issuance(order, GenerateTaxPolicy(SystemUser));

            OrderRepository.Save(order);
            InvoiceRepository.Save(invoice);
        }

        
        // Assembling Spec contains Business Logic, therefore it may be moved to domain Building Block - OrderSpecificationFactory 
        private ISpecification<Order> GenerateSpecification(SystemUser systemUser)
        {
            ISpecification<Order> specification = new ConjunctionSpecification<Order>(
                new DestinationSpecification(Locale.China), //.not(), // - do not send to China
                new ItemsCountSpecification(100), //max 100 items
                new DebtorSpecification() //not debts or max 1000  => debtors can buy for max 1000
                    .Or(new TotalCostSpecification(new Money(1000.0))));

            //vip can buy some nice stuff
            if (!IsVip(systemUser))
                specification = specification.And(new RestrictedProductsSpecification());

            return specification;
        }

        private bool IsVip(SystemUser systemUser)
        {
            // TODO 
            return false;
        }

        private ITaxPolicy GenerateTaxPolicy(SystemUser systemUser)
        {
            // TODO determine tax based on user's location
            return new DefaultTaxPolicy();
        }
    }
}
