using CQRS.DDD.Domain.Annotations;

namespace CQRS.Erp.Sales
{
    [DomainRepository]
    public interface IInvoiceRepository
    {
        void Save(Invoice invoice);
    }
}