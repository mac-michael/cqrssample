using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRS.DDD.Domain.Annotations;
using CQRS.Infrastructure;

namespace CQRS.Erp.Sales.Domain.Policies.Rabate
{
    [DomainPolicyImplementation]
    public class CrysisTaxPolicy : ITaxPolicy
    {
        public Tax CalculateTax(ProductType productType, Money net)
        {
            decimal ratio = 0.4m;
            String desc = "sorry";

            Money tax = net.MultiplyBy(ratio);

            return new Tax(tax, desc);
        }
    }
}
