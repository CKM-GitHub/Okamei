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

namespace OkameiProduction.Web.Controllers.HiuchiItiran
{
    public class HiuchiItiranController : BaseController
    {
        // GET: HiuchiItiran
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hiuchi()
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
        [HttpPost]
        public void ExportHiuchiPdf(HiuchiItiranModel model)
        {
            var vm = GetFromQueryString<HiuchiItiranModel>();
            var bl = new HiuchiItiranBL();
            var dt = bl.GetDisplayResult(vm);

            dt = dt.Select(" BukkenNo = '" + model.TantouSitenCD.Split('_')[1].ToString() + "' and BukkenName ='" + model.TantouSitenCD.Split('_')[2].ToString() + "' and SouName = '" + model.TantouSitenCD.Split('_')[3].ToString() + "'").CopyToDataTable();
            PDFPrint(dt, model.TantouSitenCD.Split('_')[2].ToString());
            // dt.Columns.Remove("KoumutenName"); 
             
        } 
        public void PDFPrint(DataTable dt,string fbname)
        {
            //火打ラベル_
            //ReplaceText(@"D:\Projects\Okame\Okamei\OkameiProduction.Web\output\project\Doc1.pdf");
            PDF_Font font_Class = new PDF_Font();
            string font_folder = Server.MapPath("~/fonts/");
            Font font_Header = font_Class.CreateJapaneseFontHeader(font_folder);
            Font font = font_Class.CreateJapaneseFont(font_folder,35);
            Font font_Color = font_Class.CreateJapaneseFont_Color(font_folder);
            if (!Directory.Exists("~/output"))
                Directory.CreateDirectory(Server.MapPath("~/output"));
            if (!Directory.Exists("~/output/project"))
                Directory.CreateDirectory(Server.MapPath("~/output/project"));
            var doc1 = new iTextSharp.text.Document();
            doc1.SetPageSize(PageSize.A4.Rotate()); 
            string path = Server.MapPath("~/output/project");
            string FileName = "火打ラベル_" + fbname + ".pdf";
            var writer=  PdfWriter.GetInstance(doc1, new FileStream(path + @"/"+FileName, FileMode.Create));
            doc1.Open();
           

            var tablea = new PdfPTable(3);
            tablea.AddCell(new PdfPCell(new Phrase("火　打　材", font_Class.CreateJapaneseFont(font_folder, 25)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 50,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                PaddingBottom = 0f,
                Colspan = 3
            });
            
            var tempVal = dt.Rows[0]["KoumutenName"].ToString()  + "様";
            //tempVal =  //System.IO.File.ReadAllLines(@"D:\ptk.txt").Last();
            var GetFont =ShrinkValue(tempVal);
            //  (Convert.ToInt32(System.IO.File.ReadAllLines(@"D:\ptk.txt").First()));

            tablea.AddCell(new PdfPCell(new Phrase(tempVal,font_Class.CreateJapaneseFont(font_folder,  GetFont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 70,
                BorderWidthBottom = 1,
                BorderWidthTop = 1,
                BorderWidthLeft = 0.3f,
                BorderWidthRight = 1f,
                PaddingBottom = 5f,
                Colspan = 3
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
                Colspan = 3
            });

            tempVal = dt.Rows[0]["BukkenName"].ToString()  ;
            tablea.AddCell(new PdfPCell(new Phrase(tempVal, font_Class.CreateJapaneseFont(font_folder,65,1)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 85,
                BorderWidthBottom = 1,
                BorderWidthTop = 1,
                BorderWidthLeft = 0.3f,
                BorderWidthRight = 1f,
                PaddingBottom = 15f,
                SpaceCharRatio=4f,
                Colspan = 3
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
                Colspan = 3
            }); 
            tempVal = dt.Rows[0]["SouName"].ToString()  ; 
            tablea.AddCell(new PdfPCell(new Phrase(tempVal, font_Class.CreateJapaneseFont(font_folder, 50,1)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 70,
                BorderWidthBottom = 1,
                BorderWidthTop = 1,
                BorderWidthLeft = 0.3f,
                BorderWidthRight = 1f,
                PaddingBottom = 15f,
                Colspan = 3,
                PaddingLeft = 25f

            }); 
            tempVal= ""; 
            foreach (DataRow dr in dt.Rows)
            {
                tempVal += dr["zairyou"].ToString() + Environment.NewLine ;

            }
            tablea.AddCell(new PdfPCell(new Phrase(tempVal, font_Class.CreateJapaneseFont(font_folder, 15)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP,
                FixedHeight = 170,
                BorderWidthBottom = 1,
                BorderWidthTop = 0,
                BorderWidthLeft = 0.3f,
                BorderWidthRight = 0,
                PaddingBottom = 5f ,
                PaddingLeft = 25f
            });
            tempVal = ""; 
            foreach (DataRow dr in dt.Rows)
            {
                tempVal += dr["toukyuu"].ToString() + Environment.NewLine;

            }
            tablea.AddCell(new PdfPCell( new Phrase(tempVal, font_Class.CreateJapaneseFont(font_folder, 15)))
            { 
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_TOP,
                FixedHeight = 170,
                BorderWidthBottom = 1,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                PaddingBottom = 5f 
            }); 
            tempVal = ""; 
            foreach (DataRow dr in dt.Rows)
            {
                tempVal += dr["honsuu"].ToString() + "本" + Environment.NewLine;

            } 
            tablea.AddCell(new PdfPCell(new Phrase(tempVal, font_Class.CreateJapaneseFont(font_folder, 15)))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_TOP,
                FixedHeight = 170,
                BorderWidthBottom = 1,
                BorderWidthTop = 0,
                BorderWidthLeft =0,
                BorderWidthRight = 1f,
                PaddingBottom = 5f ,
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
                Colspan = 3
            }); 
            tablea.AddCell(new PdfPCell(new Phrase("TEL　079-568ｰ2657    " , font_Class.CreateJapaneseFont(font_folder, 15)))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 30,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                PaddingBottom = 0,
                Colspan = 3,
                PaddingRight = 63f

            });
            doc1.Add(tablea);  
            doc1.Close();


            FileStream fs = new FileStream(path + "\\" + FileName, FileMode.Open, FileAccess.Read);
            MemoryStream ms = new MemoryStream();
            fs.CopyTo(ms);
            //string FolderName = Server.MapPath("~/output/project/");
            //using (MemoryStream ms = new MemoryStream())
            //using (FileStream fs = new FileStream(( path+"\\"+ FileName), FileMode.OpenOrCreate, FileAccess.Write))
            //{  
            //        fs.CopyTo(ms);
                 byte[] ByteData = ms.ToArray();
 
                // Write output PDF on user download path 
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.BinaryWrite(ByteData);
                Response.End();
            //}
            fs.Close();
            ms.Close();




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