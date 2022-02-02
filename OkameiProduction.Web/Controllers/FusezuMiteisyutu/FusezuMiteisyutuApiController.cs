using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class FusezuMiteisyutuApiController : BaseApiController
    {
        [HttpPost]
        public string ExistsDisplayResult([FromBody] FusezuMiteisyutuModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new FusezuMiteisyutuBL();
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
