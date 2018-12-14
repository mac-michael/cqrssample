using System.Diagnostics;
using CQRS.Base.DDD.Infrastructure.Events;
using CQRS.Sales.Interfaces.Domain.Events;

namespace CQRS.Sales.Infrastructure.Events.Listeners.Domain
{
    [EventListeners]
    public class OrderSubmittedListener : IEventListener<OrderSubmittedEvent>
    {
        [EventListener(IsAsync= true)]
        public void Handle(OrderSubmittedEvent @event)
        {
            Debug.WriteLine("Sending mail aboud order: " + @event.OrderId);
        }
    }
}
