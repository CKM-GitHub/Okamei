using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InputBukkenShousaiBukkenFileModel : BaseModel
    {
        public string BukkenNO { get; set; }
        public byte BukkenFileShurui { get; set; }
        public string BukkenFileName { get; set; }
        public string HiddenUpdateDatetime { get; set; }
    }
}
