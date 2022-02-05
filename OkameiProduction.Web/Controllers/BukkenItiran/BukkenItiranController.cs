using System;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class BukkenItiranController : BaseController
    {
        // GET: 
        public ActionResult SetCondition()
        {
            var vm = new BukkenItiranModel();
            SetDropDownListItems(vm);

            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);

            return View(vm);
        }

        // GET: 
        public ActionResult DisplayResult ()
        {
            var vm = GetFromQueryString<BukkenItiranModel>();

            BukkenItiranBL bl = new BukkenItiranBL();
            var dt = bl.GetDisplayResult(vm);
            ViewBag.Data = dt;

            return View(vm);
        }




        // ----------------------------------------/
        // private
        private void SetDropDownListItems(BukkenItiranModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.TantouSitenSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.TantouSiten);
            vm.TantouEigyouSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.TantouEigyou);
            vm.TantouPcSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.TantouPc);
            vm.TantouCadSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.TantouCad);
            vm.KubunSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.Kubun);
            vm.TokuchuuzaiUmuSelectList = dl.GetTokuchuuzaiUmuDDLItems();
        }
    }
}