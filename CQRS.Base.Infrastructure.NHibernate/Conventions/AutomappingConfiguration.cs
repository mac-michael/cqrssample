using System;
using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate;
using FluentNHibernate.Automapping;

namespace CQRS.Base.Infrastructure.NHibernate.Conventions
{
    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public static Func<Type, bool> IsEntityPredicate = t => false;
        public static Func<Type, bool> IsComponentPredicate = t => false;

        public override bool ShouldMap(Type type)
        {
            return IsEntityPredicate(type);
        }

        public override bool IsVersion(Member member)
        {
            return member.IsProperty && member.Name == "Version" &&
                   (member.PropertyType == typeof (DateTime) || member.PropertyType == typeof (byte[]));
        }

        public override bool IsComponent(Type type)
        {
            return IsComponentPredicate(type);
        }

        public override bool ShouldMap(Member member)
        {
            if (IsCollection(member))
                return true;

            if (!member.IsProperty || !member.IsPublic)
                return false;

            if (member.CanWrite)
                return true;

            if (member.IsAutoProperty)
                return true;

            if (((PropertyInfo)member.MemberInfo).GetSetMethod(true) != null)
                return true;

            // Convention based ignore
            var field = member.DeclaringType.GetField("_" + char.ToLower(member.Name[0]) +
                                           member.Name.Substring(1), BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null && field.FieldType == member.PropertyType)
                return true;

            return false;

            //if (member.MemberInfo.IsDefined(typeof(IgnoreAttribute),true))
              //  return false;

            bool shouldMap = base.ShouldMap(member);

            //if (member.IsProperty && !member.IsAutoProperty && ((PropertyInfo)member.MemberInfo).GetSetMethod(true) == null)
              //  return false;

            return (shouldMap || IsCollection(member)) ;
        }

        private static bool IsCollection(Member member)
        {
            return member.IsField && member.PropertyType.IsGenericType && (member.PropertyType.GetGenericTypeDefinition() == typeof(IList<>) || member.PropertyType.GetGenericTypeDefinition() == typeof(List<>));
        }
    }
}