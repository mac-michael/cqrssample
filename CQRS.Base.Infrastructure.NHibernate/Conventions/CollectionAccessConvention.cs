using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace CQRS.Base.Infrastructure.NHibernate.Conventions
{
    public class CollectionAccessConvention : ICollectionConvention
    {
        public void Apply(ICollectionInstance instance)
        {
            instance.Fetch.Select();
            instance.Cascade.All();
            instance.AsBag();
        }
    }
}