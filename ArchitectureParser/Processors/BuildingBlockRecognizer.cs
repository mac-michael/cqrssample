using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CQRS.Base.CQRS.Commands.Attributes;
using CQRS.Base.CQRS.Commands.Handler;
using CQRS.Base.DDD.Domain;
using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.DDD.Infrastructure.Events;
using CQRS.Base.DDD.Infrastructure.Sagas;
using CQRS.Base.DDD.Sagas;

namespace ArchitectureParser
{
    public class BuildingBlockRecognizer
    {
        public bool IsEntity(Type type)
        {
            return type.IsEntity() && !type.IsAbstract;
        }

        public bool IsCommand(Type type)
        {
            return (type.IsAttributeDefined<CommandAttribute>() || type.Name.EndsWith("Command")) && !type.IsAbstract;
        }

        public bool IsCommandHandler(Type type, Type command)
        {
            if (type.IsAbstract) return false;

            var handler = typeof (ICommandHandler<>).MakeGenericType(command);
            return handler.IsAssignableFrom(type);
        }

        public bool IsAggregateRoot(Type type)
        {
            return type.IsAggregateRoot() && !type.IsAbstract;
        }

        public bool IsRepository(Type type)
        {
            return type.IsDomainRepository() && !type.IsAbstract;
        }

        public bool IsFactory(Type type)
        {
            return type.IsDomainFactory() && !type.IsAbstract;
        }

        public bool IsService(Type type)
        {
            return type.IsDomainService() && !type.IsAbstract;
        }

        public bool IsDomainEvent(Type type)
        {
            return typeof(IDomainEvent).IsAssignableFrom(type) && !type.IsAbstract;
        }

        public string GetBoundedContextName(Type type)
        {
            if (type.Namespace != null)
            {
                var @namespace = type.Namespace.Split('.');
                if (@namespace.Length > 1)
                    return @namespace[1];
            }

            return "";
        }

        public bool IsValueObject(Type type)
        {
            return type.IsValueObject() && !type.IsAbstract;
        }

        public bool IsEventListener(Type type, Type eventType)
        {
            if ( type.IsAbstract) return false;

            var handler = typeof(IEventListener<>).MakeGenericType(eventType);
            if (!handler.IsAssignableFrom(type)) return false;

            return type.IsAttributeDefined<EventListenersAttribute>();
        }

        public bool IsSagaHandlingEvent(Type type, Type eventType)
        {
            if ( !IsSaga(type)) return false;

            var handler = typeof(ISagaAction<>).MakeGenericType(eventType);
            return handler.IsAssignableFrom(type);
        }

        public IEnumerable<Type> GetEventsForSaga(Type sagaType)
        {
            var a = typeof (ISagaAction<>);

            return from i in sagaType.GetInterfaces()
                   where i.IsGenericType
                   where a.IsAssignableFrom(i.GetGenericTypeDefinition())
                   select i.GetGenericArguments()[0];
        }

        public bool IsSaga(Type type)
        {
            return typeof (Saga).IsAssignableFrom(type) && !type.IsAbstract;
        }
    }
}