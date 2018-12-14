using System.Linq;

namespace CQRS.Base.DDD.Domain.SharedKernel.Specification
{
    public class DisjunctionSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T>[] _disjunction;

        public DisjunctionSpecification(ISpecification<T>[] disjunction)
        {
            _disjunction = disjunction;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return _disjunction.Any(spec => spec.IsSatisfiedBy(candidate));
        }
    }
}