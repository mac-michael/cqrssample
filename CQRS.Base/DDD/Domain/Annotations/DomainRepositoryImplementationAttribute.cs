using System;

namespace CQRS.Base.DDD.Domain.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DomainRepositoryImplementationAttribute : Attribute
    {
    }
}