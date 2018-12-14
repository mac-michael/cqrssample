using System.Linq.Expressions;
using CQRS.Base.CQRS.Commands.Handler;

namespace CQRS.Base.CQRS.Commands
{
    internal class GateHistoryDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _inner;

        public GateHistoryDecorator(ICommandHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public void Handle(TCommand command)
        {
            _inner.Handle(command);
        }
    }
}
