using CQRS.Infrastructure;

namespace CQRS.Erp.Sales
{
    public class Product : AggregateRoot
    {
        public ProductType Type { get; set; }
        public Money Price { get; set; }
        public string Name { get; set; }

        public Product()
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