using System.Reflection;
using Castle.Facilities.TypedFactory;

namespace CQRS.Web
{
    public class CommandHandlerFactoryComponentSelector : DefaultTypedFactoryComponentSelector
    {
        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            if (method.Name == "CreateByName" && arguments.Length == 1 && arguments[0] is string)
            {
                return (string)arguments[0];
            }
            return base.GetComponentName(method, arguments);
        }
    }
}