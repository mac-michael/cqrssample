using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace CQRS.Base.Infrastructure.NHibernate.Conventions
{
    public class SetTableNameConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            // TODO: add y and s
            instance.Table(instance.EntityType.Name  + "s");
        }
    }
}