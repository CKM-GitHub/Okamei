using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class InputBukkenShousaiController : BaseController
    {
        // GET:
        public ActionResult Entry()
        {
            var vm = GetFromQueryString<InputBukkenShousaiModel>();
            if (vm.Mode == EMode.None)
            {
                return this.HttpNotFound();
            }

            ViewBag.PreviousUrl = base.GetPreviousUrl();

            if (vm.Mode == EMode.Edit || vm.Mode == EMode.Delete)
            {
                var bl = new InputBukkenShousaiBL();
                var dt = bl.GetDisplayResult(vm);
                if (dt.Rows.Count == 0)
                {
                    vm.BukkenNO = "";
                }
                else
                {
                    var mode = vm.Mode;
                    vm = dt.AsEnumerableEntity<InputBukkenShousaiModel>().FirstOrDefault();
                    vm.Mode = mode;
                }
            }

            SetDropDownListItems(vm);
            return View(vm);
        }

        private void SetDropDownListItems(InputBukkenShousaiModel vm)
        {
            CommonBL bl = new CommonBL();
            vm.TantouSitenDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouSiten);
            vm.TantouPcDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouPc);
            vm.TantouCadDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouCad);
            vm.NyuuryokusakiDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.Nyuuryokusaki);
            vm.KubunDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.Kubun);
            vm.KanamonoDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.Kanamono);
            vm.GouhanDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.Gouhan);
            vm.TokuchuuzaiUmuDropDownListItems = bl.GetTokuchuuzaiUmuDropDownListItems();
            vm.WithOrWithoutDropDownListItems = bl.GetWithOrWithoutDropDownListItems();

            if (string.IsNullOrEmpty(vm.TantouSitenCD))
            {
                vm.TantouEigyouDropDownListItems = new List<DropDownListItem>();
            }
            else
            {
                vm.TantouEigyouDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouEigyou, vm.TantouSitenCD);
            }
        }

        // GET:
        [SessionFilter(IsRedirectedToLoginPage = false)]
        [BrowsingHistory(Disable = true)]
        public ActionResult DownloadFiles(string BukkenNO, string BukkenFileRowsCsv)
        {
            var decodedFileRowsCsv = HttpUtility.UrlDecode(BukkenFileRowsCsv);
            if (string.IsNullOrEmpty(BukkenNO) && string.IsNullOrEmpty(decodedFileRowsCsv))
            {
                return this.HttpNotFound();
            }

            if (string.IsNullOrEmpty(StaticCache.AttachedFilePath))
            {
                return this.HttpNotFound();
            }

            try
            {
                var bl = new InputBukkenShousaiBL();
                var dt = bl.GetBukkenFileName(BukkenNO, decodedFileRowsCsv);
                //データがない場合も空のzipファイルを返す

                var filePath = Path.Combine(StaticCache.AttachedFilePath, BukkenNO);
                var zipFileName = BukkenNO + ".zip";

                //メモリ不足になるようならCreateZipFilePathResultにする。
                return CreateZipContentResult(zipFileName, filePath, dt);
                //return CreateZipFilePathResult(zipFileName, filePath, dt);
            }
            catch (Exception)
            {
                return this.HttpNotFound();
            }
        }

        private FilePathResult CreateZipFilePathResult(string zipFileName, string filePath, DataTable fileNames)
        {
            var tempZipFilePath = Path.Combine(StaticCache.AttachedFilePath, Guid.NewGuid() + "_" + zipFileName);

            using (ZipArchive zipArchive = ZipFile.Open(Path.Combine(filePath, tempZipFilePath), ZipArchiveMode.Create))
            {
                foreach (DataRow dr in fileNames.Rows)
                {
                    var fileName = dr["BukkenFileName"].ToStringOrEmpty();
                    var fileFullName = Path.Combine(filePath, fileName);

                    if (System.IO.File.Exists(fileFullName))
                    {
                        ZipArchiveEntry zipEntry = zipArchive.CreateEntryFromFile(fileFullName, fileName);
                    }
                }
            }
            return File(tempZipFilePath, "application/zip", zipFileName);
        }

        private FileContentResult CreateZipContentResult(string zipFileName, string filePath, DataTable fileNames)
        {
            using (MemoryStream zipArchiveMemoryStream = new MemoryStream())
            {
                using (ZipArchive zipArchive = new ZipArchive(zipArchiveMemoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (DataRow dr in fileNames.Rows)
                    {
                        var fileName = dr["BukkenFileName"].ToStringOrEmpty();
                        var fileFullName = Path.Combine(filePath, fileName);

                        if (System.IO.File.Exists(fileFullName))
                        {
                            ZipArchiveEntry zipEntry = zipArchive.CreateEntry(fileName);
                            using (Stream entryStream = zipEntry.Open())
                            {
                                using (MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(fileFullName)))
                                {
                                    ms.CopyTo(entryStream);
                                }
                            }
                        }
                    }
                }
                return File(zipArchiveMemoryStream.ToArray(), "application/zip", zipFileName);
            }
        }



        //Hiuchi SubForm -------------------->
        [SessionFilter(IsRedirectedToLoginPage = false)]
        [HttpPost]
        public ActionResult HiuchiSubEntry()
        {
            var vm = new InputBukkenShousaiHiuchiModel();
            var form = System.Web.HttpContext.Current.Request.Form;
            vm.BukkenNO = form["BukkenNO"].ToStringOrEmpty();
            vm.BukkenName = form["BukkenName"].ToStringOrEmpty();

            if (string.IsNullOrEmpty(vm.BukkenNO))
            {
                return this.HttpNotFound();
            }

            var bl = new InputBukkenShousaiHiuchiBL();
            var dt = bl.GetBukkenHiuchiData(vm);
            if (dt.Rows.Count > 0)
            {
                var bukkenName = vm.BukkenName;
                vm = dt.AsEnumerableEntity<InputBukkenShousaiHiuchiModel>().FirstOrDefault();
                vm.BukkenName = bukkenName;
            }

            SetDropDownListItemsHiuchi(vm);
            return PartialView(vm);
        }

        private void SetDropDownListItemsHiuchi(InputBukkenShousaiHiuchiModel vm)
        {
            CommonBL bl = new CommonBL();
            vm.SouDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.HiuchiSou);
        }
        //Hiuchi SubForm <--------------------



        //Tekakou SubForm -------------------->
        [SessionFilter(IsRedirectedToLoginPage = false)]
        [HttpPost]
        public ActionResult TekakouSubEntry()
        {
            var vm = new InputBukkenShousaiTekakouModel();
            var form = System.Web.HttpContext.Current.Request.Form;
            vm.BukkenNO = form["BukkenNO"].ToStringOrEmpty();
            vm.BukkenName = form["BukkenName"].ToStringOrEmpty();

            if (string.IsNullOrEmpty(vm.BukkenNO))
            {
                return this.HttpNotFound();
            }

            var bl = new InputBukkenShousaiTekakouBL();
            var dt = bl.GetBukkenTekakouData(vm);
            if (dt.Rows.Count > 0)
            {
                var bukkenName = vm.BukkenName;
                vm = dt.AsEnumerableEntity<InputBukkenShousaiTekakouModel>().FirstOrDefault();
                vm.BukkenName = bukkenName;
            }

            return PartialView(vm);
        }

        //Tekakou SubForm <--------------------    



        //Moulder SubForm -------------------->
        [SessionFilter(IsRedirectedToLoginPage = false)]
        [HttpPost]
        public ActionResult MoulderSubEntry()
        {
            var vm = new InputBukkenShousaiMoulderModel();
            var form = System.Web.HttpContext.Current.Request.Form;
            vm.BukkenNO = form["BukkenNO"].ToStringOrEmpty();
            vm.BukkenName = form["BukkenName"].ToStringOrEmpty();

            if (string.IsNullOrEmpty(vm.BukkenNO))
            {
                return this.HttpNotFound();
            }

            var bl = new InputBukkenShousaiMoulderBL();
            var dt = bl.GetBukkenMoulderData(vm);
            if (dt.Rows.Count > 0)
            {
                var bukkenName = vm.BukkenName;
                vm.Records = dt.AsEnumerableEntity<InputBukkenShousaiMoulderRow>().ToList();
                vm.BukkenName = bukkenName;
            }
            else
            {
                vm.Records = new List<InputBukkenShousaiMoulderRow>();
            }

            SetDropDownListItemsMoulder(vm);
            return PartialView(vm);
        }

        private void SetDropDownListItemsMoulder(InputBukkenShousaiMoulderModel vm)
        {
            CommonBL bl = new CommonBL();
            vm.HinmokuDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.MoulderHinmoku);
            vm.ZairyouDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.MoulderZairyou);
        }
        //Moulder SubForm <--------------------    
    }
}