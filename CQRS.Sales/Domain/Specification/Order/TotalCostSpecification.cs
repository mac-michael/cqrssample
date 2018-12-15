using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Base.DDD.Domain.SharedKernel.Specification;

namespace CQRS.Sales.Domain.Specification.Order
{
    public class TotalCostSpecification : CompositeSpecification<Domain.Order>
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

        public override bool IsSatisfiedBy(Domain.Order order)
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