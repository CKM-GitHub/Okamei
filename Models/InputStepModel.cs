using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class InputStepModel : BaseModel
    {
        public string TantouSitenCD { get; set; }
        public int? JuchuuOption { get; set; }
        public int? BukkenSiteiOption { get; set; }
        public string NoukiStart { get; set; }
        public string NoukiEnd { get; set; }
        public string TantouEigyouCD { get; set; }
        public string TantouCadCD { get; set; }
        public string TantouPcCD { get; set; }
        public int Dankai1 { get; set; }
        public int Dankai2 { get; set; }
        public IEnumerable<DropDownListItem> TantouSitenDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouEigyouDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouPcDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouCadDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> Dankai1DropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> Dankai2DropDownListItems { get; set; }
    }
}
