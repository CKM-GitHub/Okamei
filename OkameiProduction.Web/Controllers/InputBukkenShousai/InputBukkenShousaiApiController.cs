using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net.Http.Headers;
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
        public string GetKoumutenSuggestItems([FromBody]string tantouSitenCD)
        {
            if (tantouSitenCD == null) return GetBadRequestResult();

            var bl = new CommonBL();
            var array = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.Koumuten, tantouSitenCD).Select(r => r.DisplayText).ToArray();
            return ConvertToJsonResult(array);
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

            if (model.Mode == EMode.New) result = bl.CreateBukken(model, out msgid);
            if (model.Mode == EMode.Edit) result = bl.UpdateBukken(model, out msgid);
            if (model.Mode == EMode.Delete) result = bl.DeleteBukken(model, out msgid);

            if (!result)
            {
                return GetErrorResult(msgid);
            }

            if (model.BukkenFileShurui == 1 || model.BukkenFileShurui == 3 || model.BukkenFileShurui == 4)
            {
                bl.SendMail(model);
            }
            return GetSuccessResult();
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
        public async Task<string> UploadFiles()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return GetUnsupportedMediaTypeResult();
            }

            var model = base.GetFromRequestForm<InputBukkenShousaiBukkenFileModel>();
            if (model == null || string.IsNullOrEmpty(model.BukkenNO))
            {
                return GetBadRequestResult();
            }

            if (string.IsNullOrEmpty(StaticCache.AttachedFilePath))
            {
                throw new CustomException("ファイルを保存するフォルダが設定されていません。");
            }

            string path = Path.Combine(StaticCache.AttachedFilePath, model.BukkenNO);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var provider = new MultipartFormDataStreamProvider(path);
            await Request.Content.ReadAsMultipartAsync(provider);

            var bl = new InputBukkenShousaiBL();
            foreach (var file in provider.FileData)
            {
                string fileName = file.Headers.ContentDisposition.FileName;
                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                {
                    fileName = fileName.Trim('"');
                }
                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                {
                    fileName = Path.GetFileName(fileName);
                }

                var fileFullName = Path.Combine(path, fileName);
                if (File.Exists(fileFullName))
                {
                    File.Delete(fileFullName);
                }
                File.Move(Path.Combine(path, file.LocalFileName), fileFullName);

                model.BukkenFileName = fileName;
                if (!bl.CreateBukkenFile(model, out string msgid))
                {
                    return GetErrorResult(msgid);
                }
            }

            return GetSuccessResult();
        }

        //async taskがうまくいかなったらこちら。
        //[HttpPost]
        //public string UploadFiles()
        //{
        //    var model = base.GetFromRequestForm<InputBukkenShousaiBukkenFileModel>();
        //    if (model == null || string.IsNullOrEmpty(model.BukkenNO))
        //    {
        //        return GetBadRequestResult();
        //    }

        //    if (string.IsNullOrEmpty(StaticCache.AttachedFilePath))
        //    {
        //        throw new CustomException("ファイルを保存するフォルダが設定されていません。");
        //    }

        //    var postedFiles = HttpContext.Current.Request.Files;
        //    string path = Path.Combine(StaticCache.AttachedFilePath, model.BukkenNO);
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }

        //    var bl = new InputBukkenShousaiBL();
        //    foreach (string key in postedFiles)
        //    {
        //        var file = postedFiles[key];
        //        file.SaveAs(Path.Combine(path, file.FileName));

        //        model.BukkenFileName = file.FileName;
        //        if (!bl.CreateBukkenFile(model, out string msgid))
        //        {
        //            return GetErrorResult(msgid);
        //        }
        //    }
        //    return GetSuccessResult();
        //}



        //Hiuchi ---------->
        [HttpPost]
        public string GetZairyouSuggestItems()
        {
            var bl = new CommonBL();
            var array = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.HiuchiZairyou).Select(r => r.DisplayText).ToArray();
            return ConvertToJsonResult(array);
        }

        [HttpPost]
        public string GetToukyuuSuggestItems()
        {
            var bl = new CommonBL();
            var array = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.HiuchiToukyuu).Select(r => r.DisplayText).ToArray();
            return ConvertToJsonResult(array);
        }

        [HttpPost]
        public string SaveHiuchiData([FromBody] InputBukkenShousaiHiuchiModel model)
        {
            if (model == null) return GetBadRequestResult();

            var msgid = "";
            var bl = new InputBukkenShousaiBL();
            //if (bl.CreateOrUpdateBukkenHiuchi(model))
            //{
            //    return GetSuccessResult();
            //}
            //else
            //{
            //    return GetErrorResult(msgid);
            //}
            return GetSuccessResult();
        }

        private class ImportData
        {
            public int Index { get; set; }
            public string Sou { get; set; }
            public string SouName { get; set; }
            public string Zairyou { get; set; }
            public string Toukyuu { get; set; }
            public string Honsuu { get; set; }
        }
        [HttpPost]
        public string ImportHiuchiCsv()
        {
            var postedFiles = HttpContext.Current.Request.Files;
            if (postedFiles.Count == 0)
            {
                return GetSuccessResult();
            }

            var postedFile = postedFiles[0];
            if (string.IsNullOrEmpty(postedFile.FileName))
            {
                return GetSuccessResult();
            }

            string path = HttpContext.Current.Server.MapPath("~/temp");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileName = Path.Combine(path, "HiuchiImport_" + Guid.NewGuid() + ".csv");
            postedFile.SaveAs(fileName);

            List<ImportData> list = new List<ImportData>();
            try
            {
                Encoding encoding = Encoding.GetEncoding("Shift_JIS");
                CommonBL bl = new CommonBL();
                using (var reader = new StreamReader(fileName, encoding))
                {                    
                    string[] readArray = reader.ReadLine().Split(',');
                    if (readArray.Length < 13) //title
                    {
                        return GetErrorResult("E102");
                    }

                    int counter = 0;
                    while (!reader.EndOfStream)
                    {
                        readArray = reader.ReadLine().Split(',');
                        if (readArray[0] != "木材火打") continue;

                        var data = new ImportData
                        {
                            SouName = readArray[1].ToStringOrEmpty().TrimEnd(),
                            Zairyou = string.Format("{0} {1}×{2}", readArray[2], readArray[4], readArray[5]),
                            Toukyuu = readArray[3].ToStringOrEmpty(),
                            Honsuu = readArray[7].ToStringOrEmpty()
                        };

                        var dt = bl.GetMultiPorposeByIDChar2(EMultiPorpose.HiuchiSou, data.SouName);
                        if (dt.Rows.Count == 0)
                        {
                            return GetErrorResult("E285");
                        }
                        data.Sou = dt.Rows[0]["Key"].ToStringOrEmpty();

                        if (encoding.GetByteCount(data.Zairyou) > 30 
                        || encoding.GetByteCount(data.Toukyuu) > 10
                        || encoding.GetByteCount(data.Honsuu) > 3)
                        {
                            return GetErrorResult("E290");
                        }

                        counter++;
                        data.Index = counter;
                        list.Add(data);
                    }
                    reader.Close();
                }
            }
            finally
            {
                File.Delete(fileName);
            }

            var model = new InputBukkenShousaiHiuchiModel();
            var query = list.GroupBy(r => r.Sou).Select(r => r.Key);
            foreach (var key in query)
            {
                var target = list.Where(r => r.Sou == key).OrderBy(r => r.Index);
                if (string.IsNullOrEmpty(model.Sou1))
                {
                    foreach (var data in target)
                    {
                        if (string.IsNullOrEmpty(model.Zairyou11))
                        {
                            model.Sou1 = data.Sou;
                            model.Zairyou11 = data.Zairyou;
                            model.Toukyuu11 = data.Toukyuu;
                            model.Honsuu11 = data.Honsuu;
                            continue;
                        }
                        if (string.IsNullOrEmpty(model.Zairyou12))
                        {
                            model.Zairyou12 = data.Zairyou;
                            model.Toukyuu12 = data.Toukyuu;
                            model.Honsuu12 = data.Honsuu;
                            continue;
                        }
                        if (string.IsNullOrEmpty(model.Zairyou13))
                        {
                            model.Zairyou13 = data.Zairyou;
                            model.Toukyuu13 = data.Toukyuu;
                            model.Honsuu13 = data.Honsuu;
                            continue;
                        }
                        break;
                    }
                }
                else if (string.IsNullOrEmpty(model.Sou2))
                {
                    foreach (var data in target)
                    {
                        if (string.IsNullOrEmpty(model.Zairyou21))
                        {
                            model.Sou2 = data.Sou;
                            model.Zairyou21 = data.Zairyou;
                            model.Toukyuu21 = data.Toukyuu;
                            model.Honsuu21 = data.Honsuu;
                            continue;
                        }
                        if (string.IsNullOrEmpty(model.Zairyou22))
                        {
                            model.Zairyou22 = data.Zairyou;
                            model.Toukyuu22 = data.Toukyuu;
                            model.Honsuu22 = data.Honsuu;
                            continue;
                        }
                        if (string.IsNullOrEmpty(model.Zairyou23))
                        {
                            model.Zairyou23 = data.Zairyou;
                            model.Toukyuu23 = data.Toukyuu;
                            model.Honsuu23 = data.Honsuu;
                            continue;
                        }
                        break;
                    }
                }
                else if (string.IsNullOrEmpty(model.Sou3))
                {
                    foreach (var data in target)
                    {
                        if (string.IsNullOrEmpty(model.Zairyou31))
                        {
                            model.Sou3 = data.Sou;
                            model.Zairyou31 = data.Zairyou;
                            model.Toukyuu31 = data.Toukyuu;
                            model.Honsuu31 = data.Honsuu;
                            continue;
                        }
                        if (string.IsNullOrEmpty(model.Zairyou32))
                        {
                            model.Zairyou32 = data.Zairyou;
                            model.Toukyuu32 = data.Toukyuu;
                            model.Honsuu32 = data.Honsuu;
                            continue;
                        }
                        if (string.IsNullOrEmpty(model.Zairyou33))
                        {
                            model.Zairyou33 = data.Zairyou;
                            model.Toukyuu33 = data.Toukyuu;
                            model.Honsuu33 = data.Honsuu;
                            continue;
                        }
                        break;
                    }
                }
                else if (string.IsNullOrEmpty(model.Sou4))
                {
                    foreach (var data in target)
                    {
                        if (string.IsNullOrEmpty(model.Zairyou41))
                        {
                            model.Sou4 = data.Sou;
                            model.Zairyou41 = data.Zairyou;
                            model.Toukyuu41 = data.Toukyuu;
                            model.Honsuu41 = data.Honsuu;
                            continue;
                        }
                        if (string.IsNullOrEmpty(model.Zairyou42))
                        {
                            model.Zairyou42 = data.Zairyou;
                            model.Toukyuu42 = data.Toukyuu;
                            model.Honsuu42 = data.Honsuu;
                            continue;
                        }
                        if (string.IsNullOrEmpty(model.Zairyou43))
                        {
                            model.Zairyou43 = data.Zairyou;
                            model.Toukyuu43 = data.Toukyuu;
                            model.Honsuu43 = data.Honsuu;
                            continue;
                        }
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return ConvertToJsonResult(model);
        }

        [HttpPost]
        public HttpResponseMessage HiuchiPdfExport([FromBody]InputBukkenShousaiHiuchiPdfExportModel model)
        {
            #region CreateDataTable

            var dt = new DataTable();
            dt.Columns.Add("BukkenNO");
            dt.Columns.Add("BukkenName");
            dt.Columns.Add("KoumutenName");
            dt.Columns.Add("SouName");
            dt.Columns.Add("Zairyou");
            dt.Columns.Add("Toukyuu");
            dt.Columns.Add("Honsuu");

            DataRow dr = dt.NewRow();
            dr["BukkenNO"] = model.BukkenNO;
            dr["BukkenName"] = model.BukkenName;
            dr["KoumutenName"] = model.KoumutenName;
            dr["SouName"] = model.SouName;
            dr["Zairyou"] = model.Zairyou1;
            dr["Toukyuu"] = model.Toukyuu1;
            dr["Honsuu"] = model.Honsuu1;
            dt.Rows.Add(dr);

            if (!string.IsNullOrEmpty(model.Zairyou2))
            {
                dr = dt.NewRow();
                dr["BukkenNO"] = model.BukkenNO;
                dr["BukkenName"] = model.BukkenName;
                dr["KoumutenName"] = model.KoumutenName;
                dr["SouName"] = model.SouName;
                dr["Zairyou"] = model.Zairyou2;
                dr["Toukyuu"] = model.Toukyuu2;
                dr["Honsuu"] = model.Honsuu2;
                dt.Rows.Add(dr);
            }

            if (!string.IsNullOrEmpty(model.Zairyou3))
            {
                dr = dt.NewRow();
                dr["BukkenNO"] = model.BukkenNO;
                dr["BukkenName"] = model.BukkenName;
                dr["KoumutenName"] = model.KoumutenName;
                dr["SouName"] = model.SouName;
                dr["Zairyou"] = model.Zairyou3;
                dr["Toukyuu"] = model.Toukyuu3;
                dr["Honsuu"] = model.Honsuu3;
                dt.Rows.Add(dr);
            }
            #endregion

            HiuchiItiran.HiuchiItiranController PdfCaller = new HiuchiItiran.HiuchiItiranController();
            PdfCaller.ExportHiuchiPdf(new HiuchiItiranModel(), dt);

            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/output/project/" + (model.FileName.TrimEnd()));
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            byte[] bytes = File.ReadAllBytes(filePath);
            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentLength = bytes.LongLength;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = model.FileName;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(model.FileName));
            return response;
        }

    }
}
