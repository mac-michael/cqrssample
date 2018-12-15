namespace CQRS.Base.DDD.Domain.SharedKernel.Specification
{
    public class NotSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _wrapped;

        public NotSpecification(ISpecification<T> wrapped)
        {
            _wrapped = wrapped;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return !_wrapped.IsSatisfiedBy(candidate);
        }
    }
}