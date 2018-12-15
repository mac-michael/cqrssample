using System;
using CQRS.Base.CQRS.Commands.Attributes;

namespace CQRS.CRM.Interfaces.Commands
{
    [Command]
    public class CreateLeadCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}