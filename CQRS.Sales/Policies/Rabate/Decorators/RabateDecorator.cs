using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRS.Infrastructure;

namespace CQRS.Erp.Sales.Domain.Policies.Rabate.Decorators
{
    public abstract class RebateDecorator : IRebatePolicy
    {
        public IRebatePolicy Decorated { get; set; }

        protected RebateDecorator(IRebatePolicy decorated)
        {
	    	Decorated = decorated;
	    }

        public abstract Money CalculateRebate(Product product, int quantity, Money regularCost);
    }
}
