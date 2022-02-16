using System;
using System.Text;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;
using System.Data;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace OkameiProduction.Web.Controllers.CadInputStaff
{
    public class CadInputStaffController : BaseController
    {
        // GET: CadInputStaff
        public ActionResult Index()
        {
            return View();
        } 
        public ActionResult SetCondition()
        {
            var vm = new CadInputStaffModel();
            SetDropDownListItems(vm);
            var dtMonth = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);
            ViewBag.ServerDate = dtMonth;
            return View(vm);
        }
        private void SetDropDownListItems(CadInputStaffModel vm)
        {
            CommonBL dl = new CommonBL(); 
            vm.TantouCadDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouCad); 
        }
    }
}