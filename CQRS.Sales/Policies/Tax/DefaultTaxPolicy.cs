using System;
using CQRS.DDD.Domain.Annotations;
using CQRS.Infrastructure;

namespace CQRS.Erp.Sales.Domain.Policies.Rabate
{
    [DomainPolicyImplementation]
    public class DefaultTaxPolicy : ITaxPolicy
    {
        public Tax CalculateTax(ProductType productType, Money net)
        {
            decimal? ratio;
            string desc;

            switch (productType)
            {
                case ProductType.Drug:
                    ratio = 0.05m;
                    desc = "5% (D)";
                    break;
                case ProductType.Food:
                    ratio = 0.07m;
                    desc = "7% (F)";
                    break;
                case ProductType.Standard:
                    ratio = 0.23m;
                    desc = "23%";
                    break;

                default:
                    throw new ArgumentException(productType + " not handled");
            }

            Money tax = net.MultiplyBy(ratio.Value);

            return new Tax(tax, desc);
        }
    }
}