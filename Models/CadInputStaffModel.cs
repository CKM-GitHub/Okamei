using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class CadInputStaffModel
    {
        //TantouCadCD
        public string TantouCadCD { get; set; }

        public string TantouSitenCD { get; set; }
        public string TantouCadCD1 { get; set; }
        public string TantouCadCD2{ get; set; }
        public string TantouCadCD3{ get; set; }
        public string TantouCadCD4 { get; set; }
        public string TantouCadCD5 { get; set; }
        public string TantouCadCD6 { get; set; }
        public string TantouCadCD7 { get; set; }
        public string TantouCadCD8 { get; set; }
        public string TantouCadCD9 { get; set; }
        public string TantouCadCD10 { get; set; }  
        public IEnumerable<DropDownListItem> TantouSitenDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouEigyouDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouPcDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouCadDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> KubunDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TokuchuuzaiUmuDropDownListItems { get; set; }  

        
    }
}
