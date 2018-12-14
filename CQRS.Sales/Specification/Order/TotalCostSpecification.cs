using CQRS.DDD.Domain.SharedKernel.Specification;
using CQRS.Infrastructure;

namespace CQRS.Erp.Sales.Domain.Specification
{
    public class TotalCostSpecification : CompositeSpecification<Order>
    {
        private readonly Money? _min;
        private readonly Money? _max;

        public TotalCostSpecification(Money? min, Money? max)
        {
            _min = min;
            _max = max;
        }

        public TotalCostSpecification(Money max)
            : this(null, max)
        {
        }

        public override bool IsSatisfiedBy(Order order)
        {
            if (_min != null)
                if (order.TotalCost < _min.Value)
                    return false;

            if (_max != null)
                if (order.TotalCost > _max)
                    return false;

            return true;
        }
    }
}