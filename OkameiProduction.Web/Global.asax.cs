using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using OkameiProduction.BL;

namespace OkameiProduction.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //private const string _WebApiPrefix = "api";
        //private static string _WebApiExecutionPath = String.Format("~/{0}", _WebApiPrefix);

        //protected void Application_PostAuthorizeRequest()
        //{
        //    if (IsWebApiRequest())
        //    {
        //        HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        //    }
        //}

        //private static bool IsWebApiRequest()
        //{
        //    return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(_WebApiExecutionPath);
        //}

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            StaticCache.SetIniInfo();
            //StaticCache.SetMessageCache();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (Server != null)
            {
                var ex = Server.GetLastError();
                if (ex != null)
                {
                    if (ex is HttpException && 
                        ((HttpException)ex).GetHttpCode() == (int)HttpStatusCode.NotFound)
                    {
                        // NotFoundは無視
                        return;
                    }

                    if (ex is CustomException)
                    {
                        // Iniファイル設定
                        return;
                    }

                    try
                    {
                        string userInfo = "LoginUser:" + HttpContext.Current.Session["UserInfo"].ToStringOrEmpty();
                        Logger.GetInstance().Error(ex, userInfo);

                        if (ex.InnerException != null)
                        {
                            Logger.GetInstance().Error(ex.InnerException, userInfo);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    Server.ClearError();
                }
            }
        }
    }
}
