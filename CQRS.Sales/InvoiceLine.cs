using CQRS.Infrastructure;

namespace CQRS.Erp.Sales
{
    public class InvoiceLine : Entity
    {
        public InvoiceLine()
        {
            
        }
        public InvoiceLine(Product product, int quantity, Money net, Tax tax)
        {
            Product = product;
            Quantity = quantity;
            Net = net;
            Tax = tax;
            Gross = net + tax.Amount;
        }

        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public Money Net { get; private set; }
        public Money Gross { get; private set; }
        public Tax Tax { get; private set; }


        // Temp, nHibernate
        public Invoice Invoice { get; set; }
    }
}