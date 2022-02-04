using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class HanyouKensakuApiController : BaseApiController
    {
        [HttpPost]
        public string ExistsDisplayResult([FromBody] HanyouKensakuModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new HanyouKensakuBL();
            if (bl.GetDisplayResult(model).Rows.Count > 0)
            {
                return GetSuccessResult();
            }
            else
            {
                return GetErrorResult("S013");
            }
        }

        [HttpPost]
        public string GetDisplayResult([FromBody] HanyouKensakuModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new HanyouKensakuBL();
            return bl.DataTableToJSON(bl.GetDisplayResult(model));
        }
    }
}
