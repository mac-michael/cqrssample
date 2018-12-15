using System;
using System.Collections.Generic;
using System.Linq;
using CQRS.Base.DDD.Domain;
using CQRS.Base.DDD.Domain.Exceptions;
using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Base.Infrastructure.NHibernate;
using CQRS.Sales.Interfaces.Domain;
using CQRS.Sales.Interfaces.Domain.Events;
using CQRS.Sales.Interfaces.Domain.Exceptions;

namespace CQRS.Sales.Domain
{
    public class Order : AggregateRoot
    {
        // Sample access to the internal state - remember to allow such access
        // only if makes sense, don't do that by default!
        // 
        // Notice that there is no setter!
    
        // TODO: add Customer snapshot
        public int ClientId { get; private set; }

        public Money TotalCost { get; private set; }

        // Encapsulation
        private IList<OrderLine> _items;

        public DateTime SubmitDate { get; private set; }

        public OrderStatus OrderStatus { get; private set; }

        // Sample of Policy usage (Strategy Design Pattern)
        private IRebatePolicy _rebatePolicy;

        protected Order() { }

        // Meant to be used by factory
        // Notice that Policy is set via setter because Policy need by initialised also in Repository
        internal Order(int clientId, Money initialCost, OrderStatus initialStatus)
        {
            ClientId = clientId;
            TotalCost = initialCost;
            OrderStatus = initialStatus;
            
            // Temp
            SubmitDate = DateTime.Now.AddYears(-50);

            _items = new List<OrderLine>();
        }

       
        /// <summary>
        /// Sample business method that:
        /// <ul>
        /// <li>hides internal state - OrderLine</li>
        /// <li>can veto - if order is not in DRAFT status</li>
        /// <li>not only modifies structure (list) but also performs logic - calculates total cost</li>
        /// </ul>
        /// </summary>
        public void AddProduct(Product product, int quantity)
        {
            CheckIfDraft();

            OrderLine line = Find(product);

            if (line == null)
                _items.Add(new OrderLine(product, quantity, _rebatePolicy, this));
            else
                line.IncreaseQuantity(quantity, _rebatePolicy);

            Recalculate();
        }

        // Sample business method that uses Policy
        public void ApplyRebate(IRebatePolicy rebatePolicy)
        {
            CheckIfDraft();

            _rebatePolicy = rebatePolicy;
            Recalculate();
        }

        public void Submit()
        {
            CheckIfDraft();

            OrderStatus = OrderStatus.Submitted;
            SubmitDate = DateTime.Now;

            EventPublisher.Publish(new OrderSubmittedEvent(Id));
        }

        private void CheckIfDraft()
        {
            if (OrderStatus != OrderStatus.Draft)
                throw new OrderOperationException("Operation allowed only in DRAFT status", ClientId, Id);
        }

        // Sample technique of injecting Policy into the Aggregate.
        // Can be called only once by Factory/Repository.
        internal void SetRebatePolicy(IRebatePolicy rebatePolicy)
        {
            if (_rebatePolicy != null)
                throw new IllegalStateException("Policy is already set! Probably You have logical error in code");

            _rebatePolicy = rebatePolicy;
        }

        private void Recalculate()
        {
            TotalCost = Money.Zero;

            foreach (OrderLine line in _items)
            {
                line.Recalculate(_rebatePolicy);
                TotalCost = TotalCost + line.EffectiveCost;
            }
        }

        private OrderLine Find(Product product)
        {
            return _items.FirstOrDefault(line => product.Equals(line.Product));
        }

        
        // Sample encapsulation of unstable internal implementation - assumption:
        // this impl may vary in time. So we use projection of the internal state of
        // this Aggregate<br>
        // 
        // Projection hides internal structure using Value Objects. Projection is
        // also unmodifiable.
        public IEnumerable<OrderedProduct> OrderedProducts
        {
            get
            {
                {
                    return _items.Select(line => new OrderedProduct(line.Product.Id, line.Product.Name,
                                                                    line.Quantity,
                                                                    line.EffectiveCost, line.RegularCost));
                }
            }
        }

        public int LinesNumber
        {
            get { return _items.Count(); }
        }

        public int ItemsNumber
        {
            get { return _items.Sum(line => line.Quantity); }
        }

        public bool Contains(Product product)
        {
            return _items.Any(line => line.Product.Equals(product));
        }

        public void Archive()
        {
            OrderStatus = OrderStatus.Archived;
        }
    }
}
