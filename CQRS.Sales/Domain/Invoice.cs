using System.Collections.Generic;
using CQRS.Base.DDD.Domain;
using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Base.Infrastructure.NHibernate;

namespace CQRS.Sales.Domain
{
    public class Invoice : AggregateRoot
    {
        private IList<InvoiceLine> _items;

        public Invoice()
        {
        }
        public Invoice(int clientId)
        {
            ClientId = clientId;
            _items = new List<InvoiceLine>();

            Net = Money.Zero;
            Gross = Money.Zero;
        }

        public int ClientId { get; private set; }
        public Money Net { get; private set; }
        public Money Gross { get; private set; }

        public void AddItem(InvoiceLine item)
        {
            _items.Add(item);
            item.Invoice = this;

            Net = Net + item.Net;
            Gross = Gross + item.Gross;}

        // Returns immutable projection
        public IEnumerable<InvoiceLine> Items
        {
            get { return _items; }
        }
    }

}