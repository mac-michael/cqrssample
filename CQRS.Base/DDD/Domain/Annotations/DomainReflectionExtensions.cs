using System;

namespace CQRS.Base.DDD.Domain.Annotations
{
    public static class DomainReflectionExtensions
    {
        public static bool IsDomainService(this Type type)
        {
            return type.IsAttributeDefined<DomainServiceAttribute>();
        }

        public static bool IsAggregateRoot(this Type type)
        {
            return type.IsAttributeDefined<DomainAggregateRootAttribute>();
        }

        public static bool IsEntity(this Type type)
        {
            return type.IsAttributeDefined<DomainEntityAttribute>();
        }
        
        public static bool IsValueObject(this Type type)
        {
            return type.IsAttributeDefined<DomainValueObjectAttribute>();
        }

        public static bool IsDomainRepository(this Type type)
        {
            return type.IsAttributeDefined<DomainRepositoryAttribute>();
        }

        public static bool IsDomainRepositoryImplementation(this Type type)
        {
            return type.IsAttributeDefined<DomainRepositoryImplementationAttribute>();
        }

        public static bool IsDomainFactory(this Type type)
        {
            return type.IsAttributeDefined<DomainFactoryAttribute>();
        }

        public static bool IsAttributeDefined<T>(this Type type)
        {
            return type.IsDefined(typeof (T), true);
        }
    }
}