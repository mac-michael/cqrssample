using System;
using System.Web.Mvc;
using CQRS.Base.CQRS.Commands;
using CQRS.CRM.Interfaces.Commands;

namespace CQRS.Web.Controllers
{
    public class CrmController : Controller
    {
        public IGate Gate { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateLead(string name)
        {
            Gate.Dispatch(new CreateLeadCommand() {Id = Guid.NewGuid(), Name = name});

            return RedirectToAction("Index");
        }
    }
}