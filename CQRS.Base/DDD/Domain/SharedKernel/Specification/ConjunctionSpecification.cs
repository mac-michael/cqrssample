using System.Linq;

namespace CQRS.Base.DDD.Domain.SharedKernel.Specification
{
    public class ConjunctionSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T>[] _conjunction;

        public ConjunctionSpecification(params ISpecification<T>[] conjunction)
        {
            _conjunction = conjunction;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return _conjunction.All(spec => spec.IsSatisfiedBy(candidate));
        }
    }
}