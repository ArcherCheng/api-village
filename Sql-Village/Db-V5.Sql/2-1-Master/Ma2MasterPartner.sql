
-----------------------------------------------------------------------------------------------------------------------
-- Drop Table Ma2MasterPartner    -- 村里長相片
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Ma2MasterPartner
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	PartnerId UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialId(),
	--客戶代號
	TeamId NVARCHAR(100) NOT NULL,--村里代號
	--TeamId UniqueIdentifier NOT NULL,
	-- 次序
	OrderNo decimal(10,2) NOT NULL DEFAULT 0,
	Title       NVARCHAR(200) NOT NULL, --伙伴抬頭
	PartnerName NVARCHAR(100) NOT NULL,	-- 伙伴姓名
	Description NVARCHAR(1000), -- 用心服務 · 建設社區 · 關懷鄰里
	Sex NVARCHAR(1) NOT NULL,	-- 性別: 1=男,2=女

	--Contact info
	MobileTel NVARCHAR(100) ,--行動電話 📱
	PhotoUrl NVARCHAR(200) ,

	-- 說明
	Notes NVARCHAR(200) ,--備註說明
	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Ma2MasterPartner_PrimaryKey PRIMARY KEY CLUSTERED(PartnerId),
	CONSTRAINT Ma2MasterPartner_ref_Ma1Master FOREIGN KEY (TeamId) REFERENCES Ma1Master (TeamId) ON UPDATE CASCADE ON DELETE CASCADE,
	--CONSTRAINT Ma2MasterPartner_TeamId FOREIGN KEY(TeamId) REFERENCES Au1Team(TeamId)
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Ma2MasterPartner_AutoId ON Ma2MasterPartner(AutoId);
--go
CREATE INDEX Ma2MasterPartner_TeamId ON Ma2MasterPartner(TeamId,OrderNo);
Go

-- DROP TRIGGER Ma2MasterPartner_Trigger1
go
CREATE TRIGGER Ma2MasterPartner_Trigger1 ON Ma2MasterPartner AFTER UPDATE,DELETE NOT FOR REPLICATION AS
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
			Select @tableKey=Convert(NVARCHAR(100),PartnerId) From INSERTED;
		END
	ELSE IF (@insertRows=1) AND (@deleteRows=1)
		Begin	-- Update
			SET @writeType = 2;
			Select @tableKey=Convert(NVARCHAR(100),PartnerId) From INSERTED;
		END
	ELSE IF (@insertRows=0) AND (@deleteRows=1)
		Begin	-- Delete
			SET @writeType = 3;
			Select @tableKey=Convert(NVARCHAR(100),PartnerId) From Deleted;
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
	Values('Ma2MasterPartner',@tableKey,@writeType,@newData,@oldData);
End
Go
