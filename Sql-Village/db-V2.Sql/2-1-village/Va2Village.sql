-----------------------------------------------------------------------------------------------------------------------
/*
Drop Table Va2VillageIntroduce;    -- 村里介紹
Drop Table Va2VillagePhoto;    -- 村里介紹相片，文繞圖
go
Drop Table Va2VillageBulletin;    -- 村長(政府)公告
--Drop Table Va2VillageBulletinPhoto;    -- 村長(政府)公告
go
Drop Table Va2VillageAlert;    -- 村里事件通報
--Drop Table Va2VillageAlertPhoto;    -- 村里事件通報相片
go
Drop Table Va2VillageParty;    -- 村里社團
--Drop Table Va2VillagePartyPhoto;    -- 村里社團相片
--Drop Table Va2VillagePartyActivity;    -- 村里社團活動
--Drop Table Va2VillagePartyActivityPhoto;    -- 村里社團相片
--Drop Table Va2VillagePartyActivityReply;    -- 村里社團活動
--Drop Table Va2VillagePartyActivityReplyPhoto;    -- 村里社團活動
go
Drop Table Va2VillageActivity;    -- 揪團活動訊息
--Drop Table Va2VillageActivityPhoto;    -- 揪團活動相片
go
Drop Table Va2VillageSchool;    -- 校園活動訊息
--Drop Table Va2VillageSchoolPhoto;    -- 揪團活動相片
go
Drop Table Va2VillageView;    -- 村里景點
--Drop Table Va2VillageViewPhoto;    -- 村里景點
go
Drop Table Va2VillageShop;    -- 鄰居商店
Drop Table Va2VillageShopItem;    -- 鄰居商店產品
--Drop Table Va2VillageShopItemPhoto;    -- 鄰居商店產品相片
go
Drop Table Va2VillageForum;    -- 村里論壇
-- Drop Table Va2VillageForumPhoto;    -- 村里論壇
-- Drop Table Va2VillageForumReply;    -- 村里論壇
-- Drop Table Va2VillageForumReplyPhoto;    -- 村里論壇
go
Drop Table Va2VillageShareMeal;    -- 銀髮族共餐
--Drop Table Va2VillageShareMealPhoto;    -- 銀髮族共餐相片
go
Drop Table Va2VillageWeakServe;    -- 弱勢服務
--Drop Table Va2VillageShareMealPhoto;    -- 弱勢服務相片
go
Drop Table Va2VillageApprove   -- 村里長證明事項
Go
Drop Table Va2VillageQuestion   -- 村里民線上問卷調查
Drop Table Va2VillageQuestionItme   -- 問卷調查
Drop Table Va2VillageQuestionAnswer;    -- 問卷調查
Drop Table Va2VillageQuestionResult;    -- 問卷調查
go
Drop Table Va2VillageNeighbor;    -- 相臨鄰居村里
go



*/
-----------------------------------------------------------------------------------------------------------------------
-- Drop Table Va2VillageNeighbor    -- 鄰居村里
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Va2VillageNeighbor
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	Id int IDENTITY(1,1) NOT NULL,
	TeamId UniqueIdentifier NOT NULL,--客戶代號 ,村里代號
	NeighborId UniqueIdentifier NOT NULL,--鄰居村里代號
	Direction NVARCHAR(500) ,--方向說明
	Notes NVARCHAR(500) ,--備註說明

	--for custom notes

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser NVARCHAR(100),
	CONSTRAINT Va2VillageNeighbor_PrimaryKey PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT Va2VillageNeighbor_TeamId FOREIGN KEY (TeamId) REFERENCES Au1Team (TeamId) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT Va2VillageNeighbor_NeighborId FOREIGN KEY (NeighborId) REFERENCES Au1Team (ComId)
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Va2Mas10Photo_AutoId ON Va2Mas10Photo(AutoId);
--go
CREATE UNIQUE INDEX Va2VillageNeighbor_TeamId ON Va2VillageNeighbor(TeamId,NeighborId);
Go

-- DROP TRIGGER Va2VillageNeighbor_Trigger1
go
CREATE TRIGGER Va2VillageNeighbor_Trigger1 ON Va2VillageNeighbor AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Va2VillageNeighbor';

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