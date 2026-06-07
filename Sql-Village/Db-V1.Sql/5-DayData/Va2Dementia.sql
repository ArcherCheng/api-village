-----------------------------------------------------------------------------------------------------------------------
/*

Drop Table Va2Dementia
go


*/
-----------------------------------------------------------------------------------------------------------------------
--Drop Table IF EXISTS Va2Dementia
--go


CREATE TABLE Va2Dementia(
 	--Id           int IDENTITY(1,1) NOT NULL,
	DementiaId    UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	TeamId      NVARCHAR(50) NOT NULL, --客戶代號,村里代號，可為空值，方便非村里使用協尋失蹤老人
	UserId        UniqueIdentifier NOT NULL,
    VerifyId      UniqueIdentifier NOT NULL, --驗證Id

	DementiaName  NVARCHAR(100) NOT NULL, --姓名
	Ages         INT NOT NULL DEFAULT 0, --年齡
	MobileTel     NVARCHAR(20) NOT NULL, --行動電話
	HomeTel       NVARCHAR(20) null, --住家電話
	Descriptions  NVARCHAR(2000) null, --特徵描述

    HaveFound     Bit NOT NULL DEFAULT 0, --是否找到了
	FoundDate     DateTime null , --找到日期

	ReadTimes   INT NOT NULL DEFAULT 0,
	CreateTime   DateTime NOT NULL DEFAULT GetDate(),
	OrderTime    DateTime NOT NULL DEFAULT GetDate(),  -- 更新排序時間，有按讚時更新排序

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Va2Dementia_PrimaryKey PRIMARY KEY CLUSTERED (DementiaId),
    CONSTRAINT Va2Dementia_Au1User FOREIGN KEY (UserId) REFERENCES Au1User(UserId), -- ON UPDATE CASCADE ON DELETE SET NULL,
	CONSTRAINT Va2Dementia_Aa1Master FOREIGN KEY (TeamId) REFERENCES Aa1Master(TeamId), --ON UPDATE CASCADE ON DELETE Set Null
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Va2Dementia_AutoId ON Va2Dementia(AutoId);
-- go
CREATE INDEX Va2Dementia_OrderTime  ON Va2Dementia(OrderTime);
GO
CREATE INDEX Va2Dementia_DementiaName  ON Va2Dementia(TeamId,DementiaName);
GO

-- DROP TRIGGER Va2Dementia_TriggerLog
-- go
CREATE TRIGGER Va2Dementia_TriggerLog ON Va2Dementia AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Va2Dementia';

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

DROP VIEW IF EXISTS View_Va2Dementia_Images
GO

Create VIEW View_Va2Dementia_Images AS
SELECT A.DementiaId, A.TeamId, A.UserId, A.DementiaName, A.Ages, A.Descriptions, A.MobileTel, A.HomeTel, A.HaveFound, A.FoundDate, A.ReadTimes, A.CreateTime, A.OrderTime,
	B.FromTable, B.ImageOrder, B.ImageUrl, B.ImageNotes
FROM Va2Dementia A
LEFT JOIN Aa2Image B ON A.DementiaId = B.FromId AND B.FromTable='Va2Dementia'
WHERE A.TeamId IS NOT NULL
GO