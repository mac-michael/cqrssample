using System.Linq;
using CQRS.Base.CQRS.Query.Attributes;
using CQRS.Base.Infrastructure.NHibernate;
using CQRS.Sales.Domain;
using CQRS.Sales.Interfaces.Presentation;
using NHibernate.Linq;

namespace CQRS.Sales.Presentation.Implementation
{
    [Finder]
    public class OrderFinder : IOrderFinder
    {
        public IEntityManager EntityManager { get; set; }

        public IQueryable<ClientOrderListItemDto> FindOrders()
        {
            var clientOrderListItemDtos = from o in EntityManager.CurrentSession.Query<Order>()
                                                 select new ClientOrderListItemDto (o.Id, o.TotalCost, o.SubmitDate, o.OrderStatus);
            
            return clientOrderListItemDtos;
        }

        public ClientOrderDetailsDto GetClientOrderDetails(int orderId)
        {
            var o = EntityManager.CurrentSession.Get<Order>(orderId);

            return new ClientOrderDetailsDto
                       {
                           OrderId = o.Id,
                           OrderStatus = o.OrderStatus,
                           SubmitDate = o.SubmitDate,
                           TotalCost = o.TotalCost,
                           OrderedProducts = o.OrderedProducts.ToList()
                       };
        }
    }
}