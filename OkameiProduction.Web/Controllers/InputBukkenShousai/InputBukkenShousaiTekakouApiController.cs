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
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class InputBukkenShousaiTekakouApiController : BaseApiController
    {
        [HttpPost]
        public string SaveTekakouData([FromBody] InputBukkenShousaiTekakouModel model)
        {
            if (model == null) return GetBadRequestResult();

            var msgid = "";
            var bl = new InputBukkenShousaiTekakouBL();
            if (bl.CreateBukkenTekakouData(model, out msgid))
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
