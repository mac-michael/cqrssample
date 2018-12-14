using CQRS.Base.DDD.Infrastructure.Events;

namespace CQRS.Base.DDD.Infrastructure.Sagas
{
    public interface ISagaEngine : IEventListener<object>
    {
        
    }
}
