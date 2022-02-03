using System;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class HundeggerItiranController : BaseController
    {
        // GET: 
        public ActionResult SetCondition()
        {
            var vm = new HundeggerItiranModel();

            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);

            return View(vm);
        }

        // GET: 
        public ActionResult DisplayResult ()
        {
            var vm = GetFromQueryString<HundeggerItiranModel>();

            HundeggerItiranBL bl = new HundeggerItiranBL();
            var dt = bl.GetDisplayResult(vm);
            ViewBag.Data = dt;

            return View(vm);
        }
        
    }
}