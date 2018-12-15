using CQRS.Base.DDD.Infrastructure.Events;
using CQRS.CRM.Interfaces.Domain;
using CQRS.CRM.Interfaces.Domain.Events;

namespace CQRS.Sales.Application.Listeners
{
    [EventListeners]
    public class CustomerStatusChangedListener : IEventListener<CustomerStatusChangedEvent>
    {
        public void Handle(CustomerStatusChangedEvent eventData)
        {
            if (eventData.Status == CustomerStatus.Vip)
                CalculateReabteForAllDraftOrders(eventData.CustomerId, 10);
        }

        private void CalculateReabteForAllDraftOrders(int customerId, int discount)
        {
        }
    }
}
