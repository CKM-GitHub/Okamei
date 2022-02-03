using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class BukkenShousaiApiController : BaseApiController
    {
        [HttpPost]
        public string GetEigyouStaffSelectList(string id)
        {
            if (id == null) return GetBadRequestResult();

            var bl = new CommonBL();
            return ConvertToJsonResult(bl.GetMultiPorposeDDLItems(EMultiPorpose.EigyouStaff, id));
        }
        [HttpPost]
        public string GetKoumutenSelectList(string id)
        {
            if (id == null) return GetBadRequestResult();

            var bl = new CommonBL();
            return ConvertToJsonResult(bl.GetMultiPorposeDDLItems(EMultiPorpose.Koumuten, id));
        }

        [HttpPost]
        public string GetBukkenNO(string id)
        {
            if (id == null) return GetBadRequestResult();

            var bl = new CommonBL();
            return ConvertToJsonResult(new { NewBukkeNO = bl.GetNewBukkenNO(id) });
        }
    }
}
