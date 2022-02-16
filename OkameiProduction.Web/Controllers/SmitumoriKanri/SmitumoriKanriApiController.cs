using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class SmitumoriKanriApiController : BaseApiController
    {
        [HttpPost]
        public string ExistsDisplayResult([FromBody] SmitumoriKanriModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new SmitumoriKanriBL();
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
