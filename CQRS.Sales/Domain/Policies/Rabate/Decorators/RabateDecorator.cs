using CQRS.Base.DDD.Domain.SharedKernel;

namespace CQRS.Sales.Domain.Policies.Rabate.Decorators
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
