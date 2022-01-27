IF EXISTS (select * from sys.objects where name = 'BukkenItiran_ExistsDisplayResult')
BEGIN
    DROP PROCEDURE [BukkenItiran_ExistsDisplayResult]
END
GO

CREATE PROCEDURE [dbo].[BukkenItiran_ExistsDisplayResult](
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

    SELECT TOP 1 BukkenNO

    FROM D_Bukken buk

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
END
