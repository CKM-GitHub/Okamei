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
            string userid = model.UserID;
            if (bl.SelectForLogin(model))
            {
                Session["UserInfo"] = userid + '_' + model.UserName;
                Session["UserID"] = model.UserID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["UserInfo"] = null;
                Session["UserID"] = null;
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