using System;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class KadouKeikakuController : BaseController
    {
        // GET: 
        public ActionResult SetCondition()
        {
            var vm = new KadouKeikakuModel();
            SetDropDownListItems(vm);

            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);

            return View(vm);
        }

        // GET: 
        public ActionResult DisplayResult ()
        {
            var vm = GetFromQueryString<KadouKeikakuModel>();

            KadouKeikakuBL bl = new KadouKeikakuBL();
            var dt = bl.GetDisplayResult(vm);
            ViewBag.Data = dt;

            return View(vm);
        }




        // ----------------------------------------/
        // private
        private void SetDropDownListItems(KadouKeikakuModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.TantouSitenDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouSiten);
            vm.TantouEigyouDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouEigyou);
            vm.TantouPcDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouPc);
            vm.TantouCadDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouCad);
        }
    }
}