using System; 
using System.Data; 
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf; 

namespace Models
{
    public class InputBukkenShousai_PurecattoJissekiExport
    {
        public InputBukkenShousai_PurecattoJissekiExport()
        {

        }
         
        public void Export(string Hostpath, string SavePath, string IniPath, DataTable dt)
        {
            try
            {
                //Create PDF
                PDF_Font font_Class = new PDF_Font();
                string font_folder = Hostpath;
                var doc1 = new iTextSharp.text.Document();
                #region DocSet
                doc1.SetPageSize(PageSize.A4);
                var mstr = new FileStream(SavePath, FileMode.Create);
                var writer = PdfWriter.GetInstance(doc1, mstr);
                doc1.Open();

                var Tablea = new PdfPTable(11);
                float[] widths = new float[] { 40f, 45f, 60f, 60f, 60f, 60f, 30f, 63f, 60f, 65f, 15f };
                //Tablea.SetTotalWidth(widths);
                Tablea.SetWidths(widths);
                
                Tablea.AddCell(new PdfPCell(new Phrase("プ  レ  カ  ッ  ト  実  績  日  報", font_Class.CreateJapaneseFont(font_folder, 20, 1)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 60,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 11,

                });
                //first Check
                Tablea.AddCell(new PdfPCell(new Phrase("▢", font_Class.CreateJapaneseFont(font_folder, 14)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    PaddingTop = 0f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    BackgroundColor = new BaseColor(204, 255, 204),

                    // Colspan = 1
                });
                Tablea.AddCell(new PdfPCell(new Phrase("住友林業物件", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2
                });

                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 6
                });

                Tablea.AddCell(new PdfPCell(new Phrase("作　成", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2
                });
                //second check
                Tablea.AddCell(new PdfPCell(new Phrase("▢", font_Class.CreateJapaneseFont(font_folder, 14)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingTop = 0f,
                    // PaddingBottom = 0f,
                    BackgroundColor = new BaseColor(204, 255, 204),
                    // Colspan = 1
                });

                Tablea.AddCell(new PdfPCell(new Phrase("イノス物件", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    // PaddingBottom = 0f,
                    Colspan = 2
                });

                Tablea.AddCell(new PdfPCell(new Phrase("[ 横架材ライン　Ａ・Ｂ ]", font_Class.CreateJapaneseFont(font_folder, 14)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    PaddingLeft = -30f,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 6
                });

                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 6
                });

                //Third Check
                Tablea.AddCell(new PdfPCell(new Phrase("▢", font_Class.CreateJapaneseFont(font_folder, 14)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    BackgroundColor = new BaseColor(204, 255, 204),//BaseColor.MAGENTA,
                    PaddingTop = 0f,
                    //PaddingBottom = 0f,
                    // Colspan = 1
                });

                Tablea.AddCell(new PdfPCell(new Phrase("一般物件", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingTop = 0f,
                    // PaddingBottom = 0f,
                    Colspan = 2,

                });

                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 6
                });

                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    //BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 6,

                });

                //BukkenName Check

                Tablea.AddCell(new PdfPCell(new Phrase("邸名", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });  
                //BukkenName  
                //var msze = File.ReadAllLines("D:\\pdf\\ptk.txt")[0].Split(',');
               // var BukkenName = msze[0];  //dt.Rows[0]["BukkenName"].ToString();
                Tablea.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["BukkenName"].ToString(), font_Class.CreateJapaneseFont(font_folder,  15)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 3f,
                    Colspan = 5,
                    BackgroundColor = new BaseColor(204, 255, 204), //System.Drawing.KnownColor.LightGreen ,// SetRGBColorFill(144, 238, 144);
                });

                Tablea.AddCell(new PdfPCell(new Phrase("様邸", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1
                });

                Tablea.AddCell(new PdfPCell(new Phrase("坪数", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,

                });
                //Kakoubutusuu   dt.Rows[0]["KakouTubosuu"].ToString()

                Tablea.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["KakouTubosuu"].ToString(), font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    
                    Colspan = 1,
                    BackgroundColor = new BaseColor(204, 255, 204),

                });
                Tablea.AddCell(new PdfPCell(new Phrase("坪 ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    PaddingLeft = -1.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,

                });
                //Kakou Check 
                Tablea.AddCell(new PdfPCell(new Phrase("工事店名", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });

                //KoumuTenName   //new Phrase(dt.Rows[0]["KoumuTenName"].ToString(), font_Class.CreateJapaneseFont(font_folder, 10))
                //msze = File.ReadAllLines("D:\\pdf\\ptk.txt")[1].Split(',');
                Tablea.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["KoumuTenName"].ToString(), font_Class.CreateJapaneseFont(font_folder, 13)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    PaddingBottom = 3f,
                    Colspan = 5,
                    BackgroundColor = new BaseColor(204, 255, 204),
                });

                Tablea.AddCell(new PdfPCell(new Phrase("様", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1
                });

                Tablea.AddCell(new PdfPCell(new Phrase("総本数", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,

                });
                Tablea.AddCell(new PdfPCell(new Phrase("本", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2,

                });
                //Shiten Check 
                Tablea.AddCell(new PdfPCell(new Phrase("支店名", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                //ShitenName
                Tablea.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["ShopName"].ToString(), font_Class.CreateJapaneseFont(font_folder, 13)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2,
                    BackgroundColor = new BaseColor(204, 255, 204),
                });

                Tablea.AddCell(new PdfPCell(new Phrase("支店担当", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1
                });
                //Tantou
                Tablea.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["TentouName"].ToString(), font_Class.CreateJapaneseFont(font_folder, 13)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                    BackgroundColor = new BaseColor(204, 255, 204),

                });
                Tablea.AddCell(new PdfPCell(new Phrase("様", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,

                });
                Tablea.AddCell(new PdfPCell(new Phrase("邸別管理番号", font_Class.CreateJapaneseFont(font_folder, 7)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,

                });
                //BukkenNo
                Tablea.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["BukkenNo"].ToString(), font_Class.CreateJapaneseFont(font_folder, 13)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 3,
                    BackgroundColor = new BaseColor(204, 255, 204),

                });

                //Space br 
                Tablea.AddCell(new PdfPCell(new Phrase("  ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 11,
                });
                //date
                Tablea.AddCell(new PdfPCell(new Phrase("日付", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });

                Tablea.AddCell(new PdfPCell(new Phrase("加工時間", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("加工本数", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("坪数", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });

                Tablea.AddCell(new PdfPCell(new Phrase("特記事項", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 3,
                });

                Tablea.AddCell(new PdfPCell(new Phrase("停止時間", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });

                Tablea.AddCell(new PdfPCell(new Phrase("印", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                #region Content1
                //First  Content1
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });

                //dotted
                Tablea.AddCell(new PdfPCell(new Phrase("--------------------------", font_Class.CreateJapaneseFont(font_folder, 10, 0)))
                {
                    HorizontalAlignment = Element.ALIGN_BOTTOM,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0f,
                    PaddingBottom = -4.0f,
                    PaddingRight = 0f,
                    BorderWidthRight = 0,
                    PaddingLeft = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                   // BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });


                //First Content2
                Tablea.AddCell(new PdfPCell(new Phrase("／", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("≀", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });

                //dotted
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("≀", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });

                //First Content3
                Tablea.AddCell(new PdfPCell(new Phrase("", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    //PaddingBottom = 0f,
                    PaddingTop = 0,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("本", font_Class.CreateJapaneseFont(font_folder, 8)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingTop = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("坪", font_Class.CreateJapaneseFont(font_folder, 8)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingTop = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("--------------------------", font_Class.CreateJapaneseFont(font_folder, 10, 0)))
                {
                    HorizontalAlignment = Element.ALIGN_TOP,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    PaddingTop = -6.2f,
                    BorderWidthTop = 0,
                    PaddingBottom = 0f,
                    PaddingRight = 0f,
                    BorderWidthRight = 0f,
                    BorderWidthBottom = 0,
                    PaddingLeft = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    PaddingTop = 0,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                #endregion
                #region Content2
                //First  Content1
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });

                //dotted
                Tablea.AddCell(new PdfPCell(new Phrase("--------------------------", font_Class.CreateJapaneseFont(font_folder, 10, 0)))
                {
                    HorizontalAlignment = Element.ALIGN_BOTTOM,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0f,
                    PaddingBottom = -4.0f,
                    PaddingRight = 0f,
                    BorderWidthRight = 0,
                    PaddingLeft = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    // BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });


                //First Content2
                Tablea.AddCell(new PdfPCell(new Phrase("／", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("≀", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });

                //dotted
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("≀", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });

                //First Content3
                Tablea.AddCell(new PdfPCell(new Phrase("", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    //PaddingBottom = 0f,
                    PaddingTop = 0,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("本", font_Class.CreateJapaneseFont(font_folder, 8)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingTop = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("坪", font_Class.CreateJapaneseFont(font_folder, 8)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingTop = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("--------------------------", font_Class.CreateJapaneseFont(font_folder, 10, 0)))
                {
                    HorizontalAlignment = Element.ALIGN_TOP,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    PaddingTop = -6.2f,
                    BorderWidthTop = 0,
                    PaddingBottom = 0f,
                    PaddingRight = 0f,
                    BorderWidthRight = 0f,
                    BorderWidthBottom = 0,
                    PaddingLeft = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    PaddingTop = 0,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                #endregion
                #region Content3
                //First  Content1
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });

                //dotted
                Tablea.AddCell(new PdfPCell(new Phrase("--------------------------", font_Class.CreateJapaneseFont(font_folder, 10, 0)))
                {
                    HorizontalAlignment = Element.ALIGN_BOTTOM,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0f,
                    PaddingBottom = -4.0f,
                    PaddingRight = 0f,
                    BorderWidthRight = 0,
                    PaddingLeft = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    // BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });


                //First Content2
                Tablea.AddCell(new PdfPCell(new Phrase("／", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("≀", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });

                //dotted
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("≀", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });

                //First Content3
                Tablea.AddCell(new PdfPCell(new Phrase("", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    //PaddingBottom = 0f,
                    PaddingTop = 0,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("本", font_Class.CreateJapaneseFont(font_folder, 8)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingTop = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("坪", font_Class.CreateJapaneseFont(font_folder, 8)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingTop = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("--------------------------", font_Class.CreateJapaneseFont(font_folder, 10, 0)))
                {
                    HorizontalAlignment = Element.ALIGN_TOP,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    PaddingTop = -6.2f,
                    BorderWidthTop = 0,
                    PaddingBottom = 0f,
                    PaddingRight = 0f,
                    BorderWidthRight = 0f,
                    BorderWidthBottom = 0,
                    PaddingLeft = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    PaddingTop = 0,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                #endregion
                #region Content4
                //First  Content1
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });

                //dotted
                Tablea.AddCell(new PdfPCell(new Phrase("--------------------------", font_Class.CreateJapaneseFont(font_folder, 10, 0)))
                {
                    HorizontalAlignment = Element.ALIGN_BOTTOM,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0f,
                    PaddingBottom = -4.0f,
                    PaddingRight = 0f,
                    BorderWidthRight = 0,
                    PaddingLeft = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    // BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });


                //First Content2
                Tablea.AddCell(new PdfPCell(new Phrase("／", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("≀", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });

                //dotted
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("≀", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });

                //First Content3
                Tablea.AddCell(new PdfPCell(new Phrase("", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    //PaddingBottom = 0f,
                    PaddingTop = 0,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("本", font_Class.CreateJapaneseFont(font_folder, 8)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingTop = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("坪", font_Class.CreateJapaneseFont(font_folder, 8)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingTop = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("--------------------------", font_Class.CreateJapaneseFont(font_folder, 10, 0)))
                {
                    HorizontalAlignment = Element.ALIGN_TOP,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    PaddingTop = -6.2f,
                    BorderWidthTop = 0,
                    PaddingBottom = 0f,
                    PaddingRight = 0f,
                    BorderWidthRight = 0f,
                    BorderWidthBottom = 0,
                    PaddingLeft = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    PaddingTop = 0,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                #endregion
                #region Content5
                //First  Content1
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });

                //dotted
                Tablea.AddCell(new PdfPCell(new Phrase("--------------------------", font_Class.CreateJapaneseFont(font_folder, 10, 0)))
                {
                    HorizontalAlignment = Element.ALIGN_BOTTOM,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0f,
                    PaddingBottom = -4.0f,
                    PaddingRight = 0f,
                    BorderWidthRight = 0,
                    PaddingLeft = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    // BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });


                //First Content2
                Tablea.AddCell(new PdfPCell(new Phrase("／", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("≀", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 1,
                });

                //dotted
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("≀", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });

                //First Content3
                Tablea.AddCell(new PdfPCell(new Phrase("", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    //PaddingBottom = 0f,
                    PaddingTop = 0,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("本", font_Class.CreateJapaneseFont(font_folder, 8)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0f,
                    PaddingTop = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("坪", font_Class.CreateJapaneseFont(font_folder, 8)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingTop = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("--------------------------", font_Class.CreateJapaneseFont(font_folder, 10, 0)))
                {
                    HorizontalAlignment = Element.ALIGN_TOP,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    PaddingTop = -6.2f,
                    BorderWidthTop = 0,
                    PaddingBottom = 0f,
                    PaddingRight = 0f,
                    BorderWidthRight = 0f,
                    BorderWidthBottom = 0,
                    PaddingLeft = 0f,
                    Colspan = 3,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    //PaddingBottom = 0f,
                    PaddingTop = 0,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0,
                    //BorderWidthRight = 0,
                    PaddingBottom = 0f,
                    Colspan = 2,
                });
                #endregion

                #region ContentBase
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    BorderWidthRight = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("累計時間", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthRight = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    BorderWidthRight = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 5,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("累計時間", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    BorderWidthRight = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 18.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthRight = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });


                //second base
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 39.0f,
                    BorderWidthRight = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 39.0f,
                    BorderWidthRight = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 39.0f,
                    BorderWidthRight = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 5,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(":", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 39.0f,
                    BorderWidthRight = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 1,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 39.0f,
                    //BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                #endregion

                #region 2baseSpace
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 5f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 11,
                });

                Tablea.AddCell(new PdfPCell(new Phrase("1H／坪数", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 22.1f,
                    BorderWidthBottom = 0,
                    BorderWidthRight = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 22.1f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 9,
                });

                Tablea.AddCell(new PdfPCell(new Phrase("1本／分", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 22.1f,
                    BorderWidthBottom = 0,
                    BorderWidthRight = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 22.1f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 9,
                });

                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 11,
                });

                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 2.0f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 11,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("最終データ処理日時     ：          月          日          時          分", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 25.5f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 11,
                    BackgroundColor = new BaseColor(204, 255, 204),
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 2.0f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 11,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("[連絡事項]", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    PaddingTop=3f,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 20.0f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    BorderWidthRight = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 9,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("最終チェック", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 20.0f,
                    BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 64.0f,
                    //BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthRight = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 9,
                });
                Tablea.AddCell(new PdfPCell(new Phrase(" ", font_Class.CreateJapaneseFont(font_folder, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 64.0f,
                    //BorderWidthBottom = 0,
                    //BorderWidthTop = 0,
                    //BorderWidthLeft = 0, 
                    //PaddingBottom = 0f,
                    Colspan = 2,
                });
                Tablea.AddCell(new PdfPCell(new Phrase("株式会社　岡本銘木店　プレカット事業部　三田工場", font_Class.CreateJapaneseFont(font_folder, 15)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 45.0f,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    PaddingTop=5.5f,

                    //PaddingBottom = 0f,
                    Colspan = 11,
                });
                #endregion

                //var Coor = File.ReadAllLines("D:\\pdf\\ptk.txt")[0].Split(',');
                PdfContentByte cb = writer.DirectContentUnder;
                cb.SetColorStroke(BaseColor.BLACK);
                //cb.Rectangle(Convert.ToInt32(Coor.First()), Convert.ToInt32(Coor.Last()), 10f, 10f);
                //cb.Stroke();
                //Coor = File.ReadAllLines("D:\\pdf\\ptk.txt")[1].Split(',');
                //cb.Rectangle(Convert.ToInt32(Coor.First()), Convert.ToInt32(Coor.Last()), 10f, 10f);
                //cb.Stroke();
                //Coor = File.ReadAllLines("D:\\pdf\\ptk.txt")[2].Split(',');
                //cb.Rectangle(Convert.ToInt32(Coor.First()), Convert.ToInt32(Coor.Last()), 10f, 10f);
                //cb.Stroke();

                //var Coor1 = File.ReadAllLines("D:\\pdf\\ptk.txt")[3].Split(',');
                PdfContentByte cb1 = writer.DirectContent;
                //cb1.SetColorStroke(CMYKColor.MAGENTA);
                //cb1.MoveTo(Convert.ToInt32(Coor1.First()), Convert.ToInt32(Coor1[1]));
                //cb1.Rectangle(Convert.ToInt32(Coor1.First()), Convert.ToInt32(Coor1[1]), Convert.ToInt32(Coor1[2]), Convert.ToInt32(Coor1[3]));
                //cb1.SetRGBColorFill(144, 238, 144);
                //cb1.SetColorFill(new CMYKColor(0f, 0f, 1f, 0f));
                //cb1.Stroke();
                //cb1.Fill();

                //446.7,324.7,506.5,268
                //    88.55,324.5,152,268
                //197,324.5,402.5,268
                //Coor1 = File.ReadAllLines("D:\\pdf\\ptk.txt")[3].Split(',');
                cb1.MoveTo(Convert.ToDouble(446.7), Convert.ToDouble(324.7));
                cb1.LineTo(Convert.ToDouble(506.5), Convert.ToDouble(268));
                cb1.SetLineWidth(0.2f);
                cb1.Stroke();

             // Coor1 = File.ReadAllLines("D:\\pdf\\ptk.txt")[4].Split(',');
                cb1.MoveTo(Convert.ToDouble(88.55), Convert.ToDouble(324.5));
                cb1.LineTo(Convert.ToDouble(152), Convert.ToDouble(268));
                cb1.SetLineWidth(0.2f);
                cb1.Stroke();

               //Coor1 = File.ReadAllLines("D:\\pdf\\ptk.txt")[5].Split(',');
                cb1.MoveTo(Convert.ToDouble(197), Convert.ToDouble(324.5));
                cb1.LineTo(Convert.ToDouble(402.5), Convert.ToDouble(268));
                cb1.SetLineWidth(0.2f);
                cb1.Stroke();
                #endregion
                doc1.Add(Tablea);
                doc1.Close(); 
            }
            catch(Exception ex)
            {
                File.WriteAllText("C:\\Okamei\\Okamei.log", ex.StackTrace);
            }
        }

        public (int, float) ShrinkValue(string val)
        {
            int LengthShrink = Convert.ToInt32(Encoding.GetEncoding(932).GetByteCount(val).ToString());

            if (LengthShrink > 40)
            {
                return (21, 5f);
            }
            else if (LengthShrink > 30)
            {
                return (27, 7f);
            }
            else if (LengthShrink > 20)
            {
                return (35, 7f);
            }
            else if (LengthShrink > 10)
            {
                return (45, 9f);
            }
            else
            {
                return (49, 9f);
            }
        }
    }
}
