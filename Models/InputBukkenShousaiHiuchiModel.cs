using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InputBukkenShousaiHiuchiModel : BaseModel
    {
        public string BukkenNO { get; set; }
        public string BukkenName { get; set; }
        public string HiddenUpdateDateTime { get; set; }


        public string Sou1 { get; set; }
        public string Sou1KakouDateTime { get; set; }
        public string Zairyou11 { get; set; }
        public string Toukyuu11 { get; set; }
        public string Honsuu11 { get; set; }
        public byte Sou1Sumi { get; set; }
        public string Zairyou12 { get; set; }
        public string Toukyuu12 { get; set; }
        public string Honsuu12 { get; set; }
        public string Zairyou13 { get; set; }
        public string Toukyuu13 { get; set; }
        public string Honsuu13 { get; set; }


        public string Sou2 { get; set; }
        public string Sou2KakouDateTime { get; set; }
        public string Zairyou21 { get; set; }
        public string Toukyuu21 { get; set; }
        public string Honsuu21 { get; set; }
        public byte Sou2Sumi { get; set; }
        public string Zairyou22 { get; set; }
        public string Toukyuu22 { get; set; }
        public string Honsuu22 { get; set; }
        public string Zairyou23 { get; set; }
        public string Toukyuu23 { get; set; }
        public string Honsuu23 { get; set; }


        public string Sou3 { get; set; }
        public string Sou3KakouDateTime { get; set; }
        public string Zairyou31 { get; set; }
        public string Toukyuu31 { get; set; }
        public string Honsuu31 { get; set; }
        public byte Sou3Sumi { get; set; }
        public string Zairyou32 { get; set; }
        public string Toukyuu32 { get; set; }
        public string Honsuu32 { get; set; }
        public string Zairyou33 { get; set; }
        public string Toukyuu33 { get; set; }
        public string Honsuu33 { get; set; }



        public string Sou4 { get; set; }
        public string Sou4KakouDateTime { get; set; }
        public string Zairyou41 { get; set; }
        public string Toukyuu41 { get; set; }
        public string Honsuu41 { get; set; }
        public byte Sou4Sumi { get; set; }
        public string Zairyou42 { get; set; }
        public string Toukyuu42 { get; set; }
        public string Honsuu42 { get; set; }
        public string Zairyou43 { get; set; }
        public string Toukyuu43 { get; set; }
        public string Honsuu43 { get; set; }


        public IEnumerable<DropDownListItem> SouDropDownListItems { get; set; }
    }
}
