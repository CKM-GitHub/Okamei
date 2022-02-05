using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class BukkenItiranModel : BaseModel
    {
        public string TantouSitenCD { get; set; }
        public int? JuchuuOption  { get; set; }
        public int? BukkenSiteiOption { get; set; }
        public string NoukiStart { get; set; }
        public string NoukiEnd { get; set; }
        public string TantouEigyouCD { get; set; }
        public string TantouCadCD { get; set; }
        public string TantouPcCD { get; set; }
        public int? SortOption { get; set; }
        public string BukkenNO { get; set; }
        public string KubunCD { get; set; }
        public string BukkenName { get; set; }
        public string KoumutenName { get; set; }
        public int TokuchuuzaiUmu { get; set; }
        public IEnumerable<DropDownListItem> TantouSitenSelectList { get; set; }
        public IEnumerable<DropDownListItem> TantouEigyouSelectList { get; set; }
        public IEnumerable<DropDownListItem> TantouPcSelectList { get; set; }
        public IEnumerable<DropDownListItem> TantouCadSelectList { get; set; }
        public IEnumerable<DropDownListItem> KubunSelectList { get; set; }
        public IEnumerable<DropDownListItem> TokuchuuzaiUmuSelectList { get; set; }
    }
}
