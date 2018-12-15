using System;
using System.Collections.Generic;
using System.Diagnostics;
using CQRS.Base.DDD.Application;
using CQRS.Base.DDD.Domain;

namespace CQRS.Base.DDD.Infrastructure.Events.Implementation
{
    // Registration by hand, temp
    //[Component]
    public class SimpleEventPublisher : IEventSubscriber, IDomainEventPublisher, IApplicationEventPublisher
    {
        private HashSet<IEventListener> _eventHandlers = new HashSet<IEventListener>();
        private readonly object _sync = new object();

        public void Subscribe(IEventListener listener)
        {
            lock (_sync)
            {
                var h = new HashSet<IEventListener>(_eventHandlers) { listener };
                _eventHandlers = h;
            }
        }

        public void Unsubscribe(IEventListener listener)
        {
            lock (_sync)
            {
                var h = new HashSet<IEventListener>(_eventHandlers);
                h.Remove(listener);
                _eventHandlers = h;
            }
        }

        void IApplicationEventPublisher.Publish<T>(T applicationEvent)
        {
            PublishInternal(applicationEvent);
        }

        void IDomainEventPublisher.Publish<T>(T domainEvent)
        {
            PublishInternal(domainEvent);
        }

        protected void PublishInternal<T>(T eventObject)
        {
            foreach (var handler in _eventHandlers)
            {
                if (handler is IEventListener<T>)
                {
                    try
                    {
                        ((IEventListener<T>) handler).Handle(eventObject);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("event handling error: " + e.Message);
                        throw;
                    }
                }
            }
        }
    }
}
