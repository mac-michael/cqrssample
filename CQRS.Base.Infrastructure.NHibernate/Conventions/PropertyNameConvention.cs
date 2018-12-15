using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace CQRS.Base.Infrastructure.NHibernate.Conventions
{
    public class PropertyNameConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            instance.Column("_" + instance.Name);
        }
    }
}