using CQRS.Infrastructure;

namespace CQRS.Erp.Sales.Domain
{
    public interface IRebatePolicy
    {
        Money CalculateRebate(Product product, int quantity, Money regularCost);
    }
}