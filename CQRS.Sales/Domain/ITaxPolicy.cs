using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.DDD.Domain.SharedKernel;

namespace CQRS.Sales.Domain
{
    [DomainPolicy]
    public interface ITaxPolicy
    {
        Tax CalculateTax(ProductType productType, Money net);
    }
}