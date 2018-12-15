using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRS.DDD.Domain.SharedKernel.Specification;

namespace CQRS.Erp.Sales.Domain.Specification
{
    public class RestrictedProductsSpecification : CompositeSpecification<Order>
    {
        private Product[] _restrictedProducts;

        public RestrictedProductsSpecification(params Product[] restrictedProducts)
        {
            _restrictedProducts = restrictedProducts;
        }

        public override bool IsSatisfiedBy(Order order)
        {
            return order.OrderedProducts.All(product => !IsWeaponOfMassDestruction(product));
        }

        private bool IsWeaponOfMassDestruction(OrderedProduct product)
        {
            // TODO implement

            return false;
        }
    }
}
