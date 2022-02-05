using System;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class FusezuMiteisyutuController : BaseController
    {
        // GET: 
        public ActionResult SetCondition()
        {
            var vm = new FusezuMiteisyutuModel();
            SetDropDownListItems(vm);

            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);

            return View(vm);
        }

        // GET: 
        public ActionResult DisplayResult ()
        {
            var vm = GetFromQueryString<FusezuMiteisyutuModel>();

            FusezuMiteisyutuBL bl = new FusezuMiteisyutuBL();
            var dt = bl.GetDisplayResult(vm);
            ViewBag.Data = dt;

            return View(vm);
        }




        // ----------------------------------------/
        // private
        private void SetDropDownListItems(FusezuMiteisyutuModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.TantouCadSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.TantouCad);
        }
    }
}