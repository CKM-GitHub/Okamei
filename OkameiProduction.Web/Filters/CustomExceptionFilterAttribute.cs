using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using OkameiProduction.BL;

namespace OkameiProduction.Web
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext == null)
            {
                throw new ArgumentNullException("actionExecutedContext");
            }

            Exception ex = actionExecutedContext.Exception;

            if (ex is NotImplementedException)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.NotImplemented, ex);
            }

            string userInfo = "LoginUser:" + HttpContext.Current.Session["UserInfo"].ToStringOrEmpty();
            Logger.GetInstance().Error(ex, userInfo);

            base.OnException(actionExecutedContext);
        }
    }
}