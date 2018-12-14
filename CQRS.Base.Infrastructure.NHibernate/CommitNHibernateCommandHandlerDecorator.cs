using CQRS.Base.CQRS.Commands.Handler;

namespace CQRS.Base.Infrastructure.NHibernate
{
    public class CommitNHibernateCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _inner;
        public IEntityManager Manager { get; set; }

        public CommitNHibernateCommandHandlerDecorator(ICommandHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public void Handle(TCommand command)
        {
            _inner.Handle(command);
            Manager.CurrentSession.Flush();
        }
    }
}