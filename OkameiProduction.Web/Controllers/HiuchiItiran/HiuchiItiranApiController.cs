using System.Web.Http;
using Models;
using OkameiProduction.BL;
namespace OkameiProduction.Web.Controllers
{
    public class HiuchiItiranApiController : BaseApiController
    {
        public string BadRequest = "";
        public HiuchiItiranApiController()
        {
            BadRequest = GetBadRequestResult();
        }
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
