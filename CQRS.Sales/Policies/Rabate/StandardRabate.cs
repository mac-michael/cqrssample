using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRS.DDD.Domain.Annotations;
using CQRS.Infrastructure;

namespace CQRS.Erp.Sales.Domain.Policies.Rabate
{
    [DomainPolicyImplementation]
    public class StandardRebate : IRebatePolicy
    {
        private readonly decimal _rebateRatio;
        private readonly int _mininalQuantity;

        /// <param name="rebate">value of the rebate in %, TODO: impement Percent VO</param>
        /// <param name="mininalQuantity">minimal quantity of the purchase that allows rebate</param>
        public StandardRebate(double rebate, int mininalQuantity)
        {
            _rebateRatio = new decimal(rebate/100);
            _mininalQuantity = mininalQuantity;
        }

        public Money CalculateRebate(Product product, int quantity, Money regularCost)
        {
            if (quantity >= _mininalQuantity)
                return regularCost.MultiplyBy(_rebateRatio);
            return Money.Zero;
        }
    }
}
