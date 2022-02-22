using ClosedXML.Excel;
using System;
using System.Text; 
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Path = System.IO.Path;
using System.Web;
using System.Runtime.Serialization;

namespace Models
{
   public class KakouShoudaku 
    {
        public KakouShoudaku()
        {

        }
        public void AgreementFormExport(string SavePath, string IniPath, DataTable  dt)
        {
            var wbook = new XLWorkbook(IniPath);
            var weSheet = wbook.Worksheets.Worksheet("加工承諾書");//加工承諾書 
            weSheet.Cell("A4").Value = dt.Rows[0]["KoumutenName"].ToString() + " " + "御中";
            weSheet.Cell("A6").Value = dt.Rows[0]["BukkenName"].ToString();
            weSheet.Cell("AB4").Value = dt.Rows[0]["ShopName"].ToString();
            weSheet.Cell("AB6").Value = dt.Rows[0]["SaleMan"].ToString();
            weSheet.Cell("Q7").Value = dt.Rows[0]["MailAddress"].ToString();
            weSheet.Cell("K12").Value = Get_MMDD(dt.Rows[0]["FirstDate"] == null ? "" : dt.Rows[0]["FirstDate"].ToString());
            weSheet.Cell("B13").Value = Get_MMDD(dt.Rows[0]["SecondDate"] == null ? "" : dt.Rows[0]["SecondDate"].ToString());


            // var shape = weSheet.AddShape("Description", eShapeStyle.RoundRect);

            wbook.SaveAs(SavePath);
        }
        private string Get_MMDD(string date)
        {//12月15日
            if (date != "")
            {

                var res = "";
                try
                {
                    if (DateTime.TryParse(date, out DateTime result))
                    {
                        var dtMonth = result.ToString("yyyy-MM-dd");
                        res = "'" + dtMonth.Split('-')[1].PadLeft(2, '0') + "月" + dtMonth.ToString().Replace("/", "-").Split('-')[2].PadLeft(2, '0') + "日";
                    }
                    else
                    {
                        res = "'" + date.ToString().Replace("/", "-").Split('-')[0].PadLeft(2, '0') + "月" + date.ToString().Replace("/", "-").Split('-')[1].PadLeft(2, '0') + "日";
                    }
                }
                catch
                {
                    //var log = Logger.GetInstance();
                    //log.Debug(ex.StackTrace.ToString());
                }

                return res;
            }
            return date;

        }
    }
}
