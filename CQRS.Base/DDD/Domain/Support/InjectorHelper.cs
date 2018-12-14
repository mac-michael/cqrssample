using CQRS.Base.Infrastructure.Attributes;

namespace CQRS.Base.DDD.Domain.Support
{
    [Component]
    public class InjectorHelper
    {
        public IDomainEventPublisher EventPublisher { get; set; }

        public void InjectDependencies(AggregateRoot aggregateRoot)
        {
            if (aggregateRoot != null)
            {
                aggregateRoot.EventPublisher = EventPublisher;
            }
        }
    }
}