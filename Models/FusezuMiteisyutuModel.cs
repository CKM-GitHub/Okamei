using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class FusezuMiteisyutuModel : BaseModel
    {
      
        public string TantouCadCD { get; set; }
       
       
        public IEnumerable<DropDownListItem> CADStaffSelectList { get; set; }
    }
}
