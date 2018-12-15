namespace CQRS.Base.CQRS.Commands.Handler
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> Create<T>();
        ICommandHandler CreateByName(string name);

        void Release(ICommandHandler handler);
    }
}