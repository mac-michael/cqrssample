using System;

namespace CQRS.Sales.Interfaces.Domain.Exceptions
{
    public class OrderOperationException : Exception
    {
        public OrderOperationException(string message, int? clientId, int orderId)
            : base(message)
        {
            ClientId = clientId;
            OrderId = orderId;
        }

        public OrderOperationException(String mesage, int orderId) :
            this(mesage, null, orderId)
        {
        }

        public int? ClientId { get; private set; }
        public int OrderId { get; private set; }
    }
}
