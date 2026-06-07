/*
Drop Table Ab1KeyRule;     -- 公司規則及法令參數檔
go
drop table Ab1KeyCode;     -- 代碼檔
go

*/
--------------------------------------------------------------------
-- Drop TABLE Ab1KeyRule;
--------------------------------------------------------------------
CREATE TABLE Ab1KeyRule
(
	Id int IDENTITY(1,1) NOT NULL,
	-- AutoId     UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),

	TeamId     NVARCHAR(100) NOT NULL,	--客戶代號,村里代號
	RuleId    NVARCHAR(100) NOT NULL,--規則代號
	RuleGroup NVARCHAR(100) NOT NULL,--歸屬類別
	RuleLabel NVARCHAR(100) NOT NULL,--規則標題
	RuleValue NVARCHAR(100) ,--規則內容
	Notes     NVARCHAR(200) ,--備註說明

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Ab1KeyRule_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT Ab1KeyRule_Au1Team10 FOREIGN KEY (TeamId) REFERENCES Au1Team10(TeamId)
);
Go

CREATE UNIQUE INDEX Ab1KeyRule_RuleId ON Ab1KeyRule (TeamId,RuleId);
go

-- DROP TRIGGER Ab1KeyRule_TriggerLog
-- go
CREATE TRIGGER Ab1KeyRule_TriggerLog ON Ab1KeyRule AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Ab1KeyRule';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

	IF EXISTS(Select 1
		From Inserted) AND NOT EXISTS(SELECT 1 FROM DELETED)
		SET @writeType = 1; -- Insert
	ELSE IF EXISTS(Select 1
		From Inserted) AND EXISTS(SELECT 1 FROM DELETED)
		SET @writeType = 2; -- Update
	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		SET @writeType = 3;	-- Delete

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select *	From Inserted For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select *	From Deleted For Json Auto),1,4000);

	Insert Into AppLogTable	(TableName,InsertData,DeleteData,WriteType)
	Values(@tableName, @InsertData, @DeleteData, @writeType);
End
Go


--------------------------------------------------------------------
-- Drop TABLE Ab1KeyCode;
--------------------------------------------------------------------
CREATE TABLE Ab1KeyCode
(
	-- AutoId    AUTO_INCREMENT NOT NULL,
	-- Id        UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Id int IDENTITY(1,1) NOT NULL,

	TeamId     NVARCHAR(100) NOT NULL,	--客戶代號,村里代號
	CodeGroup NVARCHAR(100) NOT NULL, --鍵值代號類別
	CodeId    NVARCHAR(100) NOT NULL, --鍵值代號內容
	CodeLabel NVARCHAR(100) NOT NULL, --鍵值代號標題
	SortOrderINT NOT NULL DEFAULT 0, --排序
	Notes NVARCHAR(200) NULL, --備註說明
	IsOnOff Bit NOT NULL Default 1, -- 是否有效

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Ab1KeyCode_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT Ab1KeyCode_Au1Team10 FOREIGN KEY (TeamId) REFERENCES Au1Team10(TeamId)
);
go

-- CREATE  CLUSTERED INDEX Ab1KeyCode_AutoId ON Ab1KeyCode(AutoId);
go
CREATE UNIQUE INDEX Ab1KeyCode_CodeId ON Ab1KeyCode (TeamId, CodeGroup ,CodeId);
go

-- DROP TRIGGER Ab1KeyCode_TriggerLog
-- go
CREATE TRIGGER Ab1KeyCode_TriggerLog ON Ab1KeyCode AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Ab1KeyCode';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

	IF EXISTS(Select 1
		From Inserted) AND NOT EXISTS(SELECT 1 FROM DELETED)
		SET @writeType = 1; -- Insert
	ELSE IF EXISTS(Select 1
		From Inserted) AND EXISTS(SELECT 1 FROM DELETED)
		SET @writeType = 2; -- Update
	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		SET @writeType = 3;	-- Delete

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select *	From Inserted For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select *	From Deleted For Json Auto),1,4000);

	Insert Into AppLogTable	(TableName,InsertData,DeleteData,WriteType)
	Values(@tableName, @InsertData, @DeleteData, @writeType);
End
Go
