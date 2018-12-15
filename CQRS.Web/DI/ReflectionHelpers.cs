using CQRS.Base.Infrastructure.Attributes;
using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;

namespace CQRS.Web
{
    public static class ReflectionHelpers
    {
        public static BasedOnDescriptor StartIfNecessary(this BasedOnDescriptor descriptor)
        {
            return descriptor.Configure(x =>
                                            {
                                                if (x.Implementation.ShouldStartComponent())
                                                    x.Start();
                                            });
        }
    }
}