using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class BukkenShousaiApiController : BaseApiController
    {
        [HttpPost]
        public string GetTantouEigyouSelectList([FromBody]string sitenCD)
        {
            if (sitenCD == null) return GetBadRequestResult();

            var bl = new CommonBL();
            return ConvertToJsonResult(bl.GetMultiPorposeDDLItems(EMultiPorpose.EigyouStaff, sitenCD));
        }
        [HttpPost]
        public string GetKoumutenSelectList([FromBody]string sitenCD)
        {
            if (sitenCD == null) return GetBadRequestResult();

            var bl = new CommonBL();
            return ConvertToJsonResult(bl.GetMultiPorposeDDLItems(EMultiPorpose.Koumuten, sitenCD));
        }

        [HttpPost]
        public string GetBukkenNO([FromBody]string sitenCD)
        {
            if (sitenCD == null) return GetBadRequestResult();

            var bl = new CommonBL();
            return ConvertToJsonResult(new { NewBukkenNO = bl.GetNewBukkenNO(sitenCD) });
        }

        public string SaveData([FromBody] BukkenShousaiModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new BukkenShousaiBL();
            return "";

        }
    }
}
