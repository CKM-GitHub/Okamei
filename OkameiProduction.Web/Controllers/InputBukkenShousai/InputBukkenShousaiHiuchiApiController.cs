using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class InputBukkenShousaiHiuchiApiController : BaseApiController
    {
        [HttpPost]
        public string GetZairyouSuggestItems()
        {
            var bl = new CommonBL();
            var array = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.HiuchiZairyou).Select(r => r.DisplayText).ToArray();
            return ConvertToJsonResult(array);
        }

        [HttpPost]
        public string GetToukyuuSuggestItems()
        {
            var bl = new CommonBL();
            var array = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.HiuchiToukyuu).Select(r => r.DisplayText).ToArray();
            return ConvertToJsonResult(array);
        }

    }
}
