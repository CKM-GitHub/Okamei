using System.Data;
using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class HanyouMasterMaintenanceApiController : BaseApiController
    {
        [HttpPost]
        public string CheckModifiedData([FromBody] HanyouMasterMaintenanceModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new HanyouMasterMaintenanceBL();
            DataTable dt;
            if (model.Key == "ID_Check")
            {
                dt = bl.GetIDDataResult(model);
                if (dt.Rows.Count > 0)
                {
                    if(dt.Columns.Contains("MessageID"))
                        return GetErrorResult("E141");
                    else
                        return DataTableToJSON(dt);
                }
                else
                {
                    return GetErrorResult("E101");
                }
            }
            else
            {
                dt = bl.GetDataResult(model);
                if (dt.Rows.Count > 0)
                {
                    return GetErrorResult("E107");
                }
                else
                {
                    return GetSuccessResult();
                }
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
