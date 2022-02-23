using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OkameiProduction.Web
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class SessionFilterAttribute : ActionFilterAttribute
    {
        public bool IsRedirectedToLoginPage { get; set; } = true;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["UserInfo"] == null)
            {
                if (IsRedirectedToLoginPage)
                {
                    filterContext.Result = new RedirectResult("~/User/Login");
                    return;
                }
                else
                {
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                    return;
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }

}