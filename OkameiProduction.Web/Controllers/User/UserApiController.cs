using System.Web.Http;
using OkameiProduction.BL;
using Models;

namespace OkameiProduction.Web.Controllers
{
    public class UserApiController : BaseApiController
    {
        [HttpPost]
        public string SelectForLogin([FromBody] UserModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new UserBL();
            if (bl.SelectForLogin(model))
            {
                CreateSession("UserInfo", model.UserID + '_' + model.UserName);
                return ConvertToJsonResult(model);
            }
            else
            {
                return GetErrorResult("E101");
            }
        }
    }
}
