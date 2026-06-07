/*
Drop Table Ap1KeyRule;     -- 公司規則及法令參數檔
go
drop table Ap1KeyCode;     -- 代碼檔
go

Select * from Ap1KeyRule     -- 公司規則及法令參數檔
go
Select * from Ap1KeyCode     -- 代碼檔
go
*/
--------------------------------------------------------------------
-- Drop TABLE Ap1KeyRule;
--------------------------------------------------------------------
CREATE TABLE Ap1KeyRule
(
	Id int IDENTITY(1,1) NOT NULL,
	--AutoId        UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),

	TeamId UniqueIdentifier NOT NULL,
	RuleId NVARCHAR(50) NOT NULL ,--規則代號
	RuleGroup NVARCHAR(50) NOT NULL ,--歸屬類別
	RuleLabel NVARCHAR(100) NOT NULL ,--規則標題
	RuleValue NVARCHAR(100) ,--規則內容
	Notes NVARCHAR(200) ,--備註說明

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	--建檔者資訊
	UpdateUser NVARCHAR(100),
	--更新者資訊
	BatchUser NVARCHAR(100),
	--過帳者資訊
	CONSTRAINT Ap1KeyRule_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT Ap1KeyRule_TeamId FOREIGN KEY(TeamId) REFERENCES Au1Team(TeamId) ON UPDATE CASCADE ON DELETE CASCADE
);
Go

CREATE UNIQUE INDEX Ap1KeyRule_RuleId ON Ap1KeyRule (TeamId,RuleId);
go

--DROP TRIGGER Ap1KeyRuleTrigger1;
go

CREATE TRIGGER Ap1KeyRuleTrigger1 ON Ap1KeyRule AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Ap1KeyRule';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

	IF EXISTS(Select 1
		From Inserted) AND NOT EXISTS(Select 1
		From Deleted)
		SET @writeType = 1;    -- Insert
	ELSE IF EXISTS(Select 1
		From Inserted) AND EXISTS(Select 1
		From Deleted)
		SET @writeType = 2;    -- Update
	ELSE IF NOT EXISTS(Select 1
		From Inserted) AND EXISTS(Select 1
		From Deleted)
		SET @writeType = 3;
	-- Delete

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select *
	From Inserted
	For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select *
	From Deleted
	For Json Auto),1,4000);

	Insert Into AppLogTable
		(TableName,InsertData,DeleteData,WriteType)
	Values(@tableName, @InsertData, @DeleteData, @writeType);
End
Go

--------------------------------------------------------------------
-- Drop TABLE Ap1KeyCode;
--------------------------------------------------------------------
CREATE TABLE Ap1KeyCode
(
	-- AutoId    AUTO_INCREMENT NOT NULL,
	-- Id        UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Id int IDENTITY(1,1) NOT NULL,

	TeamId UniqueIdentifier NOT NULL,
	CodeGroup NVARCHAR(50) NOT NULL,
	--鍵值代號類別
	CodeValue NVARCHAR(50) NOT NULL,
	--鍵值代號內容
	CodeLabel NVARCHAR(50) NOT NULL,
	--鍵值代號標題
	SortOrderINT NOT NULL DEFAULT 0,
	--排序
	Notes NVARCHAR(200) NULL,
	--備註說明
	IsOnOff Bit NOT NULL Default 1,
	-- 是否有效

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	--建檔者資訊
	UpdateUser NVARCHAR(100),
	--更新者資訊
	BatchUser NVARCHAR(100),
	--過帳者資訊
	CONSTRAINT Ap1KeyCode_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT Ap1KeyCode_TeamId FOREIGN KEY(TeamId) REFERENCES Au1Team(TeamId)	ON UPDATE CASCADE ON DELETE CASCADE
);
go

-- CREATE  CLUSTERED INDEX Ap1KeyCode_AutoId ON Ap1KeyCode(AutoId);
go
CREATE UNIQUE INDEX Ap1KeyCode_CodeGroup ON Ap1KeyCode (TeamId, CodeGroup ,CodeValue);
go

--DROP TRIGGER Ap1KeyCode_trigger1;
go
CREATE TRIGGER Ap1KeyCode_trigger1 ON Ap1KeyCode AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Ap1KeyCode';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

	IF EXISTS(Select 1
		From Inserted) AND NOT EXISTS(Select 1
		From Deleted)
		SET @writeType = 1;    -- Insert
	ELSE IF EXISTS(Select 1
		From Inserted) AND EXISTS(Select 1
		From Deleted)
		SET @writeType = 2;    -- Update
	ELSE IF NOT EXISTS(Select 1
		From Inserted) AND EXISTS(Select 1
		From Deleted)
		SET @writeType = 3;
	-- Delete

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select *
	From Inserted
	For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select *
	From Deleted
	For Json Auto),1,4000);

	Insert Into AppLogTable
		(TableName,InsertData,DeleteData,WriteType)
	values(@tableName, @InsertData, @DeleteData, @writeType);
end
go

