using System;
using System.Text;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;
using System.Data;
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
            
            ViewBag.Data =  dt;

            return View(vm);
        }
        private System.Data.DataTable LimitBytes(System.Data.DataTable dt)
        {
            //foreach (DataRow dr in dt.Rows)
            //{
            //    dr["KubunName"] = GetBytes(dr["KubunName"].ToString(), 32);

            //}
            return dt;
        }

        private string GetBytes(string input, Int32 maxLenth)
        {
            string result = input;
            int bytecount = Encoding.UTF8.GetByteCount(input);
            if (bytecount > maxLenth)
            {
                var byteArray = Encoding.UTF8.GetBytes(input);
                result = Encoding.UTF8.GetString(byteArray, 0, maxLenth);
            }
            return result;
        }
    }
}