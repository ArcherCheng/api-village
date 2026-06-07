
-----------------------------------------------------------------------------------------------------------------------
-- Drop Table Ma2MasterPhoto    -- 村里長相片
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Ma2MasterPhoto
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	Id int IDENTITY(1,1) NOT NULL,
	--客戶代號
	TeamId NVARCHAR(100) NOT NULL,--村里代號
	--TeamId UniqueIdentifier NOT NULL,
	-- 次序
	OrderNo decimal(10,2) NOT NULL DEFAULT 0,
	IsMain Bit NOT NULL DEFAULT 0,

	PublicKey NVARCHAR(200) ,
	PhotoUrl NVARCHAR(200) ,
	-- 說明
	Descriptions NVARCHAR(1000) ,
	Notes NVARCHAR(200) ,--備註說明
	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Ma2MasterPhoto_PrimaryKey PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT Ma2MasterPhoto_ref_Ma1Master FOREIGN KEY (TeamId) REFERENCES Ma1Master (TeamId) ON UPDATE CASCADE ON DELETE CASCADE,
	--CONSTRAINT Ma2MasterPhoto_TeamId FOREIGN KEY(TeamId) REFERENCES Au1Team(TeamId)
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Ma2MasterPhoto_AutoId ON Ma2MasterPhoto(AutoId);
--go
CREATE INDEX Ma2MasterPhoto_TeamId ON Ma2MasterPhoto(TeamId,OrderNo);
Go

-- DROP TRIGGER Ma2MasterPhoto_Trigger1
go
CREATE TRIGGER Ma2MasterPhoto_Trigger1 ON Ma2MasterPhoto AFTER UPDATE,DELETE NOT FOR REPLICATION AS
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
	Values('Ma2MasterPhoto',@tableKey,@writeType,@newData,@oldData);
End
Go

