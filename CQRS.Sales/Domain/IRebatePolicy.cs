using CQRS.Base.DDD.Domain.SharedKernel;

namespace CQRS.Sales.Domain
{
    public interface IRebatePolicy
    {
        Money CalculateRebate(Product product, int quantity, Money regularCost);
    }
}