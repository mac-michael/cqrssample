using CQRS.Base.DDD.Application;
using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Sales.Domain.Policies.Rabate;
using CQRS.Sales.Domain.Policies.Rabate.Decorators;

namespace CQRS.Sales.Domain
{
    [DomainFactory]
    public class RebatePolicyFactory
    {
        public ISystemUser SystemUser { get; set; }

        public IRebatePolicy CreateRebatePolicy()
        {
            IRebatePolicy rebatePolicy = new StandardRebate(10, 1);

            if (IsVip(SystemUser))
                rebatePolicy = new VipRebate(rebatePolicy, new Money(1000.0), new Money(100));

            return rebatePolicy;
        }

        private bool IsVip(ISystemUser systemUser)
        {
            return true;
        }
    }
}