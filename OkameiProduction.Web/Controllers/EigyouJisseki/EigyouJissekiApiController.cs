using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class EigyouJissekiApiController : BaseApiController
    {
        // GET: EigyouJissekiApi

        [HttpPost]
        public string ExistsDisplayResult([FromBody] EigyouJissekiModel model)
        {
            if (model == null) return GetBadRequestResult();
            var bl = new EigyouJissekiBL();
            if (bl.GetDisplayResult(model).Rows.Count > 0)
            {
                return GetSuccessResult();
            }
            else
            {
                return GetErrorResult("S013");
            }
        }
         
    }
}