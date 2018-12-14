using System.Diagnostics;
using CQRS.Base.DDD.Infrastructure.Events;
using CQRS.Sales.Interfaces.Application.Events;

namespace CQRS.Sales.Infrastructure.Events.Listeners.Application
{
    [EventListeners]
    public class ProductAddedToOrderListener : IEventListener<ProductAddedToOrderEvent>
    {
        [EventListener(IsAsync= true)]
        public void Handle(ProductAddedToOrderEvent @event)
        {
            Debug.WriteLine("Spy report:: client: " + @event.ClientId + " have added product: " + @event.Productid);
        }
    }
}
