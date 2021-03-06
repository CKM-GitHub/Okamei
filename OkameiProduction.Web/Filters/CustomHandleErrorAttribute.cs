using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OkameiProduction.BL;

namespace OkameiProduction.Web
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            string userInfo = "LoginUser:" + HttpContext.Current.Session["UserInfo"].ToStringOrEmpty();
            Logger.GetInstance().Error(filterContext.Exception, userInfo);

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                //Application_Errorは呼ばれない
                HandleAjaxRequestException(filterContext);
            }
            else
            {
                //custom errorが有効ならApplication_Errorは呼ばれない
                base.OnException(filterContext);
            }
        }

        private void HandleAjaxRequestException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            filterContext.Result = new JsonResult
            {
                Data = new
                {
                    Message = filterContext.Exception.ToString(),
                    Message2 = filterContext.ToString()
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

        }
    }
}