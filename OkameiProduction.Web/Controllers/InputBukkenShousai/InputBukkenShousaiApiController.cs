using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class InputBukkenShousaiApiController : BaseApiController
    {
        [HttpPost]
        public string GetTantouEigyouDropDownListItems([FromBody]string tantouSitenCD)
        {
            if (tantouSitenCD == null) return GetBadRequestResult();

            var bl = new CommonBL();
            return ConvertToJsonResult(bl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouEigyou, tantouSitenCD));
        }
        [HttpPost]
        public string GetKoumutenDropDownListItems([FromBody]string tantouSitenCD)
        {
            if (tantouSitenCD == null) return GetBadRequestResult();

            var bl = new CommonBL();
            return ConvertToJsonResult(bl.GetMultiPorposeDropDownListItems(EMultiPorpose.Koumuten, tantouSitenCD));
        }

        [HttpPost]
        public string GetBukkenNO([FromBody]string tantouSitenCD)
        {
            if (tantouSitenCD == null) return GetBadRequestResult();

            var bl = new InputBukkenShousaiBL();
            return ConvertToJsonResult(new { NewBukkenNO = bl.GetNewBukkenNO(tantouSitenCD) });
        }

        [HttpPost]
        public string SaveData([FromBody] InputBukkenShousaiModel model)
        {
            if (model == null) return GetBadRequestResult();

            var result = false;
            var msgid = "";
            var bl = new InputBukkenShousaiBL();

            if (model.Mode == EMode.New) result = bl.CreateBukkenAll(model, out msgid);
            if (model.Mode == EMode.Edit) result = bl.UpdateBukkenAll(model, out msgid);
            if (model.Mode == EMode.Delete) result = bl.DeleteBukkenAll(model, out msgid);

            if (!result)
            {
                return GetErrorResult(msgid);
            }
            else
            {
                return GetSuccessResult();
            }
        }

        [HttpPost]
        public string UploadMultipleFiles()
        {
            if (string.IsNullOrEmpty(StaticCache.UploadedFilePath))
            {
                throw new CustomException("ファイルを保存するフォルダが設定されていません。");
            }

            var postedFiles = HttpContext.Current.Request.Files;
            var form = HttpContext.Current.Request.Form;

            var model = new InputBukkenShousaiBukkenFileModel()
            {
                BukkenNO = form["BukkenNO"].ToStringOrEmpty(),
                BukkenFileShurui = form["BukkenFileShurui"].ToByte(0)
            };
            if (string.IsNullOrEmpty(model.BukkenNO))
            {
                return GetBadRequestResult();
            }


            string path = Path.Combine(StaticCache.UploadedFilePath, model.BukkenNO);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            var bl = new InputBukkenShousaiBL();
            foreach (string key in postedFiles)
            {
                var file = postedFiles[key];
                file.SaveAs(Path.Combine(path, file.FileName));

                model.BukkenFileName = file.FileName;
                if (!bl.CreateBukkenFile(model, out string msgid))
                {
                    return GetErrorResult(msgid);
                }
            }
            return GetSuccessResult();
        }

    }
}
