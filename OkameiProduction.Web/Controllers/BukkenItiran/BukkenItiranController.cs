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
        public ActionResult DisplayResult (string id)
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
            vm.SitenSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.Siten);
            vm.EigyouStaffSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.EigyouStaff);
            vm.PCSupportSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.PCSupport);
            vm.CADStaffSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.CADStaff);
            vm.KubunSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.Kubun);
            vm.TokuchuuzaiUmuSelectList = dl.GetTokuchuuzaiUmuDDLItems();
        }
    }
}