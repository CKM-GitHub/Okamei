using System;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers.KubunTaisyou
{
    public class KubunTaisyouController : BaseController
    {
        // GET: Classification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Classification()
        {
            var vm = new KubunTaisyouModel();
            SetDropDownListItems(vm);

            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);

            return View(vm); 
        }
        private void SetDropDownListItems(KubunTaisyouModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.SitenSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.Siten);
            vm.KubunSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.Kubun);
        }
        // GET: 
        public ActionResult DisplayResult()
        {
            var vm = GetFromQueryString<KubunTaisyouModel>();

            var bl = new KubunTaisyouBL();
            var dt = bl.GetDisplayResult(vm);
            ViewBag.Data = dt;

            return View(vm);
        }
    }
}