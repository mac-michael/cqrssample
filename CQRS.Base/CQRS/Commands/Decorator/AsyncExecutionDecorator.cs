using System.Threading.Tasks;
using CQRS.Base.CQRS.Commands.Handler;

namespace CQRS.Base.CQRS.Commands
{
    internal class AsyncExecutionDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _inner;

        public AsyncExecutionDecorator(ICommandHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public void Handle(TCommand command)
        {
            new Task(() => _inner.Handle(command)).Start();
        }
    }
}