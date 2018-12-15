using CQRS.Base.CQRS.Commands.Attributes;
using CQRS.Base.CQRS.Commands.Handler;
using CQRS.CRM.Domain;
using CQRS.CRM.Interfaces.Commands;

namespace CQRS.CRM.Commands.Handlers
{
    [CommandHandler]
    public class ChangeCustomerStatusCommandHandler : ICommandHandler<ChangeCustomerStatusCommand>
    {
        public ICustomerRepository CustomerRepository { get; set; }

        public void Handle(ChangeCustomerStatusCommand command)
        {
            Customer customer = CustomerRepository.Load(command.CustomerId);
            customer.ChangeStatus(command.Status);
            CustomerRepository.Save(customer);
        }
    }
}