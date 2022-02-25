using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class InputBukkenShousaiMoulderApiController : BaseApiController
    {
        [HttpPost]
        public string SaveMoulderData([FromBody] InputBukkenShousaiMoulderModel model)
        {
            if (model == null) return GetBadRequestResult();

            model.Records = JsonSerializer.Deserialize<List<InputBukkenShousaiMoulderRow>>(model.RecordsJson);

            var msgid = "";
            var bl = new InputBukkenShousaiMoulderBL();
            if (bl.CreateBukkenMoulderData(model, out msgid))
            {
                return GetSuccessResult();
            }
            else
            {
                return GetErrorResult(msgid);
            }
        }
    }
}
