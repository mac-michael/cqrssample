using CQRS.Base.DDD.Domain;
using CQRS.Base.DDD.Domain.SharedKernel;

namespace CQRS.Sales.Domain
{
    public class Product : AggregateRoot
    {
        public ProductType Type { get; private set; }
        public Money Price { get; private set; }
        public string Name { get; private set; }

        protected Product()
        {
            
        }

        public Product(ProductType type, Money price, string name)
        {
            Type = type;
            Price = price;
            Name = name;
        }
    }
}