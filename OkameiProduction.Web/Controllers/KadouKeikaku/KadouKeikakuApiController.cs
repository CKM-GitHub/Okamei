using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class KadouKeikakuApiController : BaseApiController
    {
        [HttpPost]
        public string ExistsDisplayResult([FromBody] KadouKeikakuModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new KadouKeikakuBL();
            if (bl.ExistsDisplayResult(model))
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
