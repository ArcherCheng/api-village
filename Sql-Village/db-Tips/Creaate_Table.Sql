/*
DROP TABLE TableName
go
*/
-----------------------------------------
-- DROP TABLE TableName
-----------------------------------------
CREATE TABLE TableName(
	unique_id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID(),
	auto_id   INT IDENTITY(1,1) NOT NULL,
	app_id    NVARCHAR(50) NOT NULL,
    app_name  NVARCHAR(50) NULL,

	at_date    DATE NULL,
	at_time    NVARCHAR(5) NULL,
    item_amt   DECIMAL(18,3) NOT NULL,
    item_qty   INT NOT NULL DEFAULT 0,

    is_on      BIT NOT NULL DEFAULT 0,
    is_admin   BIT NOT NULL DEFAULT 0,
    can_update BIT NOT NULL DEFAULT 0,

	-- relation field
    rel_id   INT NOT NULL DEFAULT 0,

	-- compute fild
    sale_amt INT NOT NULL DEFAULT 0,
    off_amt  INT NOT NULL DEFAULT 0,
    tax_amt  INT NOT NULL DEFAULT 0,
    order_amt as sale_amt - off_amt + tax_amt,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo  NVARCHAR(100),
	create_user NVARCHAR(100),
	update_user NVARCHAR(100),
	batch_user  NVARCHAR(100),

	-- 設定聚集索引為主鍵
	CONSTRAINT Pkey_TableName PRIMARY KEY CLUSTERED (unique_id),
	CONSTRAINT Pkey_TableName PRIMARY KEY CLUSTERED (auto_id),
	CONSTRAINT Pkey_TableName PRIMARY KEY CLUSTERED (app_id),

	-- 設定非聚集索引為主鍵,可讓主鍵欄位有更好的讀取效能,但會增加寫入的成本,適合讀取頻率高於寫入頻率的情況
	CONSTRAINT Pkey_TableName PRIMARY KEY NonClustered (unique_id),
	INDEX Inx_auto_id UNIQUE CLUSTERED (auto_id),

	-- 設定外來鍵
	CONSTRAINT rel_reftable FOREIGN KEY (rel_id) REFERENCES reftable(rel_id) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT rel_reftable FOREIGN KEY (rel_id) REFERENCES reftable(rel_id) ON UPDATE CASCADE ON DELETE NO ACTION,

	-- 設定檢查約束條件
	CONSTRAINT check_order_amt CHECK  (order_amt = sale_amt - off_amt + tax_amt),
	CONSTRAINT check_at_time CHECK NOT FOR REPLICATION( DBO.check_is_hhmm(at_time)>0),

	-- 設定索引
	INDEX Inx_rel_id ON TableName (rel_id, app_name)
);
go

-- Drop INDEX Inx_auto_id ON TableName;
-- GO
CREATE UNIQUE CLUSTERED INDEX Inx_auto_id ON TableName(auto_id);
GO
-- ALTER INDEX Inx_auto_id ON TableName REBUILD;
-- go

---- FOREIGN KEY must CREATE INDEX for performance
-- Drop INDEX Inx_rel_id ON TableName;
-- GO
CREATE INDEX Inx_rel_id ON TableName (rel_id, app_name);
go
-- ALTER INDEX Inx_rel_id ON TableName REBUILD;
-- go

---- create unique index
-- Drop INDEX Inx_app_name ON TableName;
-- GO
CREATE UNIQUE INDEX Inx_app_name ON TableName (app_name, rel_id);
go
-- ALTER INDEX Inx_app_name ON TableName REBUILD;
-- go


-- FOREIGN KEY
Alter Table TableName add CONSTRAINT rel_reftable
	FOREIGN KEY (rel_id)
	REFERENCES rel_reftable(rel_id)
	ON UPDATE CASCADE
    ON DELETE NO ACTION
go


--DROP TRIGGER TableName_trigger1
go
CREATE TRIGGER TableName_trigger1 ON TableName AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='TableName';

	--只記錄單筆資料的變動,多筆資料的變動不記錄
	DECLARE @insertRows Int;
	DECLARE @deleteRows int;
	SET @insertRows=0;
	SET @deleteRows=0;
	SELECT @insertRows=COUNT(*) From INSERTED;
	SELECT @deleteRows=COUNT(*) From DELETED;
	if(@insertRows>1 Or @deleteRows>1) RETURN;

	DECLARE @writeType Int;
	DECLARE @tableKey NVARCHAR(100);
	SET @writeType=0;
	SET @tableKey='';
	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
		BEGIN
			-- Insert
			SET @writeType = 1;
			Select @tableKey=Convert(NVARCHAR(100),app_id) From INSERTED;
		END
	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		Begin
			-- Update
			SET @writeType = 2;
			Select @tableKey=Convert(NVARCHAR(100),app_id) From INSERTED;
		END
	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		Begin
			-- Delete
			SET @writeType = 3;
			Select @tableKey=Convert(NVARCHAR(100),app_id) From Deleted;
		End

	DECLARE @newData NVARCHAR(4000);
	DECLARE @oldData NVARCHAR(4000);
	SET @newData=SUBSTRING((Select * From INSERTED For Json Auto),1,4000);
	SET @oldData=SUBSTRING((Select * From DELETED For Json Auto),1,4000);

	Insert Into app_table_log(TableName,TableKey,NewData,OldData,WriteType)
	Values(@tableName,@tableKey,@newData,@oldData,@writeType);
end
go


/*
--建立view
Create View View_Hm_Dep_Emp As
Select 部門 As 編號,部門名稱 As 名稱,上層部門 As 部門,'1900/01/01' As 開始日期,'9999/12/31' As 結束日期 From Hm0Dep10
Union
Select 員工編號 As 編號,姓名     As 名稱,部門     As 部門,到職日期     As 開始日期,Isnull(離職日期,'9999/12/31') As 結束日期 From Hm1Emp10
Go

*/


