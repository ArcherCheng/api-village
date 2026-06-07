/*
Drop Table Ak0KeyRule;     -- 公司規則及法令參數檔
go

Select * from Ak0KeyRule     -- 公司規則及法令參數檔
go
*/
--------------------------------------------------------------------
-- Drop TABLE Ak0KeyRule;
--------------------------------------------------------------------
CREATE TABLE Ak0KeyRule
(
	-- Id int IDENTITY(1,1) NOT NULL,
	--AutoId        UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	RuleId NVARCHAR(100) NOT NULL ,--規則代號
	RuleGroup NVARCHAR(100) NOT NULL ,--歸屬類別
	RuleLabel NVARCHAR(100) NOT NULL ,--規則標題
	RuleValue NVARCHAR(100) ,--規則內容
	Notes NVARCHAR(200) ,--備註說明
	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Ak0KeyRule_PrimaryKey PRIMARY KEY CLUSTERED (RuleId)
);
Go

--DROP TRIGGER Ak0KeyRuleTrigger1;
go

CREATE TRIGGER Ak0KeyRuleTrigger1 ON Ak0KeyRule AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	--只記錄單筆資料的變動,多筆資料的變動不記錄
	DECLARE @insertRows Int;
	DECLARE @deleteRows int;
	SELECT @insertRows=COUNT(*) From INSERTED;
	SELECT @deleteRows=COUNT(*) From DELETED;
	if(@insertRows>1 Or @deleteRows>1) RETURN;
	if(@insertRows=0 And @deleteRows=0) RETURN;

	DECLARE @tableName NVARCHAR(100);
	DECLARE @writeType Int;
	DECLARE @tableKey NVARCHAR(100);
	DECLARE @teamId NVARCHAR(100);
	SET @tableName='Ak0KeyRule';

	IF (@insertRows=1) AND (@deleteRows=0)
		BEGIN	-- Insert
			SET @writeType = 1;
			Select @tableKey=Convert(NVARCHAR(100),RuleId) From INSERTED;
		END
	ELSE IF (@insertRows=1) AND (@deleteRows=1)
		Begin	-- Update
			SET @writeType = 2;
			Select @tableKey=Convert(NVARCHAR(100),RuleId) From INSERTED;
		END
	ELSE IF (@insertRows=0) AND (@deleteRows=1)
		Begin	-- Delete
			SET @writeType = 3;
			Select @tableKey=Convert(NVARCHAR(100),RuleId) From Deleted;
		End
	ELSE
		BEGIN	-- No Match
			RETURN;
		END

	DECLARE @newData NVARCHAR(4000);
	DECLARE @oldData NVARCHAR(4000);
	SET @newData=SUBSTRING((Select * From INSERTED For Json Auto),1,4000);
	SET @oldData=SUBSTRING((Select * From DELETED For Json Auto),1,4000);

	Insert Into SysUpdateLog(TableName,TableKey,WriteType,NewData,OldData)
	Values(@tableName,@tableKey,@writeType,@newData,@oldData);
End
Go

