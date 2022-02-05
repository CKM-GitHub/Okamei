using System;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class MoulderItiranController : BaseController
    {
        // GET: 
        public ActionResult SetCondition()
        {
            var vm = new MoulderItiranModel();
            SetDropDownListItems(vm);

            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);

            return View(vm);
        }

        // GET: 
        public ActionResult DisplayResult ()
        {
            var vm = GetFromQueryString<MoulderItiranModel>();

            MoulderItiranBL bl = new MoulderItiranBL();
            var dt = bl.GetDisplayResult(vm);
            ViewBag.Data = dt;

            return View(vm);
        }




        // ----------------------------------------/
        // private
        private void SetDropDownListItems(MoulderItiranModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.TantouSitenSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.TantouSiten);
        }
    }
}