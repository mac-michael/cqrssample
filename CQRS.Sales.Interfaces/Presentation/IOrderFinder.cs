using System.Linq;

namespace CQRS.Sales.Interfaces.Presentation
{
    public interface IOrderFinder
    {
        IQueryable<ClientOrderListItemDto> FindOrders();
        ClientOrderDetailsDto GetClientOrderDetails(int orderId);
    }
}