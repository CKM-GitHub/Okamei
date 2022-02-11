using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class InputStepApiController : BaseApiController
    {
        [HttpPost]
        public string ExistsDisplayResult([FromBody] InputStepModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new InputStepBL();
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
