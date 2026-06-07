/*
DROP TABLE MyTable
go
*/
-----------------------------------------
-- MyTable Description
-----------------------------------------
CREATE TABLE MyTable(
	GuidId   UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	--IntId    int IDENTITY(1,1) NOT NULL,
	--AppId    NVARCHAR(50) NOT NULL,
    AppName  NVARCHAR(50) NULL,

	AtDate   Date NULL,
    Amt      DECIMAL(18,3) NOT NULL,
    Items   INT NOT NULL DEFAULT 0,
    IsOn     Bit NOT NULL DEFAULT 0,
	-- relation fields
    RelId   INT NOT NULL DEFAULT 0,
	-- check filds
    OrderAmtINT NOT NULL DEFAULT 0,
    SaleAmt INT NOT NULL DEFAULT 0,
    OffAmt  INT NOT NULL DEFAULT 0,
    TaxAmt  INT NOT NULL DEFAULT 0,
	-- 以下每檔資料表都會有這些欄位
	-- WriteType  INT NOT NULL DEFAULT 0,
    -- WriteTime   DateTime,
	-- WriteUser NVARCHAR(50),
	-- WriteIp   NVARCHAR(50),
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser  NVARCHAR(100),
	CONSTRAINT MyTable_PrimaryKey PRIMARY KEY NonClustered (GuidId),
	--CONSTRAINT MyTable_PrimaryKey PRIMARY KEY CLUSTERED (IntId),
	--CONSTRAINT MyTable_PrimaryKey PRIMARY KEY NonClustered (AppId),
	CONSTRAINT MyTable_RefTable FOREIGN KEY (RelId) REFERENCES RefTable(RelId),
	CONSTRAINT MyTable_OrderAmt Check (OrderAmt = SaleAmt - OffAmt + TaxAmt),
);
go

--CREATE UNIQUE CLUSTERED INDEX MyTable_IntId ON MyTable(IntId);
go
--FOREIGN KEY must CREATE INDEX for performance
CREATE INDEX MyTable_Index1 ON MyTable (RelId, AppName);
go
CREATE UNIQUE INDEX MyTable_Index1 ON MyTable (ComId, AppName);
go

Alter Table MyTable add CONSTRAINT MyTable_RelTable
	FOREIGN KEY (RelId)
	REFERENCES RelTable(RelId)

    ON DELETE NO ACTION
go

CREATE TRIGGER MyTable_TriggerLog ON MyTable AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='MyTable';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
		SET @writeType = 1;    -- Insert
	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		SET @writeType = 2;    -- Update
	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		SET @writeType = 3;    -- Delete

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select * From Inserted For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select * From Deleted For Json Auto),1,4000);

	Insert Into AppLogTable(TableName,InsertData,DeleteData,WriteType) Values(@tableName,@InsertData,@DeleteData,@writeType);
end
go


/*
--建立view
Create View View_Hm_Dep_Emp As
Select 部門 As 編號,部門名稱 As 名稱,上層部門 As 部門,'1900/01/01' As 開始日期,'9999/12/31' As 結束日期 From Hm0Dep10
Union
Select 員工編號 As 編號,姓名     As 名稱,部門     As 部門,到職日期     As 開始日期,Isnull(離職日期,'9999/12/31') As 結束日期 From Hm1Emp10
Go


Https://Dotblogs.Com.Tw/Wasichris/2015/03/11/150705

Select * From Sys.Objects Order By 1

*/