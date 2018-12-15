using System;

namespace CQRS.Erp.Sales.Domain.Exceptions
{
    public class OrderCreationException : Exception
    {
        public OrderCreationException(String message, int clientId)
            : base(message)
        {

            ClientId = clientId;
        }

        public int ClientId { get; private set; }

    }
}