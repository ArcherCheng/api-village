--指令一
SELECT tab.name                   TableName,
       col.colid                  column_id,
       col.name                   column_name,
       typ.name                   data_type,
       col.prec                   PRECISION,
       col.scale                  scale,
       col.length,
       com.TEXT                   default_value,
       CASE
         WHEN col.isnullable = 1 THEN 'Y'
         ELSE 'N'
       END                        is_nullable,
       CASE
         WHEN col.status & 0X80 = 0X80 THEN 'Y'
         ELSE 'N'
       END                        is_identity,
       (SELECT VALUE
        FROM   Fn_listextendedproperty (NULL, 'schema', 'dbo', 'table',
               tab.name,
               'column',
                       col.name)) DESCRIPTION
FROM   sysobjects tab,
       syscolumns col
       LEFT OUTER JOIN syscomments com
                       INNER JOIN sysobjects obj
                         ON com.id = obj.id
         ON col.cdefault = com.id
            AND com.colid = 1,
       systypes typ
WHERE  tab.id = col.id
       AND tab.xtype = 'U'
       AND col.xusertype = typ.xusertype
go


--指令2
SELECT
    a.TableName                as 表格名稱,
    b.COLUMN_NAME               as 欄位名稱,
    b.DATA_TYPE                 as 資料型別,
    b.CHARACTER_MAXIMUM_LENGTH  as 最大長度,
    b.COLUMN_DEFAULT            as 預設值,
    b.IS_NULLABLE               as 允許空值,
    (
        SELECT
            value
        FROM
            fn_listextendedproperty (NULL, 'schema', 'dbo', 'table',
                                     a.TableName, 'column', default)
        WHERE
            name='MS_Description'
            and objtype='COLUMN'
            and objname Collate Chinese_Taiwan_Stroke_CI_AS=b.COLUMN_NAME
    ) as 欄位備註
FROM
    INFORMATION_SCHEMA.TABLES  a
    LEFT JOIN INFORMATION_SCHEMA.COLUMNS b ON (a.TableName=b.TableName)
WHERE
    TABLE_TYPE='BASE TABLE'
ORDER BY
    a.TableName, ordinal_position


CREATE FUNCTION dbo.udf_GetNumeric
(
  @strAlphaNumeric VARCHAR(256)
)
RETURNS VARCHAR(256)
AS
BEGIN
  DECLARE @intAlpha INT
  SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric)
  BEGIN
    WHILE @intAlpha > 0
    BEGIN
      SET @strAlphaNumeric = STUFF(@strAlphaNumeric, @intAlpha, 1, '' )
      SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric )
    END
  END
  RETURN ISNULL(@strAlphaNumeric,0)
END
GO