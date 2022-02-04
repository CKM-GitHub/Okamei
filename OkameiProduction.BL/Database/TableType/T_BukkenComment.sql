IF EXISTS (select * from sys.table_types where user_type_id = Type_id(N'T_BukkenComment'))
BEGIN
    IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_Create')
        DROP PROCEDURE [InputBukkenShousai_Create]

    IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_Update')
        DROP PROCEDURE [InputBukkenShousai_Update]

    IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_Delete')
        DROP PROCEDURE [InputBukkenShousai_Delete]

    DROP TYPE [T_BukkenComment]
END
GO

CREATE TYPE T_BukkenComment AS TABLE
(
     BukkenNO            varchar(8) NOT NULL
    ,BukkenCommentRows   int
    ,BukkenComment       varchar(100)
    ,UpdateDateTime      datetime
)
GO
