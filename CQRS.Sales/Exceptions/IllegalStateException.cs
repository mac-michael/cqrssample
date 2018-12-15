using System;

namespace CQRS.Erp.Sales.Domain.Exceptions
{
    public class IllegalStateException : Exception
    {
        public IllegalStateException(string message) : base(message)
        {
        }

        public IllegalStateException()
        {
        }
    }
}