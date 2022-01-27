using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OkameiProduction.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(string id)
        {
            return View();
        }
    }
}