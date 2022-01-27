using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class BukkenItiranApiController : BaseApiController
    {
        [HttpPost]
        public string ExistsDisplayResult([FromBody] BukkenItiranModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new BukkenItiranBL();
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
