using CQRS.DDD.Domain.SharedKernel.Specification;

namespace CQRS.Erp.Sales.Domain.Specification
{
    public class DestinationSpecification : CompositeSpecification<Order>
    {	
        private Locale[] _allowedLocale;
	
        public DestinationSpecification(params Locale[] allowedLocale) 
        {	
            _allowedLocale = allowedLocale;
        }

        public override bool IsSatisfiedBy(Order order) 
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