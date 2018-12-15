using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.DDD.Domain.SharedKernel;

namespace CQRS.Sales.Domain
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