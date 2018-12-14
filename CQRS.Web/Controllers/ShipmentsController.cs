using System.Web.Mvc;
using CQRS.Base.CQRS.Commands;
using CQRS.Shipping.Interfaces.Application.Handlers;
using CQRS.Shipping.Interfaces.Presentation;

namespace CQRS.Web.Controllers
{
    public class ShipmentsController : Controller
    {
        public IShipmentFinder ShipmentFinder { get; set; }

        public IGate Gate { get; set; }

        public ActionResult Index()
        {
            return View(ShipmentFinder.FindShipment());
        }

        public ActionResult Send(int shipmentId)
        {
            Gate.Dispatch(new SendShipmentCommand(shipmentId));
            return RedirectToAction("Index");
        }

        public ActionResult Deliver(int shipmentId)
        {
            Gate.Dispatch(new DeliverShipmentCommand(shipmentId));
            return RedirectToAction("Index");
        }
    }
}
