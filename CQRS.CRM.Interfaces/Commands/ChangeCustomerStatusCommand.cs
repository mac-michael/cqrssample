using CQRS.Base.CQRS.Commands.Attributes;
using CQRS.CRM.Interfaces.Domain;

namespace CQRS.CRM.Interfaces.Commands
{
    [Command]
    public class ChangeCustomerStatusCommand
    {
        public ChangeCustomerStatusCommand(int customerId, CustomerStatus status)
        {
            CustomerId = customerId;
            Status = status;
        }

        public int CustomerId { get; private set; }
        public CustomerStatus Status { get; private set; }
    }
}
