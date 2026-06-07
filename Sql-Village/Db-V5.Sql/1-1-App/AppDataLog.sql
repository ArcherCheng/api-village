/*

drop table AppDataLog;
go

*/
-----------------------------------------
-- 記錄每一筆資料的異動備份
-- drop table AppDataLog
-----------------------------------------
CREATE TABLE AppDataLog(
	Id         BIGINT IDENTITY(1,1) NOT NULL, --序號
	TableName  NVARCHAR(100) NOT NULL, --表格名稱
	TableKey   NVARCHAR(100), --表格主鍵值, 可能是單一主鍵或複合主鍵的組合, 以逗號分隔
	WriteType  INT, --異動類別
	WriteTime  Datetime Default Getdate(), --異動時間
	NewData    NVARCHAR(4000),  -- 異動後資料, 一定要設成 max, 因為可能會有一次多筆的更新或刪除
	OldData    NVARCHAR(4000),  -- 異動前資料, 一定要設成 max, 因為可能會有一次多筆的更新或刪除
	CONSTRAINT AppDataLog_PrimaryKey PRIMARY KEY CLUSTERED (Id)
);
Go



CREATE INDEX Inx_TableName ON AppDataLog(TableName,WriteTime);
Go

CREATE INDEX Inx_WriteTime ON AppDataLog(WriteTime,TableName);
Go

/*
--DROP TRIGGER Au1Team_Trigger1;
go

CREATE TRIGGER Au1Team_Trigger1 ON Au1Team AFTER UPDATE,DELETE ROW AS
Begin
	--只記錄單筆資料的變動,多筆資料的變動不記錄
	DECLARE @insertRows Int;
	DECLARE @deleteRows int;
	SELECT @insertRows=COUNT(*) From INSERTED;
	SELECT @deleteRows=COUNT(*) From DELETED;
	if(@insertRows>1 Or @deleteRows>1) RETURN;
	if(@insertRows=0 And @deleteRows=0) RETURN;

	DECLARE @writeType Int;
	DECLARE @tableKey NVARCHAR(100);
	IF (@insertRows=1) AND (@deleteRows=0)
		BEGIN	-- Insert
			SET @writeType = 1;
			Select @tableKey=Convert(NVARCHAR(100),UserId) From INSERTED;
		END
	ELSE IF (@insertRows=1) AND (@deleteRows=1)
		Begin	-- Update
			SET @writeType = 2;
			Select @tableKey=Convert(NVARCHAR(100),UserId) From INSERTED;
		END
	ELSE IF (@insertRows=0) AND (@deleteRows=1)
		Begin	-- Delete
			SET @writeType = 3;
			Select @tableKey=Convert(NVARCHAR(100),UserId) From Deleted;
		End
	ELSE
		BEGIN	-- No Match
			RETURN;
		END

	DECLARE @newData NVARCHAR(4000);
	DECLARE @oldData NVARCHAR(4000);
	SET @newData=SUBSTRING((Select * From INSERTED For Json Auto),1,4000);
	SET @oldData=SUBSTRING((Select * From DELETED For Json Auto),1,4000);

	Insert Into AppDataLog(TableName,TableKey,WriteType,NewData,OldData)
	Values('Au1User',@tableKey,@writeType,@newData,@oldData);
End
Go
*/