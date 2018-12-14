using System.Collections.Generic;
using CQRS.Base.CQRS.Commands.Attributes;

namespace CQRS.Sales.Interfaces.Application.Commands
{
    public class CreateOrderCommand
    {
        public Dictionary<int, int> ProductIdsWithCounts { get; private set; }

        // This is a compromise, we cannot execute this command async (using messages), it must be executed sync,
        // because client is waiting for results. So if you decided that you must have some result back
        // there's no way back, because you couple client with your decision.
        // However, in specific cases you can use guid as an entity id. It can be generated on the client side,
        // so there is no need to return it from command and it can be truly async, see Lead in CRM BC
        [OutputCommandParameter]
        public int OrderId { get; set; }

        public CreateOrderCommand(Dictionary<int, int> productIdsWithCounts)
        {
            ProductIdsWithCounts = productIdsWithCounts;
        }
    }
}