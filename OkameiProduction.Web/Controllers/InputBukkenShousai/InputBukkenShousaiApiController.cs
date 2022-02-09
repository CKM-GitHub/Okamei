using System;
using System.Data;
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
        public string GetBukkenFile([FromBody]InputBukkenShousaiBukkenFileModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new InputBukkenShousaiBL();
            return DataTableToJSON(bl.GetBukkenFile(model));
        }

        [HttpPost]
        public string GetBukkenComment([FromBody]InputBukkenShousaiBukkenCommentModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new InputBukkenShousaiBL();
            return DataTableToJSON(bl.GetBukkenComment(model));
        }

        [HttpPost]
        public string GetBukkenNO([FromBody]string tantouSitenCD)
        {
            if (tantouSitenCD == null) return GetBadRequestResult();

            var bl = new InputBukkenShousaiBL();
            return ConvertToJsonResult(new { NewBukkenNO = bl.GetNewBukkenNO(tantouSitenCD) });
        }

        [HttpPost]
        public string DeleteBukkenFile([FromBody] InputBukkenShousaiBukkenFileModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new InputBukkenShousaiBL();
            if (bl.DeleteBukkenFile(model, out string msgid))
            {
                return GetSuccessResult();
            }
            else
            {
                return GetErrorResult(msgid);
            }
        }

        [HttpPost]
        public string DeleteBukkenComment([FromBody] InputBukkenShousaiBukkenCommentModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new InputBukkenShousaiBL();
            if (bl.DeleteBukkenComment(model, out string msgid))
            {
                return GetSuccessResult();
            }
            else
            {
                return GetErrorResult(msgid);
            }
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
        public string SaveBukkenComment([FromBody] InputBukkenShousaiBukkenCommentModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new InputBukkenShousaiBL();
            if (!bl.CreateBukkenComment(model, out string msgid))
            {
                return GetErrorResult(msgid);
            }
            else
            {
                return GetSuccessResult();
            }
        }

        [HttpPost]
        public string UploadFiles()
        {
            var model = base.GetFromRequestForm<InputBukkenShousaiBukkenFileModel>();
            if (model == null || string.IsNullOrEmpty(model.BukkenNO))
            {
                return GetBadRequestResult();
            }

            if (string.IsNullOrEmpty(StaticCache.UploadedFilePath))
            {
                throw new CustomException("ファイルを保存するフォルダが設定されていません。");
            }

            var postedFiles = HttpContext.Current.Request.Files;
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
