using System.Web.Mvc;

namespace OkameiProduction.Web.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            Session["UserInfo"] = null;
            return View();
        }
    }
}