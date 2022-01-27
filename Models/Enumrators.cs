namespace Models
{
    public enum EMode : int
    {
        None,
        New,
        Edit,
        Delete
    }

    public enum EMultiPorpose : int
    {
        None,
        /// <summary>
        /// 工務店
        /// </summary>
        Koumuten = 1,
        /// <summary>
        /// 区分(補助事業･イノス)
        /// </summary>
        Kubun = 2,
        /// <summary>
        /// 支店
        /// </summary>
        Siten = 3,
        /// <summary>
        /// 担当営業マン
        /// </summary>
        EigyouStaff = 4,
        /// <summary>
        /// 担当PCサポート
        /// </summary>
        PCSupport = 5,
        /// <summary>
        /// 担当CADマン
        /// </summary>
        CADStaff = 6,
        /// <summary>
        /// 入力先
        /// </summary>
        Nyuuryokusaki = 7,
        /// <summary>
        /// 金物
        /// </summary>
        Kanamono = 8,
        /// <summary>
        /// 合板
        /// </summary>
        Gouban = 9,
        /// <summary>
        /// 火打層
        /// </summary>
        HiuchiSou = 10,
        /// <summary>
        /// 火打材料
        /// </summary>
        HiuchiMaterial = 11,
        /// <summary>
        /// 火打等級
        /// </summary>
        HiuchiToukyuu = 12,
        /// <summary>
        /// 品目
        /// </summary>
        Hinmoku = 13,
        /// <summary>
        /// モルダー材料
        /// </summary>
        MoulderMaterial = 14,
        /// <summary>
        /// S見積管理表入力権限
        /// </summary>
        SMitumoriInputStaff = 15,
        /// <summary>
        /// 休日カレンダー
        /// </summary>
        Holiday = 16,
        /// <summary>
        /// 指示書・加工データ送付先
        /// </summary>
        KakouDataSending = 17,
        /// <summary>
        /// 現地案内指示書・地図送信先
        /// </summary>
        MapDataSending = 18,
        /// <summary>
        /// 木出しデータ送付先
        /// </summary>
        KidasiDataSending = 19,
        /// <summary>
        /// ユーザーID・パスワード
        /// </summary>
        LoginUser = 20,
        /// <summary>
        /// 承認期日算出日数
        /// </summary>
        ApprovalDeadline = 999
    }
}
