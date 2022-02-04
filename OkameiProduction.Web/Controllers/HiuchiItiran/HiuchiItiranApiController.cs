using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class HiuchiItiranApiController : BaseApiController
    {
        [HttpPost]
        public string ExistsDisplayResult([FromBody] HiuchiItiranModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new HiuchiItiranBL();
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
