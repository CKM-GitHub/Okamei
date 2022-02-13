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



        //Hiuchi -------------------->
        [HttpPost]
        public ActionResult HiuchiEntry()
        {
            var vm = new InputBukkenShousaiHiuchiModel();

            var request = System.Web.HttpContext.Current.Request;
            vm.BukkenNO = request.Form["BukkenNO"].ToStringOrEmpty();
            vm.BukkenName = request.Form["BukkenName"].ToStringOrEmpty();

            SetDropDownListItemsHiuchi(vm);
            return View(vm);
        }

        private void SetDropDownListItemsHiuchi(InputBukkenShousaiHiuchiModel vm)
        {
            CommonBL bl = new CommonBL();
            vm.SouDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.HiuchiSou);
        }
        //Hiuchi <--------------------

    }
}