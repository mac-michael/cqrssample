using System;

namespace CQRS.Base.DDD.Sagas
{
    public static class ReflectionHelper
    {
        public static Type FindBaseType(this Type type, Func<Type,bool> predicate )
        {
            if ( type == null ) return null;

            if (predicate(type))
                return type;

            return type.BaseType.FindBaseType(predicate);
        }

        public static bool IsSaga(this Type type)
        {
            return !type.IsAbstract && type.FindBaseType(b => b.IsGenericType &&
                                          b.GetGenericTypeDefinition() == typeof (Saga<>)) != null;
        }
    }
}