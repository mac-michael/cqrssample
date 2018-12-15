using CQRS.Base.DDD.Domain.Annotations;
using CQRS.Base.Infrastructure.NHibernate.Repositories;
using CQRS.Shipping.Domain;

namespace CQRS.Shipping.Infrastructure
{
    [DomainRepositoryImplementation]
    public class ShipmentRepository : GenericRepositoryForBaseEntity<Shipment>, IShipmentRepository
    {
    }
}