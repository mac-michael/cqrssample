using CQRS.Base.Infrastructure.Attributes;

namespace CQRS.Base.DDD.Application
{
    [Component]
    public class SystemUser : ISystemUser
    {
        public int UserId
        {
            get { return 1; }
        }
    }
}
