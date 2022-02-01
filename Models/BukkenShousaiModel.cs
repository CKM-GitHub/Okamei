using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BukkenShousaiModel : BaseModel
    {
        public string BukkenNO { get; set; }
        public string SitenCD { get; set; }
        public string TantouEigyouCD { get; set; }
        public string BukkenName { get; set; }
        public string PCSupportCD { get; set; }
        public string Address { get; set; }
        public string TantouCadCD { get; set; }
        public string KoumutenCD { get; set; }
        public string NyuuryokusakiCD { get; set; }
        public decimal? KakouTubosuu { get; set; }
        public int TokuchuuzaiUmu { get; set; }
        public int NoukiMiteiCheck { get; set; }
        public string Nouki { get; set; }
        public string ZairyouNouki { get; set; }
        public string UnsouSoukoiribi { get; set; }
        public string TokuchuuzaiComment { get; set; }
        public string KubunCD { get; set; }
        public IEnumerable<DropDownListItem> SitenSelectList { get; set; }
        public IEnumerable<DropDownListItem> EigyouStaffSelectList { get; set; }
        public IEnumerable<DropDownListItem> PCSupportSelectList { get; set; }
        public IEnumerable<DropDownListItem> CADStaffSelectList { get; set; }
        public IEnumerable<DropDownListItem> KoumutenSelectList { get; set; }
        public IEnumerable<DropDownListItem> NyuuryokusakiSelectList { get; set; }
        public IEnumerable<DropDownListItem> TokuchuuzaiUmuSelectList { get; set; }
        public IEnumerable<DropDownListItem> KubunSelectList { get; set; }
    }
}
