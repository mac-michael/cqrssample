using System.Collections.Generic;

namespace CQRS.Shipping.Interfaces.Presentation
{
    public interface IShipmentFinder
    {
        List<ShipmentDto> FindShipment();
    }
}
