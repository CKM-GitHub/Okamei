using System;
using System.Data;
using System.Web;
using System.Text.Json;
using System.Web.Http;
using OkameiProduction.BL;
using Models;

namespace OkameiProduction.Web.Controllers
{
    [CustomExceptionFilterAttribute]
    [UserAuthentication]
    public class BaseApiController : ApiController
    {
        protected T GetFromRequestForm<T>() where T : new()
        {
            var obj = new T();
            var properties = typeof(T).GetProperties();
            var requestForm = HttpContext.Current.Request.Form;

            if (requestForm.Count == 0)
            {
                return obj;
            }

            foreach (var property in properties)
            {
                var valueAsString = requestForm[property.Name];
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
                if (Enum.TryParse(val, out EMode mode))
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
                return val.ToInt32(0);
            }
            if (type == typeof(Int16) || type == typeof(Int16?))
            {
                return val.ToInt16(0);
            }
            if (type == typeof(bool) || type == typeof(bool?))
            {
                return val.ToBoolean();
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                return val.ToDecimal(0);
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return val.ToDateTime();
            }
            else if (type == typeof(Byte) || type == typeof(Byte?))
            {
                return val.ToByte(0);
            }
            else
            {
                return val;
            }
        }

        protected string GetUnsupportedMediaTypeResult()
        {
            return JsonSerializer.Serialize(new { MessageID = "415", MessageText1 = "Unsupported Media Type", MessageIcon = "error" });
        }

        protected string GetBadRequestResult()
        {
            return JsonSerializer.Serialize(new { MessageID = "400", MessageText1 = "Bad Request", MessageIcon = "error" });
        }

        protected string GetSuccessResult()
        {
            return JsonSerializer.Serialize(new { Code = "success" });
        }

        protected string GetErrorResult(string messageid, string returnValue = "")
        {
            if (messageid.Length > 0)
            {
                var message = StaticCache.GetMessageInfo(messageid);
                return JsonSerializer.Serialize(
                    new { message.MessageID, message.MessageText1, message.MessageIcon, ReturnValue = returnValue });
            }
            else
            {
                return JsonSerializer.Serialize( 
                    new { MessageID = "Error", MessageText1 = "エラーが発生しました。", MessageIcon = "error", ReturnValue = returnValue });
            }
        }

        protected string ConvertToJsonResult<T>(T model)
        {
            return JsonSerializer.Serialize<T>(model);
        }

        protected string DataTableToJSON(DataTable table)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(table);
        }
    }
}
