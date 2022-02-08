using System;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class CadStaffMiteiController : BaseController
    {
        // GET: 
        public ActionResult SetCondition()
        {
            var vm = new CadStaffMiteiModel();
            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);

            return View(vm);
        }

        // GET: 
        public ActionResult DisplayResult ()
        {
            var vm = GetFromQueryString<CadStaffMiteiModel>();

            CadStaffMiteiBL bl = new CadStaffMiteiBL();
            var dt = bl.GetDisplayResult(vm);
            ViewBag.Data = dt;

            return View(vm);
        }
    }
}