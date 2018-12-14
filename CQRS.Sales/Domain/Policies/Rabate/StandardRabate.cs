using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.DDD.Domain.SharedKernel;

namespace CQRS.Sales.Domain.Policies.Rabate
{
    [DomainPolicyImplementation]
    public class StandardRebate : IRebatePolicy
    {
        private readonly decimal _rebateRatio;
        private readonly int _minimalQuantity;

        /// <param name="rebate">value of the rebate in %, TODO: impement Percent VO</param>
        /// <param name="minimalQuantity">minimal quantity of the purchase that allows rebate</param>
        public StandardRebate(double rebate, int minimalQuantity)
        {
            _rebateRatio = new decimal(rebate/100);
            _minimalQuantity = minimalQuantity;
        }

        public Money CalculateRebate(Product product, int quantity, Money regularCost)
        {
            if (quantity >= _minimalQuantity)
                return regularCost.MultiplyBy(_rebateRatio);
            return Money.Zero;
        }
    }
}
