using CQRS.Base.CQRS.Commands.Attributes;
using CQRS.Base.CQRS.Commands.Handler;
using CQRS.CRM.Domain;
using CQRS.CRM.Interfaces.Commands;

namespace CQRS.CRM.Commands.Handlers
{
    [CommandHandler]
    public class CreateLeadCommandHandler : ICommandHandler<CreateLeadCommand>
    {
        public ILeadRepository LeadRepository { get; set; }

        public void Handle(CreateLeadCommand command)
        {
            // This is sample command which can be executed totally async.
            // The consumer is responsible for creating entity id not the server
            // It can be used later to correlate other commands or redirect user to appropriate page.

            var lead = new Lead
                           {
                               Id = command.Id,
                               Name = command.Name
                           };

            LeadRepository.Save(lead);
        }
    }
}