using System;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class UnsouSoukoireController : BaseController
    {
        // GET: 
        public ActionResult SetCondition()
        {
            var vm = new UnsouSoukoireModel();

            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);

            return View(vm);
        }

        // GET: 
        public ActionResult DisplayResult ()
        {
            var vm = GetFromQueryString<UnsouSoukoireModel>();

            UnsouSoukoireBL bl = new UnsouSoukoireBL();
            var dt = bl.GetDisplayResult(vm);
            ViewBag.Data = dt;

            return View(vm);
        }
    }
}