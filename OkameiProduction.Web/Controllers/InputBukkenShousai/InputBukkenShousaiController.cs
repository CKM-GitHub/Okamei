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
    public class InputBukkenShousaiController : BaseController
    {
        // GET: InputBukkenShousai
        public ActionResult Entry()
        {
            var vm = GetFromQueryString<InputBukkenShousaiModel>();
            if (vm.Mode == EMode.Edit || vm.Mode == EMode.Delete) {
                var bl = new InputBukkenShousaiBL();
                ViewBag.Data = bl.GetDisplayResult(vm);
            }

            ViewBag.PreviousUrl = base.GetPreviousUrl();
            SetDropDownListItems(vm);
            return View(vm);
        }





        private void SetDropDownListItems(InputBukkenShousaiModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.TantouSitenDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouSiten);
            //vm.TantouEigyouDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouEigyou);
            vm.TantouPcDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouPc);
            vm.TantouCadDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouCad);
            //vm.KoumutenDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.Koumuten);
            vm.NyuuryokusakiDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.Nyuuryokusaki);
            vm.KubunDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.Kubun);
            vm.KanamonoDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.Kanamono);
            vm.GoubanDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.Gouban);

            vm.TokuchuuzaiUmuDropDownListItems = dl.GetTokuchuuzaiUmuDropDownListItems();
        }
    }
}