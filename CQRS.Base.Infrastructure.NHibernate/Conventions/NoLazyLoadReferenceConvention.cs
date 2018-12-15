using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;

namespace CQRS.Base.Infrastructure.NHibernate.Conventions
{
    public class NoLazyLoadReferenceConvention : IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.LazyLoad(Laziness.NoProxy);
        }
    }
}