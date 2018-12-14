using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace CQRS.Base.Infrastructure.NHibernate.Conventions
{
    public class UseNewSqlDateTime2TypeConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            if (instance.Type == typeof(DateTime))
                instance.CustomSqlType("datetime2");
        }
    }
}