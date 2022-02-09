using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class UnsouSoukoireApiController : BaseApiController
    {
        [HttpPost]
        public string ExistsDisplayResult([FromBody] UnsouSoukoireModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new UnsouSoukoireBL();
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
