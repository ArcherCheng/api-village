/*

* 📢 最新公告（政策、活動）
* 🛠️ 線上報修（路燈、道路、水溝）
* 🙋‍♂️ 意見反映 / 陳情
* 📅 活動報名
* 🧾 福利申請（補助、物資）

*/
-----------------------------------------------------------------------------------------------------------------------


-----------------------------------------------------------------------------------------------------------------------
/*
Drop Table Tm2Ceo;    -- 團隊長介紹 President
Drop Table Tm2Worker;    -- 隊員介紹
Drop Table Tm2Introduce;    -- 團隊介紹
Drop Table Tm2Image;    -- 團隊相片
go
Drop Table Tm2Bulletin;    -- 村長(政府)公告
--Drop Table Tm2BulletinImage;    -- 村長(政府)公告
go
Drop Table Tm2Repair;    -- 我的團隊事件通報
--Drop Table Tm2RepairImage;    -- 我的團隊事件通報相片

Petition
Announcement
Activity
RepairRequest


go
Drop Table Tm2Party;    -- 我的團隊社團
--Drop Table Tm2PartyImage;    -- 我的團隊社團相片
--Drop Table Tm2PartyActivity;    -- 我的團隊社團活動
--Drop Table Tm2PartyActivityImage;    -- 我的團隊社團相片
--Drop Table Tm2PartyActivityTrack;    -- 我的團隊社團活動
--Drop Table Tm2PartyActivityTrackImage;    -- 我的團隊社團活動
go
Drop Table GroupActivity;    -- 揪團活動訊息
--Drop Table GroupActivityImage;    -- 揪團活動相片
go
Drop Table TeamSchool;    -- 校園活動訊息
--Drop Table TeamSchoolImage;    -- 揪團活動相片
go
Drop Table Tm2TravelView;    -- 我的團隊景點
--Drop Table TeamTravelViewImage;    -- 我的團隊景點
go
Drop Table Tm2Shop;    -- 鄰居商店
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
Drop Table Tm2Forum;    -- 相臨鄰居我的團隊
go

*/
-----------------------------------------------------------------------------------------------------------------------
/*
Drop Table Tm3Announcement;    -- 村里長公告
go
Drop Table Tm2Worker;    -- 隊員介紹
go
Drop Table Tm2MasterEducation;    -- 村里長學歷
go
Drop Table Tm2MasterExperience;   -- 村里長經歷
go
Drop Table Tm2MasterPolicy;   -- 村里長政見
go
Drop Table Tm2MasterPhoto;    -- 村里長相片
go
Drop Table Ma1Master;       -- 村里長基本資料
go

*/
-----------------------------------------------------------------------------------------------------------------------
-- Drop Table Ma1Master       -- 村里長基本資料
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Ma1Master
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	-- MasterId UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),--首長代號
	TeamId NVARCHAR(100) NOT NULL,--村里代號
	MasterName NVARCHAR(100) NOT NULL,	-- 首長姓名
	Description NVARCHAR(1000), -- 用心服務 · 建設社區 · 關懷鄰里
	Sex NVARCHAR(1) NOT NULL,	-- 性別: 1=男,2=女
	Birthday Date,--出生日期
	BirtCity NVARCHAR(100),--出生地縣市
	ElectYear INT ,--當選年份,
	ElectDate Date,--當選日期,

	--Contact info
	MobileTel NVARCHAR(100) ,--行動電話 📱
	OfficeTel NVARCHAR(100) ,--辦公室電話 📞
	Email NVARCHAR(100) ,--電子郵件
	ServiceTime NVARCHAR(100) ,--服務時間 ⏰
	Address NVARCHAR(100) ,--地址 📍
	PhotoUrl NVARCHAR(100) ,--照片網址 🖼️
	LineId NVARCHAR(100) ,--Line ID	👤
	Facebook NVARCHAR(100) ,--Facebook ID 👤
	Threads NVARCHAR(100) ,--Threads ID 👤

	Notes NVARCHAR(500) ,--備註說明

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Ma1Master_PrimaryKey PRIMARY KEY CLUSTERED(TeamId),
	CONSTRAINT Ma1Master_ref_Au1Team FOREIGN KEY (TeamId) REFERENCES Au1Team (TeamId) ON UPDATE CASCADE ON DELETE NO ACTION
);
Go

-- CREATE UNIQUE INDEX Inx_TeamId ON Ma1Master(teamId);
-- Go

-- DROP TRIGGER Ma1Master_Trigger1
go
CREATE TRIGGER Ma1Master_Trigger1 ON Ma1Master AFTER UPDATE,DELETE NOT FOR REPLICATION AS
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
			Select @tableKey=Convert(NVARCHAR(100),MasterName) From INSERTED;
		END
	ELSE IF (@insertRows=1) AND (@deleteRows=1)
		Begin	-- Update
			SET @writeType = 2;
			Select @tableKey=Convert(NVARCHAR(100),MasterName) From INSERTED;
		END
	ELSE IF (@insertRows=0) AND (@deleteRows=1)
		Begin	-- Delete
			SET @writeType = 3;
			Select @tableKey=Convert(NVARCHAR(100),MasterName) From Deleted;
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
	Values('Ma1Master',@tableKey,@writeType,@newData,@oldData);
End
Go

