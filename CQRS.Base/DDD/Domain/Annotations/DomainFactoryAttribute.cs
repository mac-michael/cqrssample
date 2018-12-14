using System;

namespace CQRS.Base.DDD.Domain.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class DomainFactoryAttribute : Attribute
    {
    }
}
