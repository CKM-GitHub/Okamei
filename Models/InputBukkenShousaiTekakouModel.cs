using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InputBukkenShousaiTekakouModel : BaseModel
    {
        public string BukkenNO { get; set; }
        public string BukkenName { get; set; }
        public string HiddenUpdateDateTime { get; set; }

        public string TeKakou1Honsuu { get; set; }
        public string TeKakou2Honsuu { get; set; }
        public string TeKakou3Honsuu { get; set; }
        public string TeKakou4Honsuu { get; set; }
        public string TeKakou5Honsuu { get; set; }
        public string TeKakou6Honsuu { get; set; }
        public string TeKakou7Honsuu { get; set; }
        public string TeKakou8Honsuu { get; set; }
    }
}
