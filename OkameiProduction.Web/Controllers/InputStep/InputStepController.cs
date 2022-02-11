using System;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class InputStepController : BaseController
    {
        // GET: 
        public ActionResult SetCondition()
        {
            var vm = new InputStepModel();
            SetDropDownListItems(vm);

            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);

            return View(vm);
        }

        // GET: 
        public ActionResult DisplayResult ()
        {
            var vm = GetFromQueryString<InputStepModel>();

            InputStepBL bl = new InputStepBL();
            var dt = bl.GetDisplayResult(vm);
            ViewBag.Data = dt;

            return View(vm);
        }




        // ----------------------------------------/
        // private
        private void SetDropDownListItems(InputStepModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.TantouCadDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouCad);
        }
    }
}