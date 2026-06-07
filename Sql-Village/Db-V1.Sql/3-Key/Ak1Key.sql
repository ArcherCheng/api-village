/*
Drop Table Ak1KeyRule;     -- 公司規則及法令參數檔
go
drop table Ak1KeyCode;     -- 代碼檔
go

*/
--------------------------------------------------------------------
-- Drop TABLE Ak1KeyRule;
--------------------------------------------------------------------
CREATE TABLE Ak1KeyRule
(
	Id int IDENTITY(1,1) NOT NULL,
	-- AutoId     UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),

	TeamId  NVARCHAR(50) NOT NULL,	--客戶代號,村里代號
	RuleId    NVARCHAR(100) NOT NULL,--規則代號
	RuleGroup NVARCHAR(100) NOT NULL,--歸屬類別
	RuleLabel NVARCHAR(100) NOT NULL,--規則標題
	RuleValue NVARCHAR(100) ,--規則內容
	Notes     NVARCHAR(200) ,--備註說明

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Ak1KeyRule_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT Ak1KeyRule_Aa1Master FOREIGN KEY (TeamId) REFERENCES Aa1Master(TeamId) ON UPDATE CASCADE ON DELETE NO ACTION
);
Go

CREATE UNIQUE INDEX Ak1KeyRule_RuleId ON Ak1KeyRule (TeamId,RuleId);
go

-- DROP TRIGGER Ak1KeyRule_TriggerLog
-- go
CREATE TRIGGER Ak1KeyRule_TriggerLog ON Ak1KeyRule AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Ak1KeyRule';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

 	DECLARE @TeamId NVARCHAR(50);

	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
		BEGIN
			SET @writeType = 1;    -- Insert
			Select @TeamId=TeamId From Inserted;
		END
	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		begin
			SET @writeType = 2;    -- Update
			Select @TeamId=TeamId From Inserted;
		end
	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		begin
 			SET @writeType = 3;    -- Delete
			Select @TeamId=TeamId From Deleted;
		end

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select * From Inserted For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select * From Deleted For Json Auto),1,4000);

	Insert Into AppLogTable(TableName,TeamId,InsertData,DeleteData,WriteType)
	Values(@tableName,@TeamId,@InsertData,@DeleteData,@writeType);
End
Go


--------------------------------------------------------------------
-- Drop TABLE Ak1KeyCode;
--------------------------------------------------------------------
CREATE TABLE Ak1KeyCode
(
	-- AutoId    AUTO_INCREMENT NOT NULL,
	-- Id        UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Id int IDENTITY(1,1) NOT NULL,

	TeamId  NVARCHAR(50) NOT NULL,	--客戶代號,村里代號
	CodeGroup NVARCHAR(100) NOT NULL, --鍵值代號類別
	CodeId    NVARCHAR(100) NOT NULL, --鍵值代號內容
	CodeLabel NVARCHAR(100) NOT NULL, --鍵值代號標題
	SortOrderINT NOT NULL DEFAULT 0, --排序
	Notes NVARCHAR(200) NULL, --備註說明
	IsOnOff Bit NOT NULL Default 1, -- 是否有效

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Ak1KeyCode_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT Ak1KeyCode_Aa1Master FOREIGN KEY (TeamId) REFERENCES Aa1Master(TeamId)
);
go

-- CREATE  CLUSTERED INDEX Ak1KeyCode_AutoId ON Ak1KeyCode(AutoId);
go
CREATE UNIQUE INDEX Ak1KeyCode_CodeId ON Ak1KeyCode (TeamId, CodeGroup ,CodeId);
go

-- DROP TRIGGER Ak1KeyCode_TriggerLog
-- go
CREATE TRIGGER Ak1KeyCode_TriggerLog ON Ak1KeyCode AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Ak1KeyCode';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

 	DECLARE @TeamId NVARCHAR(50);

	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
		BEGIN
			SET @writeType = 1;    -- Insert
			Select @TeamId=TeamId From Inserted;
		END
	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		begin
			SET @writeType = 2;    -- Update
			Select @TeamId=TeamId From Inserted;
		end
	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		begin
 			SET @writeType = 3;    -- Delete
			Select @TeamId=TeamId From Deleted;
		end

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select * From Inserted For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select * From Deleted For Json Auto),1,4000);

	Insert Into AppLogTable(TableName,TeamId,InsertData,DeleteData,WriteType)
	Values(@tableName,@TeamId,@InsertData,@DeleteData,@writeType);
End
Go
