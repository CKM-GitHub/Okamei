using System;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class HanyouMasterMaintenanceController : BaseController
    {
        // GET: 
        public ActionResult SetCondition(HanyouMasterMaintenanceModel vm)
        {
            if (vm.Mode  == 0)
                vm.Mode = EMode.New;
            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);

            return View(vm);
        }
    }
}