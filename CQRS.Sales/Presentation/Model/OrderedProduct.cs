using System;

namespace CQRS.Sales.Presentation.Model
{
    public class OrderedProductProjection
    {
        public int Id { get; set; }

        public int ProductId { get; private set; }
        public int ClientId { get; private set; }

        public DateTime EventDate { get; private set; }
        public int Quantity { get; private set; }

        protected OrderedProductProjection()
        {
            
        }
        public OrderedProductProjection(int productId, int clientId, int quantity, DateTime eventDate)
        {
            ProductId = productId;
            ClientId = clientId;
            EventDate = eventDate;
            Quantity = quantity;
        }
    }
}
