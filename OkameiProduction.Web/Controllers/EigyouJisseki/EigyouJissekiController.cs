using System;
using System.Text;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;
using System.Data;

namespace OkameiProduction.Web.Controllers.EigyouJisseki
{
    public class EigyouJissekiController : BaseController
    {

        public ActionResult SetCondition()
        {
            var vm = new EigyouJissekiModel();
            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);
            SetDropDownListItems(vm);
            return View(vm);
        }

        private void SetDropDownListItems(EigyouJissekiModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.TantouSitenDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouSiten);
            vm.TantouEigyouDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouEigyou);
            vm.TantouCadDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouCad);
        }

    }
}
