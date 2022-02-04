using System;
using System.Web;
using System.Web.Mvc;
using OkameiProduction.BL;
using Models;

namespace OkameiProduction.Web.Controllers
{
    [CustomHandleErrorAttribute]
    [BrowsingHistoryAttribute]
    [SessionFilter]
    public class BaseController : Controller
    {
        protected string GetPreviousUrl()
        {
            return Session[BrowsingHistoryAttribute.PREVIOUS_URL].ToStringOrEmpty();
        }

        protected T GetFromQueryString<T>() where T : new()
        {
            var obj = new T();
            var properties = typeof(T).GetProperties();
            var queryString = System.Web.HttpContext.Current.Request.QueryString;

            if (queryString.Count == 0)
            {
                return obj;
            }

            //if (queryString.Keys[0].ToStringOrEmpty() == "")
            //{
            //    var decode = HttpUtility.UrlDecode(queryString[0]);
            //    queryString = HttpUtility.ParseQueryString(decode);
            //}

            foreach (var property in properties)
            {
                var valueAsString = queryString[property.Name];
                var value = Parse(valueAsString, property.PropertyType);

                if (value == null)
                    continue;

                property.SetValue(obj, value, null);
            }
            return obj;
        }


        private object Parse(string val, Type type)
        {
            if (val == null) return val;

            if (type.Name == "EMode")
            {
                if (Enum.TryParse(HttpUtility.UrlDecode(val), out EMode mode))
                {
                    return mode;
                }
                else
                {
                    return EMode.None;
                }
            }
            else if (type == typeof(Int32) || type == typeof(Int32?))
            {
                return HttpUtility.UrlDecode(val).ToInt32(0);
            }
            if (type == typeof(Int16) || type == typeof(Int16?))
            {
                return HttpUtility.UrlDecode(val).ToInt16(0);
            }
            if (type == typeof(bool) || type == typeof(bool?))
            {
                return HttpUtility.UrlDecode(val).ToBoolean();
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                return HttpUtility.UrlDecode(val).ToDecimal(0);
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return HttpUtility.UrlDecode(val).ToDateTime();
            }
            else
            {
                return HttpUtility.UrlDecode(val);
            }
        }
    }
}