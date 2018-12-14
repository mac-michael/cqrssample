using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace CQRS.Web
{
    public static class ComponentRegistrationExtensions
    {
        public static BasedOnDescriptor LifestylePerSession(this BasedOnDescriptor reg)
        {
            return reg.LifestyleScoped<WebSessionScopeAccessor>();
        }
    }
}