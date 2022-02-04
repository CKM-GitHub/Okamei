using System.Collections.Generic;
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
