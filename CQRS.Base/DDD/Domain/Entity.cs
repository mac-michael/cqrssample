using CQRS.Base.DDD.Domain.Annotations;

namespace CQRS.Base.DDD.Domain
{
    [DomainEntity]
    public abstract class Entity
    {
        public int Id { get; private set; }
        public EntityStatus Status { get; private set; }

        public void MarkAsRemoved()
        {
            Status = EntityStatus.Archived;
        }
    }
}