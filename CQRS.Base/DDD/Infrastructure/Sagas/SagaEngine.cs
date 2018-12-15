using CQRS.Base.Infrastructure.Attributes;

namespace CQRS.Base.DDD.Infrastructure.Sagas
{
    [Component]
    public class SagaEngine : ISagaEngine
    {
        private readonly ISagaRegistry _sagaRegistry;

        public SagaEngine(ISagaRegistry sagaRegistry)
        {
            _sagaRegistry = sagaRegistry;
        }

        public void Handle(object eventData)
        {
            var managers = _sagaRegistry.GetSagaMangersForEvent(eventData.GetType());

            foreach (var manager in managers)
                manager(eventData);
        }
    }
}