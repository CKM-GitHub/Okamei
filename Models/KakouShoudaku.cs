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
                        res = "'" + Convert.ToInt32(dtMonth.Split('-')[1]).ToString() + "月" + Convert.ToInt32(dtMonth.ToString().Replace("/", "-").Split('-')[2]).ToString() + "日";
                    }
                    else
                    {
                        res = "'" + Convert.ToInt32(date.ToString().Replace("/", "-").Split('-')[0]).ToString() + "月" + Convert.ToInt32(date.ToString().Replace("/", "-").Split('-')[1]).ToString() + "日";
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
