using System;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class HanyouKensakuController : BaseController
    {
        // GET: 
        public ActionResult DisplayResult(HanyouKensakuModel vm)
        {
            //var vm = new HanyouKensakuModel();
            if (string.IsNullOrEmpty(vm.ID))
                vm.ID = "-1";
            else if (vm.ID == "0")
                vm.ID = "000";
            SetDropDownListItems(vm);

            HanyouKensakuBL bl = new HanyouKensakuBL();
            var dt = bl.GetDisplayResult(vm);
            ViewBag.Data = dt;

            return View(vm);
        }

        // ----------------------------------------/
        // private
        private void SetDropDownListItems(HanyouKensakuModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.IDSelectList = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.ID);
        }
    }
}