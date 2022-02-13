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
            var dtMonth = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);
            ViewBag.ServerDate = dtMonth.Remove(dtMonth.Length - 3).Replace("-","/"); 
            return View(vm);
        } 

    }
}
