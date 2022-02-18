using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class KadouKeikakuModel : BaseModel
    {
        public string TantouSitenCD { get; set; }
        public int? BukkenSiteiOption { get; set; }
        public string NoukiStart { get; set; }
        public string NoukiEnd { get; set; }
        public string TantouEigyouCD { get; set; }
        public string TantouCadCD { get; set; }
        public string TantouPcCD { get; set; }
        public int OukazaiSumi { get; set; }
        public int HasirazaiSumi { get; set; }
        public int HagarazaiSumi { get; set; }
        public int YukaSumi { get; set; }
        public int NoziSumi { get; set; }
        public int KabeSumi { get; set; }
        public int HiuchiSumi { get; set; }
        public int TekakouSumi { get; set; }
        public int HundeggerSumi { get; set; }
        public IEnumerable<DropDownListItem> TantouSitenDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouEigyouDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouPcDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouCadDropDownListItems { get; set; }
    }
}
