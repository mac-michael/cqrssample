using System.Linq;
using CQRS.Base.DDD.Domain.SharedKernel.Specification;
using CQRS.Sales.Interfaces.Domain;

namespace CQRS.Sales.Domain.Specification.Order
{
    public class RestrictedProductsSpecification : CompositeSpecification<Domain.Order>
    {
        private Product[] _restrictedProducts;

        public RestrictedProductsSpecification(params Product[] restrictedProducts)
        {
            _restrictedProducts = restrictedProducts;
        }

        public override bool IsSatisfiedBy(Domain.Order order)
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
