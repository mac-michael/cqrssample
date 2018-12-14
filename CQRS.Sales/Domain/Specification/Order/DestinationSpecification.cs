using CQRS.Base.DDD.Domain.SharedKernel.Specification;

namespace CQRS.Sales.Domain.Specification.Order
{
    public class DestinationSpecification : CompositeSpecification<Domain.Order>
    {	
        private Locale[] _allowedLocale;
	
        public DestinationSpecification(params Locale[] allowedLocale) 
        {	
            _allowedLocale = allowedLocale;
        }

        public override bool IsSatisfiedBy(Domain.Order order) 
        {
            //TODO check if order destination is on allowedLocale
            return true;
        }
    }

    public class Locale
    {
        public static Locale China { get{ return new Locale();} }
    }
}