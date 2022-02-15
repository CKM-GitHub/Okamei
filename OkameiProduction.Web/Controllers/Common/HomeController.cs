using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OkameiProduction.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string SaveMenuState(string categoryID)
        {
            //メニューの開かれているカテゴリIDを保存
            Session["MenuState"] = categoryID;
            return "true";
        }
    }
}