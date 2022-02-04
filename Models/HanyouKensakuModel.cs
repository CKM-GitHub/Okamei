using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class HanyouKensakuModel : BaseModel
    {
        public string ID { get; set; }
        public IEnumerable<DropDownListItem> IDSelectList { get; set; }
    }
}
