using System;
using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.DDD.Domain.SharedKernel;

namespace CQRS.Sales.Domain.Policies.Tax
{
    [DomainPolicyImplementation]
    public class CrysisTaxPolicy : ITaxPolicy
    {
        public Domain.Tax CalculateTax(ProductType productType, Money net)
        {
            decimal ratio = 0.4m;
            String desc = "sorry";

            Money tax = net.MultiplyBy(ratio);

            return new Domain.Tax(tax, desc);
        }
    }
}
