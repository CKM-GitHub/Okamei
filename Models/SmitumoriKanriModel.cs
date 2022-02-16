using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class SmitumoriKanriModel : BaseModel
    {
        public string sagyou_sDate { get; set; }
        public string sagyou_eDate { get; set; }
        public IEnumerable<DropDownListItem> TantouSitenDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouEigyouDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouPcDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouCadDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> KubunDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TokuchuuzaiUmuDropDownListItems { get; set; }
    }
}
