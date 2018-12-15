using CQRS.Base.Infrastructure.Attributes;

namespace CQRS.Base.CQRS.Commands
{
    [Component]
    public class StandardGate : IGate 
    {
        public RunEnvironment RunEnvironment { get; set; }

        public void Dispatch<T>(T command)
        {
            RunEnvironment.Run(command);
        }
    }
}