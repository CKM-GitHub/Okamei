using System;
using System.Text;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net;
using System.Collections.Generic;
using iTextSharp.text.pdf.parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
//using LocationTextExtractionStrategy = iTextSharp.text.pdf.parser.LocationTextExtractionStrategy;
using System.Linq;
using System.Text.RegularExpressions;
//using PDFExtraction;
using SautinSoft.Document;
using SautinSoft.Document.Drawing;
using Path = System.IO.Path;
using System.Web;
using System.Net.Http;
using iTextSharp.tool.xml;

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
        [HttpPost]
        public void ExportHiuchiPdf(HiuchiItiranModel model)
        {
            var vm = GetFromQueryString<HiuchiItiranModel>();
            var bl = new HiuchiItiranBL();
            var dt = bl.GetDisplayResult(vm); 
            dt = dt.Select(" BukkenNo = '" + model.TantouSitenCD.Split('_')[1].ToString() + "' and BukkenName ='" + model.TantouSitenCD.Split('_')[2].ToString() + "' and SouName = '" + model.TantouSitenCD.Split('_')[3].ToString() + "'").CopyToDataTable();
            var fbname = model.TantouSitenCD.Split('_')[2].ToString(); 
            PDF_Font font_Class = new PDF_Font();
            //System.Web.Hosting.HostingEnvironment.MapPath("~/output/project/123.txt");
            string font_folder = System.Web.Hosting.HostingEnvironment.MapPath("~/fonts/");
            Font font_Header = font_Class.CreateJapaneseFontHeader(font_folder);
            Font font = font_Class.CreateJapaneseFont(font_folder,35);
            Font font_Color = font_Class.CreateJapaneseFont_Color(font_folder);
            if (!Directory.Exists("~/output"))
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/output"));
            if (!Directory.Exists("~/output/project"))
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/output/project"));
            var doc1 = new iTextSharp.text.Document();
            #region DocSet
            doc1.SetPageSize(PageSize.A4.Rotate());
                        string path = System.Web.Hosting.HostingEnvironment.MapPath("~/output/project");
                        string FileName = "火打ラベル_" + fbname + ".pdf";
                        var mstr = new FileStream(path + @"/" + FileName, FileMode.Create); 
                        var writer = PdfWriter.GetInstance(doc1, mstr);
                        doc1.Open(); 
                        var tablea = new PdfPTable(4);
                        tablea.AddCell(new PdfPCell(new Phrase("火 　 打　  材", font_Class.CreateJapaneseFont(font_folder, 30)))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            FixedHeight = 50,
                            BorderWidthBottom = 0,
                            BorderWidthTop = 0,
                            BorderWidthLeft = 0,
                            BorderWidthRight = 0,
                            PaddingBottom = 0f,
                            Colspan = 4
                        });

                        var tempVal = dt.Rows[0]["KoumutenName"].ToString() + "様";
                        //tempVal =  //System.IO.File.ReadAllLines(@"D:\ptk.txt").Last();
                        var GetFont = ShrinkValue(tempVal);
                        //  (Convert.ToInt32(System.IO.File.ReadAllLines(@"D:\ptk.txt").First()));

                        tablea.AddCell(new PdfPCell(new Phrase(tempVal, font_Class.CreateJapaneseFont(font_folder, GetFont)))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            FixedHeight = 70,
                            BorderWidthBottom = 1,
                            BorderWidthTop = 1,
                            BorderWidthLeft = 0.3f,
                            BorderWidthRight = 1f,
                            PaddingBottom = 5f,
                            Colspan = 4
                        });

                        tablea.AddCell(new PdfPCell(new Phrase("  ", font_Class.CreateJapaneseFont(font_folder)))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            FixedHeight = 4,
                            BorderWidthBottom = 0f,
                            BorderWidthTop = 0f,
                            BorderWidthLeft = 0f,
                            BorderWidthRight = 0f,
                            PaddingBottom = 10f,
                            Colspan = 4
                        });

                        tempVal = dt.Rows[0]["BukkenName"].ToString();
                        tablea.AddCell(new PdfPCell(new Phrase(tempVal, font_Class.CreateJapaneseFont(font_folder, 65, 1)))
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
                            Colspan = 4
                        });
                        tablea.AddCell(new PdfPCell(new Phrase("  ", font_Class.CreateJapaneseFont(font_folder)))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            FixedHeight = 4,
                            BorderWidthBottom = 0f,
                            BorderWidthTop = 0f,
                            BorderWidthLeft = 0f,
                            BorderWidthRight = 0f,
                            PaddingBottom = 5f,
                            Colspan = 4
                        });
                        tempVal = dt.Rows[0]["SouName"].ToString();
                        tablea.AddCell(new PdfPCell(new Phrase(tempVal, font_Class.CreateJapaneseFont(font_folder, 50, 1)))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            FixedHeight = 70,
                            BorderWidthBottom = 1,
                            BorderWidthTop = 1,
                            BorderWidthLeft = 0.3f,
                            BorderWidthRight = 1f,
                            PaddingBottom = 15f,
                            Colspan = 4,
                            PaddingLeft = 25f

                        });
                        tempVal = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            tempVal += dr["zairyou"].ToString() +   Environment.NewLine+ Environment.NewLine;

                        }
                        tablea.AddCell(new PdfPCell(new Phrase(tempVal, font_Class.CreateJapaneseFont(font_folder, 22)))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            VerticalAlignment = Element.ALIGN_TOP,
                            FixedHeight = 170,
                            BorderWidthBottom = 1,
                            BorderWidthTop = 0,
                            BorderWidthLeft = 0.3f,
                            BorderWidthRight = 0,
                            PaddingTop=13f,
                            PaddingBottom = 10f,
                            PaddingLeft = 25f,
                            Colspan=2
                        });
                        tempVal = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            tempVal +=   dr["toukyuu"].ToString() + Environment.NewLine+Environment.NewLine;

                        }
                        tablea.AddCell(new PdfPCell(new Phrase(tempVal, font_Class.CreateJapaneseFont(font_folder, 22)))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            VerticalAlignment = Element.ALIGN_TOP,
                            FixedHeight = 170,
                            BorderWidthBottom = 1,
                            BorderWidthTop = 0,
                            BorderWidthLeft = 0,
                            BorderWidthRight = 0,
                            PaddingTop = 13f,
                            PaddingBottom = 10f, 
                            PaddingRight=10f
                        });
                        tempVal = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            tempVal +=   dr["honsuu"].ToString() + "本" + Environment.NewLine+ Environment.NewLine;

                        }
                        tablea.AddCell(new PdfPCell(new Phrase(tempVal, font_Class.CreateJapaneseFont(font_folder, 22)))
                        {
                            HorizontalAlignment = Element.ALIGN_RIGHT,
                            VerticalAlignment = Element.ALIGN_TOP,
                            FixedHeight = 170,
                            BorderWidthBottom = 1,
                            BorderWidthTop = 0,
                            BorderWidthLeft = 0,
                            BorderWidthRight = 1f,
                            PaddingTop = 13f,
                            PaddingBottom = 10f,
                            PaddingRight = 25f
                        });
                        tablea.AddCell(new PdfPCell(new Phrase("株式会社岡本銘木店　三田工場 ", font_Class.CreateJapaneseFont(font_folder, 15)))
                        {
                            HorizontalAlignment = Element.ALIGN_RIGHT,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            FixedHeight = 30,
                            BorderWidthBottom = 0,
                            BorderWidthTop = 0,
                            BorderWidthLeft = 0,
                            BorderWidthRight = 0,
                            PaddingBottom = 0,
                            Colspan = 4
                        });
                        tablea.AddCell(new PdfPCell(new Phrase("TEL　079-568ｰ2657    ", font_Class.CreateJapaneseFont(font_folder, 15)))
                        {
                            HorizontalAlignment = Element.ALIGN_RIGHT,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            FixedHeight = 30,
                            BorderWidthBottom = 0,
                            BorderWidthTop = 0,
                            BorderWidthLeft = 0,
                            BorderWidthRight = 0,
                            PaddingBottom = 0,
                            Colspan = 4,
                            PaddingRight = 63f

                        });
                        doc1.Add(tablea);
                        doc1.Close();

            #endregion 
        }
        public   byte[] ReadFully(Stream input)
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
                download = new FileStream(pth,   FileMode.Open,  FileAccess.Read);
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
        public int ShrinkValue(string val )
        { 
            int LengthShrink = Convert.ToInt32(Encoding.GetEncoding(932).GetByteCount(val).ToString()) ;
             
            if (LengthShrink > 40)
            {
                return 22;
            }
            else if (LengthShrink > 30  )
            {
                return 28;
            }
            else if (LengthShrink > 20)
            {
                return 36;
            }
            else if (LengthShrink > 10)
            {
                return 46;
            }
            else   
            {
                return 50;
            } 
        }
    }
     
}