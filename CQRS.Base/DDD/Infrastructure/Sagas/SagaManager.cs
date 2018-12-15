using System;
using CQRS.Base.DDD.Sagas;

namespace CQRS.Base.DDD.Infrastructure.Sagas
{
    public class SagaManager<TSaga, TSagaData> : ISagaManager
        where TSaga: Saga<TSagaData>, new()
        where TSagaData : class, new()
    {
        private readonly ISagaFinderFactory _factory;

        public SagaManager(ISagaFinderFactory factory)
        {
            _factory = factory;
        }

        public TSaga LoadSaga<TMessage>(TMessage eventData)
        {
            var data = LoadSagaData(eventData);

            var saga = _factory.CreateSaga<TSaga>();
            saga.Data = data;

            return saga;
        }

        private TSagaData LoadSagaData<TMessage>(TMessage eventData)
        {
            var finder = _factory.Create<TSagaData, TMessage>();

            var data = finder.FindBy(eventData);

            if (data == null)
                data = ((ISagaDataFinderBase<TSagaData>)finder).CreateNewSagaData();

            return data;
        }

        public void RemoveSaga<TMessage>(Saga<TSagaData> saga)
        {
            var finder = _factory.Create<TSagaData, TMessage>();

            // Refactor
            ((ISagaDataFinderBase<TSagaData>)finder).RemoveSagaData(saga.Data);
        }

        public void ProcessMessage<TMessage>(TMessage message)
        {
            TSaga sagaInstance = LoadSaga(message);

            if (sagaInstance is ISagaAction<TMessage>)
                ((ISagaAction<TMessage>)sagaInstance).Handle(message);
            else
                throw new InvalidOperationException("Missing saga action for message " + message.GetType()); 

            if (sagaInstance.IsCompleted)
                RemoveSaga<TMessage>(sagaInstance);
        }
    }
}