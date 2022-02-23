using System;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class SmitumoriKanriController : BaseController
    {
        // GET: 
        public ActionResult SetCondition()
        {
            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);
            return View();
        }

        // GET: 
        public ActionResult DisplayResult ()
        {
            var vm = GetFromQueryString<SmitumoriKanriModel>();
            SetDropDownListItems(vm);

            SmitumoriKanriBL bl = new SmitumoriKanriBL();
            var dt = bl.GetDisplayResult(vm);
            ViewBag.Data = dt;

            return View(vm);
        }


        // private
        private void SetDropDownListItems(SmitumoriKanriModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.TantouCadDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouCad, "SmitumoriKanri");
            vm.KoumutenDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.Koumuten);
            vm.TantouSitenDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouSiten, "SmitumoriKanri");
            vm.IraiEigyouDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouEigyou, "SmitumoriKanri");
        }
    }
}