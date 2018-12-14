using System.Collections.Generic;
using System.Linq;
using CQRS.Base.CQRS.Query.Attributes;
using CQRS.Base.Infrastructure.NHibernate;
using CQRS.Shipping.Domain;
using CQRS.Shipping.Interfaces.Presentation;
using NHibernate.Linq;

namespace CQRS.Shipping.Infrastructure
{
    [Finder]
    public class ShipmentFinder : IShipmentFinder
    {
        public IEntityManager EntityManager { get; set; }

        public List<ShipmentDto> FindShipment()
        {
            return (from e in EntityManager.CurrentSession.Query<Shipment>()
                   select new ShipmentDto(e.Id, e.OrderId, e.ShipmentStatus)).ToList();
        }
    }
}