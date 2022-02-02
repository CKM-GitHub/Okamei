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
        public int OukazaiExistence { get; set; }
        public int OukazaiCheck { get; set; }
        public int KabeExistence { get; set; }
        public int KabeCheck { get; set; }
        public int HasirazaiExistence { get; set; }
        public int HasirazaiCheck { get; set; }
        public int HiuchiExistence { get; set; }
        public int HiuchiCheck { get; set; }
        public int HagarazaiExistence { get; set; }
        public int HagarazaiCheck { get; set; }
        public decimal? HagarazaiNumber { get; set; }
        public int YukaExistence { get; set; }
        public int YukaCheck { get; set; }
        public string YukaGoubanCD { get; set; }
        public decimal? YukaNumber { get; set; }
        public int NojiExistence { get; set; }
        public int NojiCheck { get; set; }
        public string NojiGoubanCD { get; set; }
        public decimal? NojiNumber { get; set; }
        public int HundeggerExistence { get; set; }
        public int HundeggerCheck { get; set; }
        public decimal? TekakouTime { get; set; }
        public decimal? HundeggerTime { get; set; }
        //Page 3.
        //public int UpFileOption { get; set; }
        //public int DownFileOption { get; set; }
        //Page 4.
        public string Comment { get; set; }
        
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
