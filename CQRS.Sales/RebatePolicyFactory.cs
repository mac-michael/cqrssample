using CQRS.DDD.Application;
using CQRS.DDD.Domain.Annotations;
using CQRS.Erp.Sales.Domain.Policies.Rabate;
using CQRS.Erp.Sales.Domain.Policies.Rabate.Decorators;
using CQRS.Infrastructure;

namespace CQRS.Erp.Sales.Domain
{
    [DomainFactory]
    public class RebatePolicyFactory
    {
        public SystemUser SystemUser { get; set; }

        public IRebatePolicy CreateRebatePolicy()
        {
            IRebatePolicy rebatePolicy = new StandardRebate(10, 1);

            if (IsVip(SystemUser))
                rebatePolicy = new VipRebate(rebatePolicy, new Money(1000.0), new Money(100));

            return rebatePolicy;
        }

        private bool IsVip(SystemUser systemUser)
        {
            return true;
        }
    }
}