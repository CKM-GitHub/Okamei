IF EXISTS (select * from sys.table_types where user_type_id = Type_id(N'T_BukkenMoulder'))
BEGIN
    IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_CreateBukkenMoulder')
        DROP PROCEDURE [InputBukkenShousai_CreateBukkenMoulder]

    DROP TYPE [T_BukkenMoulder]
END
GO

CREATE TYPE T_BukkenMoulder AS TABLE
(
    BukkenNO                varchar(8)
    ,BukkenMoulderRows      int
    ,MoulderHinmoku         varchar(10)
    ,MoulderZairyou         varchar(10)
    ,MoulderHaba            int
    ,MoulderNari            int
    ,MoulderNagasa          int
    ,MoulderHonsuu          int
    ,MoulderKakouDateTime   datetime
    ,MoulderSumi            tinyint
    ,UpdateDateTime         datetime
    ,DisplayOrder           int
)
GO
