using CQRS.Base.CQRS.Commands.Handler;

namespace CQRS.Base.CQRS.Commands.Decorator
{
    public class ConatinerCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandlerFactory _factory;
        public ConatinerCommandHandlerDecorator(ICommandHandlerFactory factory)
        {
            _factory = factory;
        }

        public void Handle(TCommand command)
        {
            ICommandHandler<TCommand> handler = null;
            try
            {
                ICommandHandler commandHandler = _factory.CreateByName(command.GetType().FullName);

                handler = (ICommandHandler<TCommand>)commandHandler;
                handler.Handle(command);
            }
            finally
            {
                if (handler != null) 
                    _factory.Release(handler);
            }
        }
    }
}