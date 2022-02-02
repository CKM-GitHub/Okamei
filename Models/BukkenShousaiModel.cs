using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BukkenShousaiModel : BaseModel
    {
        //Page 1.
        public string BukkenNO { get; set; }
        public string SitenCD { get; set; }
        public string BukkenName { get; set; }
        public string Address { get; set; }
        public string KoumutenCD { get; set; }
        public decimal? KakouTubosuu { get; set; }
        public int NoukiMiteiCheck { get; set; }
        public string Nouki { get; set; }
        public string UnsougaishaSoukoiriDate { get; set; }
        public string KubunCD { get; set; }
        public string TantouEigyouCD { get; set; }
        public string PCSupportCD { get; set; }
        public string TantouCadCD { get; set; }
        public string NyuuryokusakiCD { get; set; }
        public int TokuchuuzaiUmu { get; set; }
        public string ZairyouNouki { get; set; }
        public string TokuchuuzaiComment { get; set; }
        //Page 2.
        public string JuchuuDate { get; set; }
        public string HusezuTeishutuDate { get; set; }
        public string KakouShouninDate { get; set; }
        public string KidasiDate { get; set; }
        public string KakouSijishoHakkouDate { get; set; }
        public string kannnouDate { get; set; }
        public string CancelDate { get; set; }
        public string KakouNissuu { get; set; }
        public string UpdateDatetime { get; set; }
        public string KanamonoCD { get; set; }
        public int OukazaiExist { get; set; }
        public int OukazaiCheck { get; set; }
        public int KabeExist { get; set; }
        public int KabeCheck { get; set; }
        public int HasirazaiExist { get; set; }
        public int HasirazaiCheck { get; set; }
        public int HiuchiExist { get; set; }
        public int HiuchiCheck { get; set; }
        public int HagarazaiExist { get; set; }
        public int HagarazaiCheck { get; set; }
        public decimal? HagarazaiNumber { get; set; }
        public int YukaExist { get; set; }
        public int YukaCheck { get; set; }
        public string YukaGoubanCD { get; set; }
        public decimal? YukaNumber { get; set; }
        public int NojiExist { get; set; }


        public string YukazaiCD { get; set; }
        public string NojizaiCD { get; set; }

        public IEnumerable<DropDownListItem> SitenSelectList { get; set; }
        public IEnumerable<DropDownListItem> EigyouStaffSelectList { get; set; }
        public IEnumerable<DropDownListItem> PCSupportSelectList { get; set; }
        public IEnumerable<DropDownListItem> CADStaffSelectList { get; set; }
        public IEnumerable<DropDownListItem> KoumutenSelectList { get; set; }
        public IEnumerable<DropDownListItem> NyuuryokusakiSelectList { get; set; }
        public IEnumerable<DropDownListItem> TokuchuuzaiUmuSelectList { get; set; }
        public IEnumerable<DropDownListItem> KubunSelectList { get; set; }
        public IEnumerable<DropDownListItem> KanamonoSelectList { get; set; }
        public IEnumerable<DropDownListItem> GoubanSelectList { get; set; }
    }
}
