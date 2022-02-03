using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class HundeggerItiranApiController : BaseApiController
    {
        [HttpPost]
        public string ExistsDisplayResult([FromBody] HundeggerItiranModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new HundeggerItiranBL();
            if (bl.GetDisplayResult(model).Rows.Count >0)
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
