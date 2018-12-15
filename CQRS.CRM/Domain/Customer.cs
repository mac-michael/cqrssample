using System;
using System.Collections.Generic;
using System.Linq;
using CQRS.Base.DDD.Domain;
using CQRS.CRM.Interfaces.Domain;
using CQRS.CRM.Interfaces.Domain.Events;

namespace CQRS.CRM.Domain
{
    public class Customer : AggregateRoot
    {
        public string Name { get; private set; }
        public string CompanyName { get; private set; }

        // CRM
        public DateTime Birthday { get; private set; }

        private CustomerStatus _status;

        // CRM
        public void ChangeStatus(CustomerStatus status)
        {
            if (status == _status)
                return;

            _status = status;

            //Sample Case: give 10% rebate for all draft orders - communication via events with different Bounded Context to achieve decoupling
            EventPublisher.Publish(new CustomerStatusChangedEvent(Id, status));
        }
    }
}
