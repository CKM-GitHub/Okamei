using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class InputStepModel : BaseModel
    {
      
        public string TantouCadCD { get; set; }
       
       
        public IEnumerable<DropDownListItem> TantouCadDropDownListItems { get; set; }
    }
}
