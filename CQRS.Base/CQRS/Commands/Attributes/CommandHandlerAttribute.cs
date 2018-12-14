using System;

namespace CQRS.Base.CQRS.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandHandlerAttribute : Attribute
    {
    }
}