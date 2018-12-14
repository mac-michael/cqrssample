using System;

namespace CQRS.Base.DDD.Infrastructure.Events
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventListenerAttribute : Attribute
    {
        public bool IsAsync { get; set; }

        public EventListenerAttribute()
        {
            
        }

        public EventListenerAttribute(bool isAsync)
        {
            IsAsync = isAsync;
        }
    }
}
