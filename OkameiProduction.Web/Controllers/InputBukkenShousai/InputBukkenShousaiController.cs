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
                if (dt.Rows.Count > 0)
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
                //vm.KoumutenDropDownListItems = new List<DropDownListItem>();
            }
            else
            {
                vm.TantouEigyouDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouEigyou, vm.TantouSitenCD);
                //vm.KoumutenDropDownListItems = bl.GetMultiPorposeDropDownListItems(EMultiPorpose.Koumuten, vm.TantouSitenCD);
            }
        }



        // GET:
        public ActionResult DownloadFiles(string BukkenNO, string BukkenFileRowsCsv)
        {
            var decodedFileRowsCsv = HttpUtility.UrlDecode(BukkenFileRowsCsv);
            if (string.IsNullOrEmpty(BukkenNO) && string.IsNullOrEmpty(decodedFileRowsCsv))
            {
                return this.HttpNotFound();
            }

            if (string.IsNullOrEmpty(StaticCache.UploadedFilePath))
            {
                return this.HttpNotFound();
            }

            try
            {
                var bl = new InputBukkenShousaiBL();
                var dt = bl.GetBukkenFileName(BukkenNO, decodedFileRowsCsv);
                //データがない場合も空のzipファイルを返す

                string filePath = Path.Combine(StaticCache.UploadedFilePath, BukkenNO);
                string zipFileName = BukkenNO + ".zip";

                using (MemoryStream zipArchiveMemoryStream = new MemoryStream())
                {
                    using (ZipArchive zipArchive = new ZipArchive(zipArchiveMemoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (DataRow dr in dt.Rows)
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
            catch (Exception)
            {
                return this.HttpNotFound();
            }
        }
    }
}