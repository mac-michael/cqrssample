namespace CQRS.Base.DDD.Infrastructure.Events
{
    public interface IEventListener
    {
        
    }

    public interface IEventListener<in TEvent> : IEventListener
    {
        void Handle(TEvent eventData);
    }
}