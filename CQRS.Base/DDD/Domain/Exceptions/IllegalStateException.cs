using System;

namespace CQRS.Base.DDD.Domain.Exceptions
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