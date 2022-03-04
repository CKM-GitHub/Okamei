using System;
using System.Text;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Path = System.IO.Path;
using System.Web;

namespace OkameiProduction.Web.Controllers.HiuchiItiran
{
    public class HiuchiItiranController : BaseController
    {
        // GET: HiuchiItiran
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SetCondition()
        {
            var vm = new HiuchiItiranModel();
            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);
            SetDropDownListItems(vm);
            return View(vm);
        }
        private void SetDropDownListItems(HiuchiItiranModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.TantouSitenDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouSiten);
            vm.TantouEigyouDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouEigyou);
            vm.TantouCadDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouCad);
        }
        public ActionResult DisplayResult()
        {
            var vm = GetFromQueryString<HiuchiItiranModel>();
            var bl = new HiuchiItiranBL();
            var dt = bl.GetDisplayResult(vm);
            dt.Columns.Remove("KoumutenName");
            dt.AcceptChanges();
            ViewBag.Data = dt;

            return View(vm);
        }
        //Work in IDM of some PCs
        [HttpPost]
        [ValidateInput(false)]
        public void Export()
        {
            try
            {

                Response.Buffer = true;
                Response.ContentType = "application/OCTET-STREAM";
                Response.AddHeader("Access-Control-Allow-Methods", "GET, POST,PUT");

                Response.AppendHeader("Content-Disposition", "attachment; filename=123.txt");
                Response.TransmitFile(Server.MapPath("~/output/project/") + "123.txt");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Flush();
                Response.End();
            }
            catch (System.Exception e)
            {
            }
        }
        //PTK 2022/02/09
        //Tried and spent 3 days in most of trammited Response  or Via Xmr. Failed and Thought current method would be fine in "ControllerAPI via Anchor Link" download procedure.  
        //In this procedure, this might work both IDM and build in chrome download.
        [HttpPost]
        public void ExportHiuchiPdf(HiuchiItiranModel model, DataTable dtExternal= null)  // if dtExternal exist, call as  ExportHiuchiPdf(new HiuchiItiranModel/null,dt)
        {
            //Initilize
            #region initialize
            var dt = new DataTable();
            var fbname = "";
            string FileName = "";
            PDF_Font font_Class = new PDF_Font();
            string font_folder = System.Web.Hosting.HostingEnvironment.MapPath("~/fonts/");
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/output/project");
            Font font_Header = font_Class.CreateJapaneseFontHeader(font_folder);
            Font font = font_Class.CreateJapaneseFont(font_folder, 35);
            Font font_Color = font_Class.CreateJapaneseFont_Color(font_folder);
            if (!Directory.Exists("~/output"))
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/output"));
            if (!Directory.Exists("~/output/project"))
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/output/project"));
            #endregion
            //Foreign DataTable
            if (dtExternal == null)
            {
                var vm = GetFromQueryString<HiuchiItiranModel>();
                var bl = new HiuchiItiranBL();
                dt = bl.GetDisplayResult(vm);
                dt = dt.Select(" BukkenNo = '" + model.TantouSitenCD.Split('_')[1].ToString() + "' and BukkenName ='" + model.TantouSitenCD.Split('_')[2].ToString() + "' and SouName = '" + model.TantouSitenCD.Split('_')[3].ToString() + "'").CopyToDataTable();
                fbname = model.TantouSitenCD.Split('_')[2].ToString();
                FileName = "火打ラベル_" + fbname + ".pdf";
            }
            else
            {
                dt = dtExternal;
                fbname = dt.Rows[0]["BukkenName"].ToString();
                FileName = "火打材ラベル_" + fbname + ".pdf";
                model = new HiuchiItiranModel();
            }
            //Create PDF
            var doc1 = new iTextSharp.text.Document();
            #region DocSet
            doc1.SetPageSize(PageSize.A4.Rotate());
            var mstr = new FileStream(path + @"/" + FileName, FileMode.Create);
            var writer = PdfWriter.GetInstance(doc1, mstr);
            doc1.Open();

            var Tablea = new PdfPTable(5);
            Tablea.AddCell(new PdfPCell(new Phrase("火 　 打　  材", font_Class.CreateJapaneseFont(font_folder, 30)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 50,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                PaddingBottom = 0f,
                Colspan = 5
            });

            var TempVal = dt.Rows[0]["KoumutenName"].ToString() + " 様";
            var GetFont = ShrinkValue(TempVal);
            
            Tablea.AddCell(new PdfPCell(new Phrase(TempVal, font_Class.CreateJapaneseFont(font_folder, GetFont.Item1)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 70,
                BorderWidthBottom = 1,
                BorderWidthTop = 1,
                BorderWidthLeft = 0.3f,
                BorderWidthRight = 1f,
                PaddingBottom =GetFont.Item2,
                Colspan = 5
            });

            Tablea.AddCell(new PdfPCell(new Phrase("  ", font_Class.CreateJapaneseFont(font_folder)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 4,
                BorderWidthBottom = 0f,
                BorderWidthTop = 0f,
                BorderWidthLeft = 0f,
                BorderWidthRight = 0f,
                PaddingBottom = 10f,
                Colspan = 5
            });

            TempVal = dt.Rows[0]["BukkenName"].ToString();
            Tablea.AddCell(new PdfPCell(new Phrase(TempVal, font_Class.CreateJapaneseFont(font_folder, 65, 1)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 85,
                BorderWidthBottom = 1,
                BorderWidthTop = 1,
                BorderWidthLeft = 0.3f,
                BorderWidthRight = 1f,
                PaddingBottom = 18f,
                SpaceCharRatio = 4f,
                Colspan = 5
            });

            Tablea.AddCell(new PdfPCell(new Phrase("  ", font_Class.CreateJapaneseFont(font_folder)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 4,
                BorderWidthBottom = 0f,
                BorderWidthTop = 0f,
                BorderWidthLeft = 0f,
                BorderWidthRight = 0f,
                PaddingBottom = 5f,
                Colspan = 5
            });

            TempVal = dt.Rows[0]["SouName"].ToString();
            Tablea.AddCell(new PdfPCell(new Phrase(TempVal, font_Class.CreateJapaneseFont(font_folder, 50, 1)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 70,
                BorderWidthBottom = 1,
                BorderWidthTop = 1,
                BorderWidthLeft = 0.3f,
                BorderWidthRight = 1f,
                PaddingBottom = 15f,
                Colspan = 5,
                PaddingLeft = 25f

            });

            //Zairyou
            TempVal = "";
            if (dtExternal != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TempVal += dr["Zairyou"].ToString() + Environment.NewLine + Environment.NewLine;
                }
                model.Zairyou = TempVal;
            }

            Tablea.AddCell(new PdfPCell(new Phrase(model.Zairyou.Trim(), font_Class.CreateJapaneseFont(font_folder, 19)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP,
                FixedHeight = 170,
                BorderWidthBottom = 1,
                BorderWidthTop = 0,
                BorderWidthLeft = 0.3f,
                BorderWidthRight = 0,
                PaddingTop = 28f,
                PaddingBottom = 10f,
                PaddingLeft = 10f,  
                Colspan = 3, 
            }) ;

            //Toukyuu
            TempVal = "";
            if (dtExternal != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TempVal += dr["Toukyuu"].ToString() + Environment.NewLine + Environment.NewLine; 
                }
                model.Toukyuu = TempVal;
            }
            Tablea.AddCell(new PdfPCell(new Phrase(model.Toukyuu.Trim(), font_Class.CreateJapaneseFont(font_folder, 19)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP,
                FixedHeight = 170,
                BorderWidthBottom = 1,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                PaddingTop = 28f,
                PaddingBottom = 10f,
                Colspan = 1

                //PaddingRight = 10f
                //PaddingLeft=15f
            });

            //Honsuu
            TempVal = "";
            if (dtExternal != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TempVal += dr["honsuu"].ToString() + "本" + Environment.NewLine + Environment.NewLine;

                }
                model.Honsuu = TempVal;
            }
            Tablea.AddCell(new PdfPCell(new Phrase(model.Honsuu.Trim() + (dtExternal== null ? "本" : "") , font_Class.CreateJapaneseFont(font_folder, 19)))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_TOP,
                FixedHeight = 170,
                BorderWidthBottom = 1,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 1f,
                PaddingTop = 28f,
                PaddingBottom = 10f,
                PaddingRight = 10f,
                //PaddingRight = 25f,
                Colspan = 1

            });

            Tablea.AddCell(new PdfPCell(new Phrase("株式会社岡本銘木店　三田工場 ", font_Class.CreateJapaneseFont(font_folder, 15)))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 30,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                PaddingBottom = 0,
                Colspan = 5
            });

            Tablea.AddCell(new PdfPCell(new Phrase("TEL　079-568ｰ2657    ", font_Class.CreateJapaneseFont(font_folder, 15)))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 30,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                PaddingBottom = 0,
                Colspan = 5,
                PaddingRight = 63f

            });
            doc1.Add(Tablea);
            doc1.Close();

            #endregion 
        }
        public byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        private Chunk GetChunk(string text)
        {
            Chunk chunk = new Chunk(text);
            chunk.SetCharacterSpacing(3f);
            return chunk;
        }
        public Font CreateJapaneseFontHeader(string font_folder)
        {
            BaseFont baseFT = BaseFont.CreateFont(font_folder + "SIMSUN.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font = new iTextSharp.text.Font(baseFT, 13, Font.BOLD);
            return null;
        }

        //May work in IDM 
        private void DownloadFile(string pth)
        {
            HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.pdf");

            // Write the file to the Response  
            const int bufferLength = 1024 * 1024;
            byte[] buffer = new Byte[bufferLength];
            int length = 0;
            Stream download = null;
            try
            {
                download = new FileStream(pth, FileMode.Open, FileAccess.Read);
                do
                {
                    if (Response.IsClientConnected)
                    {
                        length = download.Read(buffer, 0, bufferLength);
                        Response.OutputStream.Write(buffer, 0, length);
                        buffer = new Byte[bufferLength];
                    }
                    else
                    {
                        length = -1;
                    }
                }
                while (length > 0);
                Response.Flush();
                Response.End();
            }
            finally
            {
                if (download != null)
                    download.Close();
            }
        }
        public (int,float) ShrinkValue(string val)
        {
            int LengthShrink = Convert.ToInt32(Encoding.GetEncoding(932).GetByteCount(val).ToString());

            if (LengthShrink > 40)
            {
                return (21,5f);
            }
            else if (LengthShrink > 30)
            {
                return (27, 7f);
            }
            else if (LengthShrink > 20)
            {
                return (35,7f);
            }
            else if (LengthShrink > 10)
            {
                return (45,9f);
            }
            else
            {
                return (49,9f);
            }
        }
    }
     
}