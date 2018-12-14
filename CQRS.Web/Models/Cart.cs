using System.Collections.Generic;
using CQRS.Base.DDD.Infrastructure.Events;
using CQRS.Base.Infrastructure.Attributes;
using CQRS.Sales.Interfaces.Domain.Events;

namespace CQRS.Web.Controllers
{
    [Component(ComponentLifestyle.PerSession)]
    public class Cart : IEventListener<OrderCreatedEvent>
    {
        private readonly Dictionary<int,int> _productsIdsWithCount = new Dictionary<int, int>();
        public Dictionary<int, int> ProductsIdsWithCount
        {
            get { return _productsIdsWithCount; }
        }

        public bool HasItems { get { return _productsIdsWithCount.Count > 0; } }

        // ClearBasketOnSuccessfulOrderCreation
        public void Handle(OrderCreatedEvent orderCreatedEvent)
        {
            Clear();
        }

        public void Clear()
        {
            ProductsIdsWithCount.Clear();
        }

        public void Add(int productId)
        {
            int value;
            if (!ProductsIdsWithCount.TryGetValue(productId, out value))
                value = 0;

            value++;

            ProductsIdsWithCount[productId] = value;
        }
    }
}