using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class BukkenShousaiController : BaseController
    {
        // GET: BukkenShousai
        public ActionResult Entry()
        {
            ViewBag.PreviousUrl = base.GetPreviousUrl();

            var vm = GetFromQueryString<BukkenShousaiModel>();

            SetDropDownListItems(vm);
            return View(vm);
        }





        private void SetDropDownListItems(BukkenShousaiModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.SitenSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.Siten);
            //vm.EigyouStaffSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.EigyouStaff);
            vm.PCSupportSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.PCSupport);
            vm.CADStaffSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.CADStaff);
            //vm.KoumutenSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.Koumuten);
            vm.NyuuryokusakiSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.Nyuuryokusaki);
            vm.KubunSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.Kubun);
            vm.KanamonoSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.Kanamono);
            vm.GoubanSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.Gouban);

            vm.TokuchuuzaiUmuSelectList = dl.GetTokuchuuzaiUmuDDLItems();
        }
    }
}