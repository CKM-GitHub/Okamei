using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace OkameiProduction.BL
{
    public class CommonBL
    {
        public IEnumerable<DropDownListItem> GetMultiPorposeDDLItems(EMultiPorpose id, string char4 = "")
        {
            var options = new List<DropDownListItem>();

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = (int)id };
            sqlParams[1] = new SqlParameter("@Char4", SqlDbType.VarChar) { Value = char4 };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("M_MultiPorpose_SelectForDropDownLit", sqlParams);

            foreach (DataRow dr in dt.Rows)
            {
                var option = new DropDownListItem()
                {
                    Value = dr["Value"].ToStringOrEmpty(),
                    DisplayText = dr["DisplayText"].ToStringOrEmpty(),
                    SortNumber = dr["SortBy"].ToInt32(0)
                };
                options.Add(option);
            }

            if (options.Count == 0)
            {
                options.Add(new DropDownListItem());
            }

            return options;
        }

        public IEnumerable<DropDownListItem> GetTokuchuuzaiUmuDDLItems()
        {
            var options = new List<DropDownListItem>();
            options.Add(new DropDownListItem() { Value = "1", DisplayText = "有", SortNumber = 0 });
            options.Add(new DropDownListItem() { Value = "2", DisplayText = "無", SortNumber = 1 });
            options.Add(new DropDownListItem() { Value = "3", DisplayText = "未定", SortNumber = 2 });

            return options;
        }

        public string GetNewBukkenNO(string sitenCD)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@SitenCD", SqlDbType.VarChar) { Value = sitenCD };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("M_MultiPorpose_GetNewBukkenNO", sqlParams);

            if (dt.Rows.Count == 0)
            {
                return "";
            }

            var dr = dt.Rows[0];
            var prefix = dr["Prefix"].ToStringOrEmpty();
            var number = dr["Number"].ToStringOrEmpty();
            var newBukkenNO = prefix + number.PadLeft(8 - prefix.Length, '0');

            return newBukkenNO;
        }







        public bool CheckAndFormatDate(string val, out string errorcd, out string outVal)
        {
            errorcd = "";
            outVal = "";

            if (val == "") return true;

            if (!CheckIsHalfWidth(val, out errorcd))
            {
                return false;

            }

            if (val.Length > 10)
            {
                errorcd = "E103";
                return false;
            }

            if (val.Contains("/"))
            {
                var split = val.Split('/');
                if (split.Length <= 2)
                {
                    //MM/dd -> yyyy/MM/dd
                    val = DateTime.Now.Year.ToString() + "/" + val;
                }
                else if (split.Length == 3)
                {
                    //yy/MM/dd -> yyyy/MM/dd
                    val = DateTime.Now.Year.ToString().Substring(0, 4 - split[0].Length) + val;
                }
            }
            else
            {
                if (val.Length <= 4)
                {
                    //MMdd -> yyyyMMdd
                    val = DateTime.Now.Year.ToString() + val.PadLeft(4, '0');
                }
                else if (val.Length < 8)
                {
                    //yyMMdd -> yyyyMMdd
                    val = DateTime.Now.Year.ToString().Substring(0, 8 - val.Length) + val;
                }
            }

            if (val.ToDateTime() == null)
            {
                errorcd = "E103";
                return false;
            }

            outVal = val.ToDateTime(DateTime.Now).ToString(DateTimeFormat.yyyyMMdd);
            return true;
        }

        public bool CheckCompareDate(string fromDate, string toDate, out string errorcd)
        {
            errorcd = "";

            if (!CheckAndFormatDate(fromDate, out errorcd, out string correctFromDate))
            {
                return false;
            }

            if (!CheckAndFormatDate(toDate, out errorcd, out string correctToDate))
            {
                return false;
            }

            if (correctFromDate.ToDateTime() > correctToDate.ToDateTime())
            {
                errorcd = "E104";
                return false;
            }
            return true;
        }

        public bool CheckByteCount(string val, int maxLength, out string errorcd, out string cutString)
        {
            errorcd = "";

            cutString = val.GetByteString(maxLength);
            if (val != cutString)
            {
                errorcd = "E142"; //入力した値の桁数が正しくありません。
                return false;
            }
            return true;
        }

        public bool CheckIsHalfWidth(string val, out string errorcd)
        {
            errorcd = "";

            Encoding e = Encoding.GetEncoding("Shift_JIS");
            if (e.GetByteCount(val) != val.Length)
            {
                errorcd = "E221"; //入力できない文字が含まれています。
                return false;
            }
            return true;
        }

        public bool CheckIsDoubleByte(string val, out string errorcd)
        {
            errorcd = "";

            Encoding e = Encoding.GetEncoding("Shift_JIS");
            if (e.GetByteCount(val) != (val.Length * 2))
            {
                errorcd = "E221"; //入力できない文字が含まれています。
                return false;
            }
            return true;
        }

        public bool CheckIsNumeric(string val, int integerdigits, int decimaldigits, out string errorcd, out string outVal)
        {
            errorcd = "";
            outVal = "";

            if (!Decimal.TryParse(val.Trim(), out decimal decimalValue))
            {
                errorcd = "E221"; //入力できない文字が含まれています。
                return false;
            }

            //小数以下桁数で切り捨て
            if (decimaldigits > 0)
            {
                decimal power = Math.Pow(10, decimaldigits).ToDecimal(0);
                decimalValue = Math.Truncate(decimalValue * power) / power;
            }

            //min max
            string maxValue = "";
            if (integerdigits == 0)
            {
                maxValue = "0";
            }
            else
            {
                maxValue= new string('9', integerdigits);
            }
            if (decimaldigits > 1)
            {
                maxValue += "." + new string('9', decimaldigits);
            }

            if (maxValue.ToDecimal(0) < decimalValue || (maxValue.ToDecimal(0) * -1) > decimalValue)
            {
                errorcd = "E142";
                return false; //入力した値の桁数が正しくありません。
            }

            //out val
            if (decimaldigits == 0)
            {
                outVal = decimalValue.ToString("#,##0");
            }
            else
            {
                var format = "#,##0." + new String('0', decimaldigits);
                outVal = decimalValue.ToString(format);
            }

            return true;
        }
    }
}
