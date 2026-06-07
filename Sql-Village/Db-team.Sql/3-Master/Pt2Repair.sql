-----------------------------------------------------------------------------------------------------------------------
/*
Drop Table Pt2Ceo;    -- 團隊長介紹 President
Drop Table Pt2Worker;    -- 隊員介紹
Drop Table Pt2Introduce;    -- 團隊介紹
Drop Table Pt2Image;    -- 團隊相片
go
Drop Table Pt2Bulletin;    -- 村長(政府)公告
--Drop Table Pt2BulletinImage;    -- 村長(政府)公告
go
Drop Table Pt2Repair;    -- 我的團隊事件通報
--Drop Table Pt2RepairImage;    -- 我的團隊事件通報相片
go
Drop Table Pt2Party;    -- 我的團隊社團
--Drop Table Pt2PartyImage;    -- 我的團隊社團相片
--Drop Table Pt2PartyActivity;    -- 我的團隊社團活動
--Drop Table Pt2PartyActivityImage;    -- 我的團隊社團相片
--Drop Table Pt2PartyActivityTrack;    -- 我的團隊社團活動
--Drop Table Pt2PartyActivityTrackImage;    -- 我的團隊社團活動
go
Drop Table GroupActivity;    -- 揪團活動訊息
--Drop Table GroupActivityImage;    -- 揪團活動相片
go
Drop Table TeamSchool;    -- 校園活動訊息
--Drop Table TeamSchoolImage;    -- 揪團活動相片
go
Drop Table Pt2TravelView;    -- 我的團隊景點
--Drop Table TeamTravelViewImage;    -- 我的團隊景點
go
Drop Table Pt2Shop;    -- 鄰居商店
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
Drop Table Pt2Forum;    -- 相臨鄰居我的團隊
go



*/






-----------------------------------------------------------------------------------------------------------------------
-- 報修資料主檔
/*
DROP TABLE Pt2RepairTrack;
go
DROP TABLE Pt2Repair;
go

*/
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2Repair(
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
	CONSTRAINT Pt2Repair_PrimaryKey PRIMARY KEY CLUSTERED (RepairId),
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Pt2Repair_AutoId ON Pt2Repair(AutoId);
-- go
CREATE INDEX Pt2Repair_Subject  ON Pt2Repair(Subject);
GO


-- DROP TRIGGER Pt2Repair_TriggerLog
-- go
CREATE TRIGGER Pt2Repair_TriggerLog ON Pt2Repair AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Pt2Repair';

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
-- DROP TABLE Pt2RepairTrack; -- 報修資料主檔
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2RepairTrack(
	TrackId      UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	RepairId     UniqueIdentifier NOT NULL,
    TrackType   INT NOT NULL DEFAULT 0, --1:收到報修 2:回報進度 3:報修結案
	Contents     NVARCHAR(4000) NOT NULL, --報修事項

	-- CustomerId NVARCHAR(100) NOT NULL,	--客戶代號,村里代號
	UserId     UniqueIdentifier NOT NULL,
	CreateTime DateTime NOT NULL DEFAULT GetDate(),

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Pt2RepairTrack_PrimaryKey PRIMARY KEY CLUSTERED (TrackId),
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Pt2RepairTrack_AutoId ON Pt2RepairTrack(AutoId);
-- go
CREATE INDEX Pt2RepairTrack_Subject  ON Pt2RepairTrack(RepairId);
GO


-- DROP TRIGGER Pt2RepairTrack_TriggerLog
-- go
CREATE TRIGGER Pt2RepairTrack_TriggerLog ON Pt2RepairTrack AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Pt2RepairTrack';

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