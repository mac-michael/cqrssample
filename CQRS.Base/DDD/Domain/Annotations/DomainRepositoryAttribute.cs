using System;

namespace CQRS.Base.DDD.Domain.Annotations
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class DomainRepositoryAttribute : Attribute
    {
    }
}