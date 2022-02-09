using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InputBukkenShousaiBukkenCommentModel : BaseModel
    {
        public string BukkenNO { get; set; }
        public int BukkenCommentRows { get; set; }
        public string BukkenComment { get; set; }
        public string HiddenUpdateDatetime { get; set; }
    }
}
