using System;

namespace CQRS.Base.DDD.Infrastructure.Events
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventListenersAttribute : Attribute
    {

    }
}