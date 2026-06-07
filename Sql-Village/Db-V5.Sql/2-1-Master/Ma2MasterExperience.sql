

-----------------------------------------------------------------------------------------------------------------------
-- Drop Table Ma2MasterExperience    -- 村里長經歷
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Ma2MasterExperience
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	Id int IDENTITY(1,1) NOT NULL,
	TeamId NVARCHAR(100) NOT NULL,--首長代號
	OrderNo decimal(10,2), -- 次序
	OrderTitle NVARCHAR(50) ,	-- 序號
	Descriptions NVARCHAR(1000) NOT NULL,	-- 學歷
	Notes NVARCHAR(200) ,--備註說明

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Ma2MasterExperience_PrimaryKey PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT Ma2MasterExperience_ref_Ma1Master FOREIGN KEY (TeamId) REFERENCES Ma1Master (TeamId) ON UPDATE CASCADE ON DELETE NO ACTION
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Ma2MasterExperience_AutoId ON Ma2MasterExperience(AutoId);
--go
CREATE INDEX Inx_TeamId ON Ma2MasterExperience(TeamId,OrderNo);
Go

-- DROP TRIGGER Ma2MasterExperience_Trigger1
go
CREATE TRIGGER Ma2MasterExperience_Trigger1 ON Ma2MasterExperience AFTER UPDATE,DELETE NOT FOR REPLICATION AS
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

	Insert Into AppDataLog(TableName,TableKey,WriteType,NewData,OldData)
	Values('Ma2MasterExperience',@tableKey,@writeType,@newData,@oldData);
End
Go