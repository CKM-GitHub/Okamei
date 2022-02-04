using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            Session.RemoveAll();
            ViewBag.IsPostback = "false";
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            var bl = new UserBL();
            if (bl.SelectForLogin(model))
            {
                Session["UserInfo"] = model.UserID + '_' + model.UserName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["UserInfo"] = null;
                var msg = StaticCache.GetMessageInfo("E101");
                ViewBag.IsPostback = "true";
                ViewBag.MessageID = msg.MessageID;
                ViewBag.MessageText1 = msg.MessageText1;
                ViewBag.MessageIcon = msg.MessageIcon;
                return View();
            }
        }
    }
}