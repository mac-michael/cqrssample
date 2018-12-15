using System;
using CQRS.DDD.Domain.Annotations;
using CQRS.Infrastructure;

namespace CQRS.Erp.Sales
{
    [DomainValueObject]
    public class Tax
    {
        public Tax()
        {
            
        }

        public Tax(Money amount, string description)
        {
            Amount = amount;
            Description = description;
        }

        public Money Amount { get; private set; }
        public string Description { get; private set; }
    }
}