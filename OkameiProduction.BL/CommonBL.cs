using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Models;

namespace OkameiProduction.BL
{
    public class CommonBL
    {
        private static Dictionary<char, char> strConvDictionary = new Dictionary<char, char>() {
            {'０','0'},{'１','1'},{'２','2'},{'３','3'},
            {'４','4'},{'５','5'},{'６','6'},{'７','7'},
            {'８','8'},{'９','9'},{ '．','.' },{ '／','/'}
        };

        //M_MultiPorpose ---------->

        public DataTable GetMultiPorposeByIDChar2(EMultiPorpose id, string char2)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = (int)id };
            sqlParams[1] = new SqlParameter("@Char2", SqlDbType.VarChar) { Value = char2 };

            DBAccess db = new DBAccess();
            return db.SelectDatatable("M_MultiPorpose_SelectByIDChar2", sqlParams);
        }

        public IEnumerable<DropDownListItem> GetMultiPorposeDropDownListItems(EMultiPorpose id, string char4 = "")
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

        public IEnumerable<DropDownListItem> GetTokuchuuzaiUmuDropDownListItems()
        {
            var options = new List<DropDownListItem>();
            options.Add(new DropDownListItem() { Value = "1", DisplayText = "有", SortNumber = 0 });
            options.Add(new DropDownListItem() { Value = "2", DisplayText = "無", SortNumber = 1 });
            options.Add(new DropDownListItem() { Value = "3", DisplayText = "未定", SortNumber = 2 });

            return options;
        }

        public IEnumerable<DropDownListItem> GetWithOrWithoutDropDownListItems()
        {
            var options = new List<DropDownListItem>();
            options.Add(new DropDownListItem() { Value = "1", DisplayText = "有", SortNumber = 0 });
            options.Add(new DropDownListItem() { Value = "2", DisplayText = "無", SortNumber = 1 });

            return options;
        }
        public IEnumerable<DropDownListItem> GetDankai1DropDownListItems()
        {
            var options = new List<DropDownListItem>();
            options.Add(new DropDownListItem() { Value = "1", DisplayText = "未", SortNumber = 0 });
            options.Add(new DropDownListItem() { Value = "2", DisplayText = "待", SortNumber = 1 });
            options.Add(new DropDownListItem() { Value = "3", DisplayText = "指", SortNumber = 2 });

            return options;
        }
        public IEnumerable<DropDownListItem> GetDankai2DropDownListItems()
        {
            var options = new List<DropDownListItem>();
            options.Add(new DropDownListItem() { Value = "1", DisplayText = "未", SortNumber = 0 });
            options.Add(new DropDownListItem() { Value = "2", DisplayText = "待", SortNumber = 1 });
            options.Add(new DropDownListItem() { Value = "3", DisplayText = "指", SortNumber = 2 });

            return options;
        }

        //M_MultiPorpose <----------





        //M_Control ---------->

        public DataTable GetMControl()
        {
            DBAccess db = new DBAccess();
            return db.SelectDatatable("M_Control_Select", null);
        }

        //M_Control <----------





        //Validation ---------->

        public bool CheckAndFormatYMDate(string inputText, out string errorcd, out string outVal)
        {
            errorcd = "";
            outVal = "";

            if (string.IsNullOrEmpty(inputText)) return true;

            if (!CheckIsHalfWidth(inputText, out errorcd, out inputText))
            {
                return false; 
            }

            if (inputText.Length > 7)
            {
                errorcd = "E103";
                return false;
            }

            if (inputText.Contains("/"))
            {
                var split = inputText.Split('/');
                if (split.Length == 2)
                {
                    //yyyyMM -> yyyy/MM/dd
                    inputText = split[0] + "/" + split[1]+ "/" + "01";
                }
              
            }
            else if (inputText.Contains("-"))
            {
                var split = inputText.Split('-');
                if (split.Length == 2)
                {
                    //yyyyMM -> yyyy/MM/dd
                    inputText = split[0] + "/" + split[1] + "/" + "01";
                }
            }
            else
            {
                if (inputText.Length == 6)
                {
                    //yyyyMM -> yyyyMMdd
                    inputText = inputText.ToString().Substring(0, 4) + "/"+ inputText.ToString().Substring(6-2)+"/"+"01";
                }
                else if (inputText.Length ==4)
                {
                    //yyyy -> yyyyMMdd
                    inputText = inputText.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2,'0') + "/" + "01";
                }
                else if (inputText.Length ==2)
                {
                    //mm -> yyyyMMdd
                    inputText = DateTime.Now.Year.ToString().PadLeft(4, '0') + "/" + inputText.ToString() + "/" + "01";
                }
                else if (inputText.Length ==1)
                {
                    //m -> yyyyMMdd
                    inputText = DateTime.Now.Year.ToString().PadLeft(4, '0') + "/" + inputText.ToString().PadLeft(2,'0') + "/" + "01";
                }
            }

            if (inputText.ToDateTime() == null)
            {
                errorcd = "E103";
                return false;
            }

            outVal = inputText.ToDateTime(DateTime.Now).ToString(DateTimeFormat.yyyyMMdd);
            outVal = outVal.Substring(0, 7).Replace("-","/");
            return true;
        }


        public bool CheckAndFormatDate(string inputText, out string errorcd, out string outVal)
        {
            errorcd = "";
            outVal = "";

            if (string.IsNullOrEmpty(inputText)) return true;

            if (!CheckIsHalfWidth(inputText, out errorcd, out inputText))
            {
                return false;

            }

            if (inputText.Length > 10)
            {
                errorcd = "E103";
                return false;
            }

            if (inputText.Contains("/"))
            {
                var split = inputText.Split('/');
                if (split.Length <= 2)
                {
                    //MM/dd -> yyyy/MM/dd
                    inputText = DateTime.Now.Year.ToString() + "/" + inputText;
                }
                else if (split.Length == 3)
                {
                    //yy/MM/dd -> yyyy/MM/dd
                    inputText = DateTime.Now.Year.ToString().Substring(0, 4 - split[0].Length) + inputText;
                }
            }
            else
            {
                if (inputText.Length <= 4)
                {
                    //MMdd -> yyyyMMdd
                    inputText = DateTime.Now.Year.ToString() + inputText.PadLeft(4, '0');
                }
                else if (inputText.Length < 8)
                {
                    //yyMMdd -> yyyyMMdd
                    inputText = DateTime.Now.Year.ToString().Substring(0, 8 - inputText.Length) + inputText;
                }
            }

            if (inputText.ToDateTime() == null)
            {
                errorcd = "E103";
                return false;
            }

            outVal = inputText.ToDateTime(DateTime.Now).ToString(DateTimeFormat.yyyyMMdd);
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

        public bool CheckByteCount(string inputText, int maxLength, out string errorcd, out string cutString)
        {
            errorcd = "";

            cutString = inputText.GetByteString(maxLength);
            if (inputText != cutString)
            {
                errorcd = "E142"; //入力した値の桁数が正しくありません。
                return false;
            }
            return true;
        }

        public bool CheckIsHalfWidth(string inputText, out string errorcd, out string outVal)
        {
            errorcd = "";
            outVal = "";
            inputText = ConvertDoubleByteToSingleByte(inputText);

            Encoding e = Encoding.GetEncoding("Shift_JIS");
            if (e.GetByteCount(inputText) != inputText.Length)
            {
                errorcd = "E221"; //入力できない文字が含まれています。
                return false;
            }

            outVal = inputText;
            return true;
        }

        public bool CheckIsDoubleByte(string inputText, out string errorcd)
        {
            errorcd = "";

            Encoding e = Encoding.GetEncoding("Shift_JIS");
            if (e.GetByteCount(inputText) != (inputText.Length * 2))
            {
                errorcd = "E221"; //入力できない文字が含まれています。
                return false;
            }
            return true;
        }

        public bool CheckIsNumeric(string inputText, int integerdigits, int decimaldigits, out string errorcd, out string outVal)
        {
            errorcd = "";
            outVal = "";
            inputText = ConvertDoubleByteToSingleByte(inputText);

            if (!Decimal.TryParse(inputText.Trim(), out decimal decimalValue))
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
                outVal = decimalValue.ToString("###0");
            }
            else
            {
                var format = "###0." + new String('0', decimaldigits);
                outVal = decimalValue.ToString(format);
            }

            return true;
        }

        public string ConvertDoubleByteToSingleByte(string text)
        {
            var replaced = new String(
              text.Select(
                n => (strConvDictionary.ContainsKey(n) ? strConvDictionary[n] : n)
                ).ToArray()
              );

            return replaced;
        }

        //Validation <----------
    }
}
