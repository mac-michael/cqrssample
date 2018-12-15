using CQRS.Base.DDD.Domain;
using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.DDD.Domain.Support;
using CQRS.Base.Infrastructure.NHibernate.Repositories;
using CQRS.Sales.Domain;
using CQRS.Sales.Interfaces.Domain.Events;

namespace CQRS.Sales.Infrastructure.Repositories
{
    [DomainRepositoryImplementation]
    public class OrderRepository : GenericRepositoryForBaseEntity<Order>, IOrderRepository
    {
        public RebatePolicyFactory RebatePolicyFactory { get; set; }
        public InjectorHelper Injector { get; set; }
        public IDomainEventPublisher EventPublisher { get; set; }

        public override void Save(Order order)
        {
            var newEntity = order.Id == 0;

            base.Save(order);

            if (newEntity)
                EventPublisher.Publish(new OrderCreatedEvent(order.Id));
        }

        public override Order Load(int orderId)
        {
            Order order = base.Load(orderId);
            order.SetRebatePolicy(RebatePolicyFactory.CreateRebatePolicy());
            return order;
        }
    }
}