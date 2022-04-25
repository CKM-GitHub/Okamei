using System;
using System.Text;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;
using System.Data;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace OkameiProduction.Web.Controllers.CadInputStaff
{
    public class CadInputStaffController : BaseController
    {
        // GET: CadInputStaff
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SetCondition()
        {
            var vm = new CadInputStaffModel();
            SetDropDownListItems(vm);
            var dtMonth = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);
            ViewBag.ServerDate = dtMonth;
            return View(vm);
        }
        private void SetDropDownListItems(CadInputStaffModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.TantouCadDropDownListItems = dl.GetMultiPorposeDropDownListItems(EMultiPorpose.TantouCad);
        }
        public ActionResult DisplayResult()
        {

            var d= System.Text.RegularExpressions.Regex.Split("1212##1233", @"\#\#");
            var vm = GetFromQueryString<CadInputStaffModel>();
            var bl = new CadInputStaffBL();
            var dt = bl.GetDisplayResult(vm);
            var dtResult = GetPrepareData(dt);
            //var dataView = new DataView(dtResult); 
            //    dataView.Sort = "SijiKikitu ASC,Num1 ASC,BukkenNo ASC ";
            //var result= dataView.ToTable();
            var query = from r in dtResult.AsEnumerable()
                        orderby r["SijiKikitu"].ToStringOrNull() ?? "99/99", r["Num1"].ToInt32(0), r.Field<string>("BukkenNo")
                        select r;
            var result = query.CopyToDataTable();
            result.Columns.Remove("Num1");
            //指示期限
            result.Columns["SijiKikitu"].ColumnName = "指示期限";
            ViewBag.Data = result;

            return View(vm);
        }
        private DataTable GetPrepareData(DataTable dt)
        {
            var samaNull = 0;
            foreach (DataRow dr in dt.Rows)
            {
                
                dr["SijiKikitu"] = (dr["SijiKikitu"].ToStringOrNull() == null) ? "" : dr["SijiKikitu"].ToString();
                if (dr["TantouName"].ToStringOrNull() == null)
                    samaNull += 1;
                dr["TantouName"] = (dr["TantouName"].ToStringOrNull() == null) ? ("SamaNull" + samaNull ): dr["TantouName"].ToString();
            }

            var dtHeader = new DataTable();
            var dtHeaderRow = dt.AsEnumerable().GroupBy(r => new { Col1 = r["TantouName"] }).Select(g => g.OrderBy(r => r["SijiKikitu"]).First());
            if (dtHeaderRow.Count() != 0 && dtHeaderRow != null)
                dtHeader = dtHeaderRow.CopyToDataTable();
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("SijiKikitu", typeof(string));
            dtResult.Columns.Add("BukkenNo");
            dtResult.Columns.Add("Num1");

            foreach (DataRow dr in dtHeader.Rows)
            {
                dtResult.Columns.Add(dr["TantouName"].ToString(), typeof(string));
            }

            dtHeaderRow = dt.AsEnumerable().GroupBy(r => new { Col1 = r["SijiKikitu"] }).Select(g => g.OrderBy(r => r["SijiKikitu"]).First());

            if (dtHeaderRow != null && dtHeaderRow.Count() != 0)
            {
                dtHeader = dtHeaderRow.CopyToDataTable();

                foreach (DataRow dr in dtHeader.Rows)
                {
                    DataRow drResultNew = dtResult.NewRow();
                    var ShijiDate = dr["SijiKikitu"].ToString();
                    drResultNew["SijiKikitu"] = ShijiDate;
                    drResultNew["Num1"] = dr["Num1"].ToString();
                    drResultNew["BukkenNo"] = dr["BukkenNo"].ToString();
                    var tempDt = dt.Select(" SijiKikitu = '" + ShijiDate + "' ").CopyToDataTable();
                    foreach (DataRow drtemp in tempDt.Rows)
                    {
                        var TantouTemp = drtemp["TantouName"].ToString();
                        var BukkenName = drtemp["BukkenNo"].ToString()+"^^^"+ drtemp["BukkenName"].ToString();
                        drResultNew[TantouTemp] += BukkenName + "###";
                    }
                    dtResult.Rows.Add(drResultNew);
                }
            
            }
            return dtResult;
        }
    
    }
}