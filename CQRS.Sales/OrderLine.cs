using CQRS.Infrastructure;

namespace CQRS.Erp.Sales.Domain
{
    public class OrderLine : Entity
    {
        protected OrderLine()
        {
            
        }

        public OrderLine(Product product, int quantity, IRebatePolicy rebatePolicy, Order order)
        {
            Product = product;
            Quantity = quantity;
            Order = order;
            Recalculate(rebatePolicy);
        }

        public Order Order { get; private set; }
        public Product Product { get; private set; }

        public int Quantity { get; private set; }
        public Money EffectiveCost { get; private set; }
        public Money RegularCost { get; private set; }

        public void IncreaseQuantity(int quantity, IRebatePolicy rebatePolicy)
        {
            Quantity += quantity;
            Recalculate(rebatePolicy);
        }

        public void Recalculate(IRebatePolicy rebatePolicy)
        {
            RegularCost = Product.Price.MultiplyBy(Quantity);
            Money rebate = rebatePolicy.CalculateRebate(Product, Quantity,
                                                        RegularCost);
            EffectiveCost = RegularCost - rebate;
        }
    }
}