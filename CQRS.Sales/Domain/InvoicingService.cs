using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Sales.Interfaces.Domain;

namespace CQRS.Sales.Domain
{
    // Sample Domain Service that contains logic that:
    // Does not have a natural place in any aggregate - we don't want to bloat Order with issuance of the Invoice
    // Has broad dependencies - we don't want Order to become a 'God Class'
    // Is used only (or not many) in one Use Case/user Story so is not essential for any Aggregate
    // 
    // Notice that this Domain Service is managed by Container in order to be able to inject dependencies like Repo  
    [DomainService]
    public class InvoicingService
    {
        public IProductRepository ProductRepository { get; set; }

        public Invoice Issuance(Order order, ITaxPolicy taxPolicy)
        {
            // TODO refactor to InvoiceFactory, testability
            Invoice invoice = new Invoice(order.ClientId);

            foreach (OrderedProduct orderedProduct in order.OrderedProducts)
            {
                Product product = ProductRepository.Load(orderedProduct.ProductId);

                Money net = orderedProduct.EffectiveCost;
                Tax tax = taxPolicy.CalculateTax(product.Type, net);

                InvoiceLine invoiceLine = new InvoiceLine(product, orderedProduct.Quantity, net, tax);
                invoice.AddItem(invoiceLine);
            }

            return invoice;
        }
    }
}