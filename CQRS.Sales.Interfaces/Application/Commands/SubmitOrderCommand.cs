using System;

namespace CQRS.Sales.Interfaces.Application.Commands
{
    public class SubmitOrderCommand
    {
        public int OrderId { get; private set; }

        public SubmitOrderCommand(int orderId)
        {
            OrderId = orderId;
        }

        public override bool Equals(Object obj)
        {
            if (obj is SubmitOrderCommand)
            {
                SubmitOrderCommand command = (SubmitOrderCommand) obj;
                return OrderId == command.OrderId;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return OrderId.GetHashCode();
        }
    }
}