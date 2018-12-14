using System;
using CQRS.DDD.Domain.Annotations;
using CQRS.Infrastructure;

namespace CQRS.Erp.Sales.Domain
{
    // Encapsulation sample
    [DomainValueObject]
    public class OrderedProduct
    {
        public int ProductId { get; set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public Money EffectiveCost { get; private set; }
        public Money RegularCost { get; private set; }

        public OrderedProduct(int productId, String name, int quantity, Money effectiveCost, Money regularCost)
        {
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            EffectiveCost = effectiveCost;
            RegularCost = regularCost;
        }
    }
}