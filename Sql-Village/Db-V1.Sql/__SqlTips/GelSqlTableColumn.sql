USE HrModel;
SELECT a.Table_schema +'.'+a.TableName   as 表格名稱
       ,b.COLUMN_NAME                     as 欄位名稱
       ,b.DATA_TYPE                       as 資料型別
       ,isnull(b.CHARACTER_MAXIMUM_LENGTH,'') as 長度
       ,isnull(b.COLUMN_DEFAULT,'')           as 預設值
       ,b.IS_NULLABLE                         as 是否允許空值
       ,( SELECT value
          FROM fn_listextendedproperty (NULL, 'schema', a.Table_schema, 'table', a.TableName, 'column', default)
          WHERE name='MS_Description' and objtype='COLUMN'
          and objname Collate Chinese_Taiwan_Stroke_CI_AS = b.COLUMN_NAME
        ) as 欄位描述
FROM INFORMATION_SCHEMA.TABLES  a
 LEFT JOIN INFORMATION_SCHEMA.COLUMNS b ON a.TableName = b.TableName
WHERE TABLE_TYPE='BASE TABLE'
ORDER BY a.TableName , b.ORDINAL_POSITION



-------------------------------------------這是指令碼------------------------------------------------------

--簡單介紹一下上面的語法

--INFORMATION_SCHEMA.TABLES //查詢該資料庫裡所有資料表資訊

--INFORMATION_SCHEMA.COLUMNS  //查詢該資料表裡所有資料欄位資訊

--fn_listextendedproperty //列出資料表欄位的資訊(為了取得 欄位描述 而使用)

--※Chinese_Taiwan_Stroke_CI_AS  //這是指定資料庫的編碼(此為 台灣繁體中文且不分大小寫)

-------------------------------------------------------------------------------------
--使用指令統計所有資料表使用容量
SELECT
    t.NAME AS TableName,
    s.Name AS SchemaName,
    p.rows,
    SUM(a.total_pages) * 8 AS TotalSpaceKB,
    CAST(ROUND(((SUM(a.total_pages) * 8) / 1024.00), 2) AS NUMERIC(36, 2)) AS TotalSpaceMB,
    SUM(a.used_pages) * 8 AS UsedSpaceKB,
    CAST(ROUND(((SUM(a.used_pages) * 8) / 1024.00), 2) AS NUMERIC(36, 2)) AS UsedSpaceMB,
    (SUM(a.total_pages) - SUM(a.used_pages)) * 8 AS UnusedSpaceKB,
    CAST(ROUND(((SUM(a.total_pages) - SUM(a.used_pages)) * 8) / 1024.00, 2) AS NUMERIC(36, 2)) AS UnusedSpaceMB
FROM
    sys.tables t
INNER JOIN
    sys.indexes i ON t.OBJECT_ID = i.object_id
INNER JOIN
    sys.partitions p ON i.object_id = p.OBJECT_ID AND i.index_id = p.index_id
INNER JOIN
    sys.allocation_units a ON p.partition_id = a.container_id
LEFT OUTER JOIN
    sys.schemas s ON t.schema_id = s.schema_id
WHERE
    t.NAME NOT LIKE 'dt%'
    AND t.is_ms_shipped = 0
    AND i.OBJECT_ID > 255
GROUP BY
    t.Name, s.Name, p.Rows
ORDER BY
    TotalSpaceMB DESC, t.Name

--查詢單一資料表使用容量
EXEC sp_spaceused N'dbo.ot2day10';
GO
--查詢所有資料表使用容量
sp_msforeachtable N'EXEC sp_spaceused [?]';
GO
--查詢某欄位裡面的字串長度
Select len(InsertData),InsertData From applogtable order by len(InsertData) desc








