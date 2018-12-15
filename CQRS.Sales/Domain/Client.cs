using CQRS.Base.DDD.Domain;

namespace CQRS.Sales.Domain
{
    public class Client : AggregateRoot
    {
        public string Name { get; private set; }
        public string CompanyName { get; private set; }
        //public ClientAddress Address { get; private set; }
    }

    public class ClientAddress
    {
    }
}