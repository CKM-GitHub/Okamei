using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class MoulderItiranModel : BaseModel
    {
        public string SitenCD { get; set; }
        public string NoukiStart { get; set; }
        public string NoukiEnd { get; set; }
        public IEnumerable<DropDownListItem> SitenSelectList { get; set; }
    }
}
