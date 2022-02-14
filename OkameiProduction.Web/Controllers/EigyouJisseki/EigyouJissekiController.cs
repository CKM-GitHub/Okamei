using System;
using System.Text;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;
using System.Data;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace OkameiProduction.Web.Controllers.EigyouJisseki
{
    public class EigyouJissekiController : BaseController
    {

        public ActionResult SetCondition()
        {
            var vm = new EigyouJissekiModel();
            var dtMonth = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);
            ViewBag.ServerDate = dtMonth.Remove(dtMonth.Length - 3).Replace("-","/"); 
            return View(vm);
        }
        public ActionResult DisplayResult()
        {
            var vm = GetFromQueryString<EigyouJissekiModel>();
            var bl = new EigyouJissekiBL();
            var dt = bl.GetDisplayResult(vm);
            //ViewBag.Data = dt;
            try
            {
                GetDataTable(dt, "2022-01");
            }
            catch { }
            return View(vm);
        } 

        private (DateTime, DateTime) GetDatePartition(DateTime date, int count)
        {
            var date1 = new DateTime();
            var date2 = new DateTime();
            if ( count == 1)
            {
                date1 = Convert.ToDateTime(date.Year + "-" + date.Month.ToString().PadLeft(2, '0') + "-" + "21");
                date2 = Convert.ToDateTime(date.Year + "-" + date.Month.ToString().PadLeft(2, '0') + "-" + "25");
            }
            else if (count ==2)
            {
                date1 = Convert.ToDateTime(date.Year + "-" + date.Month.ToString().PadLeft(2, '0') + "-" + "26"); 
                date2 = Convert.ToDateTime(date.Year + "-" + date.Month.ToString().PadLeft(2, '0') + "-" + date.Day.ToString().PadLeft(2, '0')); 
            }
            else if (count == 3)
            {
                date1 = Convert.ToDateTime((date.AddMonths(1)).Year + "-" + date.AddMonths(1).Month.ToString().PadLeft(2, '0') + "-" + "01");
                date2 = Convert.ToDateTime((date.AddMonths(1)).Year + "-" + date.AddMonths(1).Month.ToString().PadLeft(2, '0') + "-" + "05"); 
            }
            else if (count == 4)
            {
                date1 = Convert.ToDateTime((date.AddMonths(1)).Year + "-" + date.AddMonths(1).Month.ToString().PadLeft(2, '0') + "-" + "06");
                date2 = Convert.ToDateTime((date.AddMonths(1)).Year + "-" + date.AddMonths(1).Month.ToString().PadLeft(2, '0') + "-" + "10"); 
            }
            else if (count == 5)
            {
                date1 = Convert.ToDateTime((date.AddMonths(1)).Year + "-" + date.AddMonths(1).Month.ToString().PadLeft(2, '0') + "-" + "11");
                date2 = Convert.ToDateTime((date.AddMonths(1)).Year + "-" + date.AddMonths(1).Month.ToString().PadLeft(2, '0') + "-" + "15");
            }
            else
            {
                date1 = Convert.ToDateTime((date.AddMonths(1)).Year + "-" + date.AddMonths(1).Month.ToString().PadLeft(2, '0') + "-" + "16");
                date2 = Convert.ToDateTime((date.AddMonths(1)).Year + "-" + date.AddMonths(1).Month.ToString().PadLeft(2, '0') + "-" + "20");

            }
            return (date1,date2);
            

        }
        private DataTable GetDataTable  (DataTable dt, string DateMonth)
        {
            DateMonth = DateMonth.Replace("/", "-") + "-01"; 
            var ShopName = ""; 
            var DutierName = "";
            var CountAll = 0;
            decimal AmountAll = 0 ;
            var DtIterate = GetHeader() ;
            var InitialDate =  Convert.ToDateTime(DateMonth).AddMonths(-1);
            InitialDate = Convert.ToDateTime(InitialDate.Year + "-" + InitialDate.Month.ToString().PadLeft(2,'0') + "-" + "21");
            var FinalDate = new DateTime();
            var dtShopDutier = new DataTable();
            dtShopDutier = dt.AsEnumerable() .GroupBy(r => new { Col1 = r["Shop"], Col2 = r["Dutier"] }) .Select(g => g.OrderBy(r => r["mp1Num1"]).First()).CopyToDataTable();
            
            foreach (DataRow dr in dtShopDutier.Rows)
            {
                ShopName = dr["Shop"].ToStringOrNull();
                DutierName = dr["Dutier"].ToStringOrNull();
                var drNew = DtIterate.NewRow();
                drNew["Shop"] = ShopName;
                drNew["Dutier"] = DutierName;
                drNew["mp1Num1"] = dr["mp1Num1"].ToStringOrNull();
                drNew["mp2Num1"] = dr["mp2Num1"].ToStringOrNull();
                
                for (int i = 0; i < 6; i++)
                {
                    var Resultant = GetDatePartition(Convert.ToDateTime(DateMonth).AddMonths(-1), (i + 1));
                    InitialDate = Resultant.Item1;
                    FinalDate = Resultant.Item2;
                    var drow = dt.Select(" Shop = '" + ShopName + "' and Dutier = '" + DutierName + "' and Nouki >= '" + InitialDate + "' and Nouki <= '" + FinalDate + "'");
                  

                    if (i == 0)
                    {
                        if (drow.Count() != 0)
                        {
                           var ArrayVal= GetInsertData(drow);
                            drNew["Count21"] = ArrayVal.Item1;
                            drNew["Amount21"] = ArrayVal.Item2;
                            CountAll += ArrayVal.Item1;
                            AmountAll += ArrayVal.Item2;
                        }
                    }
                    else if (i ==1)
                    {
                        if (drow.Count() != 0)
                        {
                            var ArrayVal = GetInsertData(drow);
                            drNew["Count26"] = ArrayVal.Item1;
                            drNew["Amount26"] = ArrayVal.Item2;
                            CountAll += ArrayVal.Item1;
                            AmountAll += ArrayVal.Item2;
                        }
                    }
                    else if (i == 2)
                    {
                        if (drow.Count() != 0)
                        {
                            var ArrayVal = GetInsertData(drow);
                            drNew["Count01"] = ArrayVal.Item1;
                            drNew["Amount01"] = ArrayVal.Item2;
                            CountAll += ArrayVal.Item1;
                            AmountAll += ArrayVal.Item2;
                        }
                    }
                    else if (i == 3)
                    {
                        if (drow.Count() != 0)
                        {
                            var ArrayVal = GetInsertData(drow);
                            drNew["Count06"] = ArrayVal.Item1;
                            drNew["Amount06"] = ArrayVal.Item2;
                            CountAll += ArrayVal.Item1;
                            AmountAll += ArrayVal.Item2;
                        }
                    }
                    else if (i == 4)
                    {
                        if (drow.Count() != 0)
                        {
                            var ArrayVal = GetInsertData(drow);
                            drNew["Count11"] = ArrayVal.Item1;
                            drNew["Amount11"] = ArrayVal.Item2;
                            CountAll += ArrayVal.Item1;
                            AmountAll += ArrayVal.Item2;
                        }
                    }
                    else
                    {
                        if (drow.Count() != 0)
                        {
                            var ArrayVal = GetInsertData(drow);
                            drNew["Count16"] = ArrayVal.Item1;
                            drNew["Amount16"] = ArrayVal.Item2;
                            CountAll += ArrayVal.Item1;
                            AmountAll += ArrayVal.Item2;
                        }
                    }

                }
                drNew["CountAll"] = CountAll;
                drNew["AmountAll"] = AmountAll;
                DtIterate.Rows.Add(drNew);
                CountAll = 0;
                AmountAll = 0;
            } 
            return DtIterate; 
        }

        private (int, decimal) GetInsertData(DataRow[] drow)
        {
             
                var query = (from row in drow.CopyToDataTable().AsEnumerable()
                             group row by new
                             {
                                 TantouEigyouCD = row.Field<string>("TantouEigyouCD"),
                                 TantouSitenCD = row.Field<string>("TantouSitenCD"),
                             } into grp
                             select new
                             {
                                 TantouEigyouCD = grp.Key.TantouEigyouCD,
                                 TantouSitenCD = grp.Key.TantouSitenCD,
                                 KakoutuboSuu = grp.Sum(r => r.Field<decimal>("KakoutuboSuu")),
                                 Count = grp.Count(),
                                 //mp1Num1 = grp.Max(r => r.Field<string>("mp1Num1")),
                                 //mp2Num1 = grp.Max(r => r.Field<string>("mp2Num1")),
                             }).ToList();
            return (query[0].Count,query[0].KakoutuboSuu); 
        }
        private DataTable GetHeader()
        {
            DataTable dt = new DataTable();
            string[] Header = new string[] { "Shop", "Dutier", "Count21", "Amount21", "Count26", "Amount26",   "Count01", "Amount01", "Count06", "Amount06", "Count11", "Amount11" , "Count16", "Amount16" , "CountAll", "AmountAll" ,"mp1Num1", "mp2Num1"};
            foreach(var i in Header)
            {
                dt.Columns.Add(i,typeof(string));
            }
            return dt;
        }



    }
}
