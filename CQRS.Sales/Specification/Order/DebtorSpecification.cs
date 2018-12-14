using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRS.DDD.Domain.SharedKernel.Specification;
using CQRS.Infrastructure;

namespace CQRS.Erp.Sales.Domain.Specification
{
    public class DebtorSpecification : CompositeSpecification<Order>
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

        public override bool IsSatisfiedBy(Order order)
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
