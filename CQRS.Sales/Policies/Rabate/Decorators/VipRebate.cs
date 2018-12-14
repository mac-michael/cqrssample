using System;
using CQRS.Infrastructure;

namespace CQRS.Erp.Sales.Domain.Policies.Rabate.Decorators
{
    // Sample usage of the Decorator Design Pattern - allows to assemble additional logic <b>in the runtime<b><br>
    // This example subtracts given value from total product cost if cost > threshold
    public class VipRebate : RebateDecorator
    {
        private readonly Money _minimalThreshold;

        private readonly Money _rebateValue;

        public VipRebate(Money minimalThreshold, Money rebateValue)
            : this(null, minimalThreshold, rebateValue)
        {
        }

        public VipRebate(IRebatePolicy decorated, Money minimalThreshold, Money rebateValue)
            : base(decorated)
        {
            if (rebateValue > minimalThreshold)
                throw new ArgumentException("Rabate can't be graterThan minimal threshold");

            _minimalThreshold = minimalThreshold;
            _rebateValue = rebateValue;
        }

        public override Money CalculateRebate(Product product, int quantity, Money regularCost)
        {
            Money baseValue = (Decorated == null)
                                  ? regularCost
                                  : Decorated.CalculateRebate(product, quantity, regularCost);

            if (baseValue > _minimalThreshold)
                return baseValue - _rebateValue;
            return baseValue;
        }
    }
}
