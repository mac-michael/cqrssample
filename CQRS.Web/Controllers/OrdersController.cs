using System.Web.Mvc;
using System.Linq;
using CQRS.Sales.Interfaces.Presentation;

namespace CQRS.Web.Controllers
{
    public class OrdersController : Controller
    {
        public IOrderFinder OrderFinder { get; set; }

        public ActionResult Index()
        {
            IQueryable<ClientOrderListItemDto> query= OrderFinder.FindOrders();

            //query = query.Where(x => x.Status == OrderStatus.Draft);
            //query = query.Where(x => x.TotalCost.Value < 4);
            //query = query.Where(x => x.OrderId ==1);
            //IQueryable<int> queryable = query.Select(x => x.OrderId);

            return View(query.ToList());
        }
    }
}