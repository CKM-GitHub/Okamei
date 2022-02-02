using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class MoulderItiranApiController : BaseApiController
    {
        [HttpPost]
        public string ExistsDisplayResult([FromBody] MoulderItiranModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new MoulderItiranBL();
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
