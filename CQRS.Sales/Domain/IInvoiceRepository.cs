using CQRS.Base.DDD.Domain.Annotations;

namespace CQRS.Sales.Domain
{
    [DomainRepository]
    public interface IInvoiceRepository
    {
        void Save(Invoice invoice);
    }
}