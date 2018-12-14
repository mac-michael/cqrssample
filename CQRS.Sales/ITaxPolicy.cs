using System.Collections.Generic;
using CQRS.DDD.Domain.Annotations;
using CQRS.Infrastructure;

namespace CQRS.Erp.Sales
{
    [DomainPolicy]
    public interface ITaxPolicy
    {
        Tax CalculateTax(ProductType productType, Money net);
    }
}