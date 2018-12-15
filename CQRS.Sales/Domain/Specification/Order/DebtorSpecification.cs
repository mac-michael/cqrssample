using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Base.DDD.Domain.SharedKernel.Specification;

namespace CQRS.Sales.Domain.Specification.Order
{
    public class DebtorSpecification : CompositeSpecification<Domain.Order>
    {
        private readonly Money _maxDebt;

        public DebtorSpecification(Money maxDebt)
        {
            _maxDebt = maxDebt;
        }

        // No debt is allowed
        public DebtorSpecification()
            : this(Money.Zero)
        {
        }

        public override bool IsSatisfiedBy(Domain.Order order)
        {
            Money debt = LoadDebt(order.ClientId);

            return debt <= _maxDebt;
        }

        private Money LoadDebt(int clientId)
        {
            // TODO load debt using injected Repo/Service to this Spec
            return Money.Zero;
        }
    }
}
