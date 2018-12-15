using System;

namespace CQRS.Sales.Interfaces.Domain.Exceptions
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