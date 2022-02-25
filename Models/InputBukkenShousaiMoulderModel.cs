using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InputBukkenShousaiMoulderRow
    {
        public string BukkenMoulderRows { get; set; }
        public string MoulderHinmoku { get; set; }
        public string MoulderZairyou { get; set; }
        public string MoulderHaba { get; set; }
        public string MoulderNari { get; set; }
        public string MoulderNagasa { get; set; }
        public string MoulderHonsuu { get; set; }
        public string MoulderKakouDateTime { get; set; }
        public byte MoulderSumi { get; set; }
    }

    public class InputBukkenShousaiMoulderModel : BaseModel
    {
        public string BukkenNO { get; set; }
        public string BukkenName { get; set; }
        public string HiddenUpdateDateTime { get; set; }
        public List<InputBukkenShousaiMoulderRow> Records { get; set; }
        public string RecordsJson { get; set; }

        public IEnumerable<DropDownListItem> HinmokuDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> ZairyouDropDownListItems { get; set; }        
    }
}
