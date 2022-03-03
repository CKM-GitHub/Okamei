IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_CreateBukkenHiuchi')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_CreateBukkenHiuchi]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_CreateBukkenHiuchi](
     @SouCount                  int
    ,@BukkenNO                  varchar(8)
    ,@Sou1                      varchar(10)
    ,@Zairyou11                 varchar(30)
    ,@Toukyuu11                 varchar(10)
    ,@Honsuu11                  int
    ,@Zairyou12                 varchar(30)
    ,@Toukyuu12                 varchar(10)
    ,@Honsuu12                  int
    ,@Zairyou13                 varchar(30)
    ,@Toukyuu13                 varchar(10)
    ,@Honsuu13                  int
    ,@Sou1Sumi                  tinyint
    ,@Sou2                      varchar(10)
    ,@Zairyou21                 varchar(30)
    ,@Toukyuu21                 varchar(10)
    ,@Honsuu21                  int
    ,@Zairyou22                 varchar(30)
    ,@Toukyuu22                 varchar(10)
    ,@Honsuu22                  int
    ,@Zairyou23                 varchar(30)
    ,@Toukyuu23                 varchar(10)
    ,@Honsuu23                  int
    ,@Sou2Sumi                  tinyint
    ,@Sou3                      varchar(10)
    ,@Zairyou31                 varchar(30)
    ,@Toukyuu31                 varchar(10)
    ,@Honsuu31                  int
    ,@Zairyou32                 varchar(30)
    ,@Toukyuu32                 varchar(10)
    ,@Honsuu32                  int
    ,@Zairyou33                 varchar(30)
    ,@Toukyuu33                 varchar(10)
    ,@Honsuu33                  int
    ,@Sou3Sumi                  tinyint
    ,@Sou4                      varchar(10)
    ,@Zairyou41                 varchar(30)
    ,@Toukyuu41                 varchar(10)
    ,@Honsuu41                  int
    ,@Zairyou42                 varchar(30)
    ,@Toukyuu42                 varchar(10)
    ,@Honsuu42                  int
    ,@Zairyou43                 varchar(30)
    ,@Toukyuu43                 varchar(10)
    ,@Honsuu43                  int
    ,@Sou4Sumi                  tinyint
    ,@Operator                  varchar(10)
    ,@UpdateDateTime            varchar(23)
    ,@OutExclusionError         tinyint OUTPUT
)AS
BEGIN
    SET @OutExclusionError = 0
    DECLARE @SysDatetime datetime = GETDATE()

    DECLARE @OldSou1KakouDateTime datetime
    DECLARE @OldSou2KakouDateTime datetime
    DECLARE @OldSou3KakouDateTime datetime
    DECLARE @OldSou4KakouDateTime datetime

    SELECT @OldSou1KakouDateTime = Sou1KakouDateTime,
           @OldSou2KakouDateTime = Sou2KakouDateTime,
           @OldSou3KakouDateTime = Sou3KakouDateTime,
           @OldSou4KakouDateTime = Sou4KakouDateTime
    FROM D_BukkenHiuchi
    WHERE BukkenNO = @BukkenNO


    DELETE FROM D_BukkenHiuchi
    WHERE BukkenNO = @BukkenNO
    AND UpdateDateTime = @UpdateDateTime

    IF @@ROWCOUNT = 0 AND @UpdateDateTime IS NOT NULL
    BEGIN
        SET @OutExclusionError = 1
        RETURN
    END

    IF @SouCount > 0
	BEGIN
        INSERT INTO D_BukkenHiuchi
        (
            BukkenNO
            ,Sou1
            ,Zairyou11
            ,Toukyuu11
            ,Honsuu11
            ,Zairyou12
            ,Toukyuu12
            ,Honsuu12
            ,Zairyou13
            ,Toukyuu13
            ,Honsuu13
            ,Sou1Sumi
            ,Sou1KakouDateTime
            ,Sou2
            ,Zairyou21
            ,Toukyuu21
            ,Honsuu21
            ,Zairyou22
            ,Toukyuu22
            ,Honsuu22
            ,Zairyou23
            ,Toukyuu23
            ,Honsuu23
            ,Sou2Sumi
            ,Sou2KakouDateTime
            ,Sou3
            ,Zairyou31
            ,Toukyuu31
            ,Honsuu31
            ,Zairyou32
            ,Toukyuu32
            ,Honsuu32
            ,Zairyou33
            ,Toukyuu33
            ,Honsuu33
            ,Sou3Sumi
            ,Sou3KakouDateTime
            ,Sou4
            ,Zairyou41
            ,Toukyuu41
            ,Honsuu41
            ,Zairyou42
            ,Toukyuu42
            ,Honsuu42
            ,Zairyou43
            ,Toukyuu43
            ,Honsuu43
            ,Sou4Sumi
            ,Sou4KakouDateTime
            ,InsertOperator
            ,InsertDateTime
            ,UpdateOperator
            ,UpdateDateTime
        )
        VALUES
        (
             @BukkenNO
            ,@Sou1
            ,@Zairyou11
            ,@Toukyuu11
            ,@Honsuu11
            ,@Zairyou12
            ,@Toukyuu12
            ,@Honsuu12
            ,@Zairyou13
            ,@Toukyuu13
            ,@Honsuu13
            ,@Sou1Sumi
            ,CASE @Sou1Sumi WHEN 1 THEN ISNULL(@OldSou1KakouDateTime, @SysDatetime) ELSE NULL END
            ,@Sou2
            ,@Zairyou21
            ,@Toukyuu21
            ,@Honsuu21
            ,@Zairyou22
            ,@Toukyuu22
            ,@Honsuu22
            ,@Zairyou23
            ,@Toukyuu23
            ,@Honsuu23
            ,@Sou2Sumi
            ,CASE @Sou2Sumi WHEN 1 THEN ISNULL(@OldSou2KakouDateTime, @SysDatetime) ELSE NULL END
            ,@Sou3
            ,@Zairyou31
            ,@Toukyuu31
            ,@Honsuu31
            ,@Zairyou32
            ,@Toukyuu32
            ,@Honsuu32
            ,@Zairyou33
            ,@Toukyuu33
            ,@Honsuu33
            ,@Sou3Sumi
            ,CASE @Sou3Sumi WHEN 1 THEN ISNULL(@OldSou3KakouDateTime, @SysDatetime) ELSE NULL END
            ,@Sou4
            ,@Zairyou41
            ,@Toukyuu41
            ,@Honsuu41
            ,@Zairyou42
            ,@Toukyuu42
            ,@Honsuu42
            ,@Zairyou43
            ,@Toukyuu43
            ,@Honsuu43
            ,@Sou4Sumi
            ,CASE @Sou4Sumi WHEN 1 THEN ISNULL(@OldSou4KakouDateTime, @SysDatetime) ELSE NULL END
            ,@Operator
            ,@SysDatetime
            ,@Operator
            ,@SysDatetime
        )
	END
END
