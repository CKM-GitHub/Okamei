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
        public IEnumerable<DropDownListItem> TantouSitenDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouEigyouDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouPcDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouCadDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> KubunDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TokuchuuzaiUmuDropDownListItems { get; set; }
    }
}
