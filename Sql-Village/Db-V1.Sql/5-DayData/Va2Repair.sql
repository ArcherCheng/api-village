-----------------------------------------------------------------------------------------------------------------------
/*
Drop Table Va2Ceo;    -- 團隊長介紹 President
Drop Table Va2Worker;    -- 隊員介紹
Drop Table Va2Introduce;    -- 團隊介紹
Drop Table Va2Image;    -- 團隊相片
go
Drop Table Va2Repair;    -- 村長(政府)公告
--Drop Table Va2RepairImage;    -- 村長(政府)公告
go
Drop Table Va2Repair;    -- 我的團隊事件通報
--Drop Table Va2RepairImage;    -- 我的團隊事件通報相片
go
Drop Table Va2Party;    -- 我的團隊社團
--Drop Table Va2PartyImage;    -- 我的團隊社團相片
--Drop Table Va2PartyActivity;    -- 我的團隊社團活動
--Drop Table Va2PartyActivityImage;    -- 我的團隊社團相片
--Drop Table Va2PartyActivityTrack;    -- 我的團隊社團活動
--Drop Table Va2PartyActivityTrackImage;    -- 我的團隊社團活動
go
Drop Table GroupActivity;    -- 揪團活動訊息
--Drop Table GroupActivityImage;    -- 揪團活動相片
go
Drop Table TeamSchool;    -- 校園活動訊息
--Drop Table TeamSchoolImage;    -- 揪團活動相片
go
Drop Table Va2TravelView;    -- 我的團隊景點
--Drop Table TeamTravelViewImage;    -- 我的團隊景點
go
Drop Table Va2Shop;    -- 鄰居商店
Drop Table TeamShopItem;    -- 鄰居商店產品
--Drop Table TeamShopItemImage;    -- 鄰居商店產品相片
go
Drop Table TeamForum;    -- 我的團隊論壇
-- Drop Table TeamForumImage;    -- 我的團隊論壇
-- Drop Table TeamForumTrack;    -- 我的團隊論壇
-- Drop Table TeamForumTrackImage;    -- 我的團隊論壇
go
Drop Table TeamShareMeal;    -- 銀髮族共餐
--Drop Table TeamShareMealImage;    -- 銀髮族共餐相片
go
Drop Table TeamWeakServe;    -- 弱勢服務
--Drop Table TeamShareMealImage;    -- 弱勢服務相片
go
Drop Table TeamApprove   -- 我的團隊長證明事項
Go
Drop Table TeamQuestion   -- 我的團隊民線上問卷調查
Drop Table TeamQuestionItme   -- 問卷調查
Drop Table TeamQuestionAnswer;    -- 問卷調查
Drop Table TeamQuestionResult;    -- 問卷調查
go
Drop Table Va2Forum;    -- 相臨鄰居我的團隊
go



*/






-----------------------------------------------------------------------------------------------------------------------
-- 報修資料主檔
/*
DROP TABLE Va2RepairTrack;
go
DROP TABLE Va2Repair;
go

*/
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Va2Repair(
	RepairId   UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Subject    NVARCHAR(200) NOT NULL, --報修事項
	Contents   NVARCHAR(4000) NOT NULL, --報修事項

	SpeedType  NVARCHAR(200) null, --報修速別
	IsTop      Bit NOT NULL DEFAULT 0, --是否置頂
	TopDays    Int DEFAULT 0, --置頂天數

	-- CustomerId NVARCHAR(100) NOT NULL,	--客戶代號,村里代號
	UserId     UniqueIdentifier NOT NULL,
	CreateTime DateTime NOT NULL DEFAULT GetDate(),
	ReadTimes  int NOT NULL,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Va2Repair_PrimaryKey PRIMARY KEY CLUSTERED (RepairId),
    CONSTRAINT Va2Repair_AppUser FOREIGN KEY (UserId) REFERENCES AppUser(UserId)
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Va2Repair_AutoId ON Va2Repair(AutoId);
-- go
CREATE INDEX Va2Repair_Subject  ON Va2Repair(Subject);
GO


-- DROP TRIGGER Va2Repair_TriggerLog
-- go
CREATE TRIGGER Va2Repair_TriggerLog ON Va2Repair AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Va2Repair';

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

-----------------------------------------------------------------------------------------------------------------------
-- DROP TABLE Va2RepairTrack; -- 報修資料主檔
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Va2RepairTrack(
	TrackId      UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	RepairId     UniqueIdentifier NOT NULL,
    TrackType   INT NOT NULL DEFAULT 0, --1:收到報修 2:回報進度 3:報修結案
	Contents     NVARCHAR(4000) NOT NULL, --報修事項

	-- CustomerId NVARCHAR(100) NOT NULL,	--客戶代號,村里代號
	UserId     UniqueIdentifier NOT NULL,
	CreateTime DateTime NOT NULL DEFAULT GetDate(),

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Va2RepairTrack_PrimaryKey PRIMARY KEY CLUSTERED (TrackId),
    CONSTRAINT Va2RepairTrack_AppUser FOREIGN KEY (UserId) REFERENCES AppUser(UserId)
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Va2RepairTrack_AutoId ON Va2RepairTrack(AutoId);
-- go
CREATE INDEX Va2RepairTrack_Subject  ON Va2RepairTrack(RepairId);
GO


-- DROP TRIGGER Va2RepairTrack_TriggerLog
-- go
CREATE TRIGGER Va2RepairTrack_TriggerLog ON Va2RepairTrack AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Va2RepairTrack';

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