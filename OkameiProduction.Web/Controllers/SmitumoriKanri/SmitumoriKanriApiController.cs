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

        [HttpPost]
        public bool UserIDCheck([FromBody] SmitumoriKanriModel model)
        {
            var bl = new SmitumoriKanriBL();
            return bl.UserIDCheck(model);
        }


        [HttpPost]
        public bool DeleteSMitumoriCounterData([FromBody] SmitumoriKanriModel model)
        {
            var bl = new SmitumoriKanriBL();
            return bl.DeleteSMitumoriCounterData(model);
        }
    }
}
