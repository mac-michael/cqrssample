namespace CQRS.Base.DDD.Application
{
    public interface IApplicationEventPublisher
    {
        void Publish<T>(T eventData);
    }
}