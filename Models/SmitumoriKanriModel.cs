﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class SmitumoriKanriModel : BaseModel
    {
        public string sagyou_sDate { get; set; }
        public string sagyou_eDate { get; set; }
        public string SMitumoriCounter { get; set; }
        public string SagyouDate { get; set; }
        public string TantouCad { get; set; }
        public string BukkenName { get; set; }
        public string KoumutenName { get; set; }
        public string KakoutuboSuu { get; set; }
        public string TantouSiten { get; set; }
        public string TantouEigyou { get; set; }
        public string KaesiYoteiDate { get; set; }
        public string DetailDataJson { get; set; }
        public IEnumerable<DropDownListItem> TantouCadDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> KoumutenDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouSitenDropDownListItems { get; set; }
        public IEnumerable<DropDownListItem> TantouEigyouDropDownListItems { get; set; }
    }
}
