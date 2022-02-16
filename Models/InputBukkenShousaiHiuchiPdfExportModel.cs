using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InputBukkenShousaiHiuchiPdfExportModel : BaseModel
    {
        public string BukkenNO { get; set; }
        public string BukkenName { get; set; }
        public string KoumutenName { get; set; }
        public string FileName { get; set; }


        public string SouName { get; set; }
        public string Zairyou1 { get; set; }
        public string Toukyuu1 { get; set; }
        public string Honsuu1 { get; set; }
        public string Zairyou2 { get; set; }
        public string Toukyuu2 { get; set; }
        public string Honsuu2 { get; set; }
        public string Zairyou3 { get; set; }
        public string Toukyuu3 { get; set; }
        public string Honsuu3 { get; set; }
    }
}
