using System.Web.Http;
using Models;
using OkameiProduction.BL;
namespace OkameiProduction.Web.Controllers
{
    public class KubunTaisyouApiController : BaseApiController
    {

        [HttpPost]
        public string ExistsDisplayResult([FromBody] KubunTaisyouModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new KubunTaisyouBL();
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
