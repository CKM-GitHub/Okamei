using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class CadStaffMiteiApiController : BaseApiController
    {
        [HttpPost]
        public string ExistsDisplayResult([FromBody] CadStaffMiteiModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new CadStaffMiteiBL();
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
