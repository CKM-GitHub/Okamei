using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InputBukkenShousaiModel : BaseModel
    {
        //Page 1.
        public string BukkenNO { get; set; }
        public string TantouSitenCD { get; set; }
        public string BukkenName { get; set; }
        public string Juusho { get; set; }
        public string KoumutenName { get; set; }
        public decimal? KakouTubosuu { get; set; }
        public bool NoukiMiteiKBN { get; set; }
        public string Nouki { get; set; }
        public string UnsouKuraireDate { get; set; }
        public string KubunCD { get; set; }
        public string TantouEigyouCD { get; set; }
        public string TantouPcCD { get; set; }
        public string TantouCadCD { get; set; }
        public string NyuuryokusakiCD { get; set; }
        public int? TokuchuuzaiUmu { get; set; }
        public string ZairyouNouki { get; set; }
        public string TokuchuuzaiComment { get; set; }
        //Page 2.
        public string JuchuuDate { get; set; }
        public string FusezuTeishutuDate { get; set; }
        public string KakouShouninDate { get; set; }
        public string KidasiDate { get; set; }
        public string KakousijishoHakkouDate { get; set; }
        public string KannouDate { get; set; }
        public string CancelDate { get; set; }
        public string KakouNissuu { get; set; }
        public string UpdateDatetime { get; set; }
        public string KanamonoCD { get; set; }
        public int? OukazaiKakou { get; set; }
        public bool OukazaiSumi { get; set; }
        public int? KabeKakou { get; set; }
        public bool KabeSumi { get; set; }
        public int? HasirazaiKakou { get; set; }
        public bool HasirazaiSumi { get; set; }
        public int? HiuchiKakou { get; set; }
        public bool HiuchiSumi { get; set; }
        public int? HagarazaiKakou { get; set; }
        public bool HagarazaiSumi { get; set; }
        public decimal? HagarazaiSuu { get; set; }
        public int? YukaKakou { get; set; }
        public bool YukaSumi { get; set; }
        public string YukaGouhanShurui { get; set; }
        public decimal? YukaGouhanSuu { get; set; }
        public int? NoziKakou { get; set; }
        public bool NoziSumi { get; set; }
        public string NoziGouhanShurui { get; set; }
        public decimal? NoziGouhanSuu { get; set; }
        public int? TekakouKakou { get; set; }
        public bool TekakouSumi { get; set; }
        public decimal? TekakouTime { get; set; }
        public int? HundeggerKakou { get; set; }
        public bool HundeggerSumi { get; set; }
        public decimal? HundeggerTime { get; set; }
        //Page 3.
        //public int? UpFileOption { get; set; }
        //public int? DownFileOption { get; set; }
        //Page 4.
        public string BukkenComment { get; set; }
        
        public IEnumerable<DropDownListItem> TantouSitenDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouEigyouDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouPcDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouCadDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> KoumutenDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> NyuuryokusakiDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TokuchuuzaiUmuDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> KubunDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> KanamonoDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> GoubanDropDownListItems { get; set; }
    }
}
