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
        public decimal KakouTubosuu { get; set; }
        public byte NoukiMiteiKBN { get; set; }
        public string Nouki { get; set; }
        public string UnsouKuraireDate { get; set; }
        public string KubunCD { get; set; }
        public string TantouEigyouCD { get; set; }
        public string TantouPcCD { get; set; }
        public string TantouCadCD { get; set; }
        public string NyuuryokusakiCD { get; set; }
        public byte TokuchuuzaiUmu { get; set; }
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
        public int KakouNissuu { get; set; }
        public string UpdateDatetime { get; set; }
        public string KanamonoCD { get; set; }
        public byte OukazaiKakou { get; set; }
        public byte OukazaiSumi { get; set; }
        public byte KabeKakou { get; set; }
        public byte KabeSumi { get; set; }
        public byte HasirazaiKakou { get; set; }
        public byte HasirazaiSumi { get; set; }
        public byte HiuchiKakou { get; set; }
        public byte HiuchiSumi { get; set; }
        public byte HagarazaiKakou { get; set; }
        public byte HagarazaiSumi { get; set; }
        public int HagarazaiSuu { get; set; }
        public byte YukaKakou { get; set; }
        public byte YukaSumi { get; set; }
        public string YukaGouhanShurui { get; set; }
        public int YukaGouhanSuu { get; set; }
        public byte NoziKakou { get; set; }
        public byte NoziSumi { get; set; }
        public string NoziGouhanShurui { get; set; }
        public int NoziGouhanSuu { get; set; }
        public byte TekakouKakou { get; set; }
        public byte TekakouSumi { get; set; }
        public decimal TekakouTime { get; set; }
        public byte HundeggerKakou { get; set; }
        public byte HundeggerSumi { get; set; }
        public decimal HundeggerTime { get; set; }
        //Page 3.
        //Page 4.
        public string BukkenComment { get; set; }
        public string HiddenUpdateDatetime { get; set; }


        public IEnumerable<DropDownListItem> TantouSitenDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouEigyouDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouPcDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouCadDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> KoumutenDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> NyuuryokusakiDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TokuchuuzaiUmuDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> KubunDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> KanamonoDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> GouhanDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> WithOrWithoutDropDownListItems { get; set; }
    }
}
