using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class InputBukkenShousaiApiController : BaseApiController
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

        [HttpPost]
        public string SaveData([FromBody] InputBukkenShousaiModel model)
        {
            if (model == null) return GetBadRequestResult();

            var result= false;
            var msgid = "";
            var bl = new InputBukkenShousaiBL();

            if (model.Mode == EMode.New) result = bl.CreateBukkenAll(model, out msgid);
            if (model.Mode == EMode.Edit) result = bl.UpdateBukkenAll(model, out msgid);
            if (model.Mode == EMode.Delete) result = bl.CreateBukkenAll(model, out msgid);

            if (!result)
            {
                return GetErrorResult(msgid);
            }
            else
            {
                return GetSuccessResult();
            }
        }
    }
}
