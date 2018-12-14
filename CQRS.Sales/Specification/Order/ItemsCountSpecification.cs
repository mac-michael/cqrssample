using CQRS.DDD.Domain.SharedKernel.Specification;

namespace CQRS.Erp.Sales.Domain.Specification
{
    public class ItemsCountSpecification : CompositeSpecification<Order>
    {
        private readonly int? _min;
        private readonly int? _max;

        public ItemsCountSpecification(int? min, int? max)
        {
            _min = min;
            _max = max;
        }

        public ItemsCountSpecification(int? max)
            : this(null, max)
        {
        }

        public override bool IsSatisfiedBy(Order order)
        {
            if (_min != null)
                if (order.ItemsNumber < _min)
                    return false;

            if (_max != null)
                if (order.ItemsNumber > _max)
                    return false;

            return true;
        }
    }
}