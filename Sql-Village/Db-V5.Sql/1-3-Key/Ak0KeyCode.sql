/*

drop table Ak0KeyCode;     -- 代碼檔
go
Select * from Ak0KeyCode     -- 代碼檔
go

*/
--------------------------------------------------------------------
-- Drop TABLE Ak0KeyCode;
--------------------------------------------------------------------
CREATE TABLE Ak0KeyCode
(
	-- auto_id    AUTO_INCREMENT NOT NULL,
	-- id        UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Id int IDENTITY(1,1) NOT NULL,

	-- TeamId NVARCHAR(100) NOT NULL,
	--鍵值代號類別
	CodeGroup NVARCHAR(100) NOT NULL,
	--鍵值代號標題
	CodeLabel NVARCHAR(100) NOT NULL,
	--鍵值代號內容
	CodeValue NVARCHAR(100) NOT NULL,
	--排序
	SortOrder INT NOT NULL DEFAULT 0,
	-- 是否有效
	IsOnOff Bit NOT NULL Default 1,

	Notes NVARCHAR(200) ,--備註說明
	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Pkey_Ak0KeyCode PRIMARY KEY CLUSTERED (Id)
);
go

-- CREATE  CLUSTERED INDEX Ak0KeyCode_AutoId ON Ak0KeyCode(AutoId);
go
CREATE UNIQUE INDEX Inx_CodeValue ON Ak0KeyCode (CodeGroup,CodeLabel,CodeValue);
go

--DROP TRIGGER Ak0KeyCode_trigger1;
go
CREATE TRIGGER Ak0KeyCode_trigger1 ON Ak0KeyCode AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
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
	SET @tableName='Ak0KeyCode';

	IF (@insertRows=1) AND (@deleteRows=0)
		BEGIN	-- Insert
			SET @writeType = 1;
			Select @tableKey=Convert(NVARCHAR(100),Id) From INSERTED;
		END
	ELSE IF (@insertRows=1) AND (@deleteRows=1)
		Begin	-- Update
			SET @writeType = 2;
			Select @tableKey=Convert(NVARCHAR(100),Id) From INSERTED;
		END
	ELSE IF (@insertRows=0) AND (@deleteRows=1)
		Begin	-- Delete
			SET @writeType = 3;
			Select @tableKey=Convert(NVARCHAR(100),Id) From Deleted;
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
end
go

