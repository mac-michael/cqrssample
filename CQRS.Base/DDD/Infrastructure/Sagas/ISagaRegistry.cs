using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CQRS.Base.DDD.Sagas;
using CQRS.Base.Infrastructure.Attributes;

namespace CQRS.Base.DDD.Infrastructure.Sagas
{
    public interface ISagaRegistry
    {
        IEnumerable<Action<object>> GetSagaMangersForEvent(Type eventType);
    }

    [Component]
    public class SagaRegistry : ISagaRegistry
    {
        private readonly ISagaFinderFactory _finder;
        readonly Dictionary<Type, List<Action<object>>> _interestedManagers = new Dictionary<Type, List<Action<object>>>();
        
        public SagaRegistry(Saga[] sagas, ISagaFinderFactory finder)
        {
            _finder = finder;
            var m = from s in sagas
                    from i in s.GetType().GetInterfaces()
                    where i.IsGenericType
                    let generic = i.GetGenericTypeDefinition()
                    where generic == typeof (ISagaAction<>)
                    let a = new {MessageType = i.GetGenericArguments()[0], Manager = CreateManagerForSaga(s)}
                    let param = Expression.Parameter(typeof (object), "event")
                    let func = Expression.Lambda<Action<object>>(
                        Expression.Call(
                            Expression.Constant(a.Manager), "ProcessMessage", new[] {a.MessageType},
                            Expression.Convert(param, a.MessageType)),param)
                    group func.Compile() by a.MessageType
                    into g
                    select g;

            _interestedManagers = m.ToDictionary(k => k.Key, v => v.ToList());
        }

        private ISagaManager CreateManagerForSaga(Saga saga)
        {
            var sagaType = saga.GetType();
            var sagaDataType = saga.GetType().FindBaseType(
                p => p.IsGenericType && p.GetGenericTypeDefinition() == typeof (Saga<>)).GetGenericArguments()[0];

            if (sagaDataType == null)
                throw new InvalidOperationException("Saga not derived from Saga<T>");

            var manager = typeof (SagaManager<,>).MakeGenericType(sagaType, sagaDataType);
            return (ISagaManager) Activator.CreateInstance(manager, _finder);
        }

        public IEnumerable<Action<object>> GetSagaMangersForEvent(Type eventType)
        {
            List<Action<object>> managers;
            if ( _interestedManagers.TryGetValue(eventType, out managers))
                return managers;

            return Enumerable.Empty<Action<object>>();
        }
    }
}