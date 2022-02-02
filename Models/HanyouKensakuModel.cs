using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class HanyouKensakuModel : BaseModel
    {
        public string GeneralPurposeCD { get; set; }
        public IEnumerable<DropDownListItem> GeneralSelectList { get; set; }
    }
}
