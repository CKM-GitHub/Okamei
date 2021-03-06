IF EXISTS (select * from sys.objects where name = 'BukkenItiran_SelectCSVResult')
BEGIN
    DROP PROCEDURE [BukkenItiran_SelectCSVResult]
END
GO


CREATE PROCEDURE [dbo].[BukkenItiran_SelectCSVResult](
    @TantouSitenCD          varchar(10)
    ,@JuchuuOption          tinyint
    ,@BukkenSiteiOption     tinyint
    ,@NoukiStart            date
    ,@NoukiEnd              date
    ,@TantouEigyouCD        varchar(10)
    ,@TantouPcCD            varchar(10)
    ,@TantouCadCD           varchar(10)
    ,@KubunCD               varchar(10)
    ,@BukkenNO              varchar(8)
    ,@BukkenName            varchar(20)
    ,@KoumutenName          varchar(50)
    ,@TokuchuuzaiUmu        tinyint
    ,@UserID                varchar(10)
)AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @SystemDate date = GETDATE()

    SELECT 
        CASE buk.NoukiMiteiKBN WHEN 1 THEN '未定' 
                ELSE FORMAT(buk.Nouki, 'MM/dd') END                     AS 納期
        ,FORMAT(buk.UnsouKuraireDate, 'MM/dd')                          AS 倉庫入
        ,buk.BukkenNO                                                   AS 物件番号
        ,buk.BukkenName													AS 物件名
        ,buk.KoumutenName												AS 工務店
        ,FORMAT(buk.KakouTubosuu, '##0.00')                             AS 坪数
        ,siten.Char2													AS 支店
        ,eigyo.Char2													AS 営業
        ,cad.Char2														AS 入力者
        ,FORMAT(buk.JuchuuDate, 'MM/dd')                                AS 受注日
        ,FORMAT(buk.FusezuTeishutuDate, 'MM/dd')                        AS 伏図提出
        , CASE buk.NoukiMiteiKBN WHEN 1 THEN '' 
          ELSE FORMAT(dbo.fnGetBusinessDay(buk.Nouki, ISNULL(mp999.Num1,0) * -1), 'MM/dd') END  AS 承認期日
        ,FORMAT(buk.KakoutuShouninDate, 'MM/dd')                        AS 加工承認 
        ,CASE WHEN buk.KakousijishoHakkouDate IS NOT NULL THEN '済'
                ELSE FORMAT(dbo.fnGetSijiKijitu(
                            buk.Nouki,
                            buk.UnsouKuraireDate,
                            buk.KanamonoCD,
                            buk.TekakouTime,
                            buk.HundeggerTime), 'MM/dd') END            AS 指示期日
        ,FORMAT(buk.KakouNissuu, '##0')                                 AS 加工日数
        ,FORMAT(buk.UpdateDateTime, 'MM/dd')                            AS 最終更新
        ,kanam.Char1													AS 金物
        ,nyusa.Char1													AS 入力先

        ,CASE buk.TokuchuuzaiUmu 
                WHEN 1 THEN '有' 
                WHEN 2 THEN '無' 
                WHEN 3 THEN '未定' ELSE '' END                           AS 特注材

        ,CASE WHEN buk.KakousijishoHakkouDate IS NOT NULL AND buk.KakousijishoHakkouDate < @SystemDate THEN '加工指示済み'
                WHEN buk.KakoutuShouninDate   IS NOT NULL AND buk.KakoutuShouninDate     < @SystemDate THEN '加工承認済み'
                WHEN buk.FusezuTeishutuDate   IS NOT NULL AND buk.FusezuTeishutuDate     < @SystemDate THEN '伏図提出済み'
                ELSE '' END                                             AS 進捗状況

        ,buk.KoumutenName                                               AS SortKoumutenName
        ,buk.Nouki                                                      AS SortNouki

    FROM D_Bukken buk
    LEFT OUTER JOIN M_MultiPorpose siten ON siten.ID = 3 AND siten.[Key] = buk.TantouSitenCD
    LEFT OUTER JOIN M_MultiPorpose eigyo ON eigyo.ID = 4 AND eigyo.[Key] = buk.TantouEigyouCD
    LEFT OUTER JOIN M_MultiPorpose cad   ON cad.ID   = 6 AND cad.[Key]   = buk.TantouCadCD
    LEFT OUTER JOIN M_MultiPorpose kanam ON kanam.ID = 8 AND kanam.[Key] = buk.KanamonoCD
    LEFT OUTER JOIN M_MultiPorpose nyusa ON nyusa.ID = 7 AND nyusa.[Key] = buk.NyuuryokusakiCD
    LEFT OUTER JOIN M_MultiPorpose mp999 ON mp999.ID = 999 AND mp999.[Key] = 1

    WHERE (@TantouSitenCD IS NULL OR buk.TantouSitenCD = @TantouSitenCD)

      AND ((@JuchuuOption = 1 AND buk.CancelDate IS NULL) 
        OR (@JuchuuOption = 2 AND buk.CancelDate IS NOT NULL)
        )

      AND (@BukkenSiteiOption <> 2 
        OR (buk.TantouEigyouCD = @UserID 
        OR buk.TantouPcCD = @UserID
        OR buk.TantouCadCD = @UserID
        ))

      AND (@NoukiStart IS NULL OR buk.Nouki >= @NoukiStart)
      AND (@NoukiEnd IS NULL OR buk.Nouki <= @NoukiEnd)
      AND (@TantouEigyouCD IS NULL OR buk.TantouEigyouCD = @TantouEigyouCD)
      AND (@TantouPcCD IS NULL OR buk.TantouPcCD = @TantouPcCD)
      AND (@TantouCadCD IS NULL OR buk.TantouCadCD = @TantouCadCD)
      AND (@KubunCD IS NULL OR buk.KubunCD = @KubunCD)
      AND (@BukkenNO IS NULL OR buk.BukkenNO = @BukkenNO)
      AND (@BukkenName IS NULL OR buk.BukkenName like CONCAT('%', @BukkenName, '%'))
      AND (@KoumutenName IS NULL OR buk.KoumutenName like CONCAT('%', @KoumutenName, '%'))
      AND (@TokuchuuzaiUmu = 0 OR buk.TokuchuuzaiUmu = @TokuchuuzaiUmu)

      ORDER BY buk.Nouki, eigyo.Num1, buk.TantouEigyouCD, buk.KoumutenName --まずは納期順で取得
END
