using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class HanyouMasterMaintenanceApiController : BaseApiController
    {
        [HttpPost]
        public string CheckDataResult([FromBody] HanyouMasterMaintenanceModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new HanyouMasterMaintenanceBL();
            if (bl.GetDataResult(model).Rows.Count > 0)
            {
                return GetSuccessResult();
            }
            else
            {
                return GetErrorResult("S013");
            }
        }

        [HttpPost]
        public string GetDataResult([FromBody] HanyouMasterMaintenanceModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new HanyouMasterMaintenanceBL();
            return DataTableToJSON(bl.GetDataResult(model));
        }

        [HttpPost]
        public string ModifyData([FromBody] HanyouMasterMaintenanceModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new HanyouMasterMaintenanceBL();
            return bl.ModifyData(model)? "true" : "false";
        }
    }
}
