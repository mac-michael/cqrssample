using System;
using System.Linq;
using System.Reflection;

namespace CQRS.Base.Infrastructure.Attributes
{
    public static class ReflectionExtensions
    {
        public static bool ShouldStartComponent(this Type type)
        {
            var a = type.GetCustomAttributes(typeof(ComponentAttribute), true)
                    .OfType<ComponentAttribute>()
                    .FirstOrDefault();

            if (a == null)
                return false;

            return a.Start;
        }

        public static bool IsComponentLifestyle(this Type type, ComponentLifestyle lifestyle)
        {
            var a = type.GetCustomAttributes(typeof (ComponentAttribute), true)
                .OfType<ComponentAttribute>()
                .FirstOrDefault();

            if (a == null)
                return false;

            return a.Lifestyle == lifestyle;
        }

        public static bool IsDefined<T>(this MemberInfo member, bool inherit = true) where T :Attribute
        {
            return member.IsDefined(typeof (T), inherit);
        }

    }
}