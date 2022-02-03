using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ValidateModel
    {
        public bool IsDateType { get; set; }
        public bool IsCompareDate { get; set; }
        public bool IsHalfWidth { get; set; }
        public bool IsDoubleByte { get; set; }
        public bool IsDoubleByteOnly { get; set; }
        public bool IsNumeric { get; set; }
        public int Integerdigits { get; set; }
        public int Decimaldigits { get; set; }
        public string InputValue1 { get; set; }
        public string ComparisonValue { get; set; }
        public string MaxLength { get; set; }

    }
}
