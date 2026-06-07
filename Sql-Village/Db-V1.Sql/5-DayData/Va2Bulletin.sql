-- -----------------------------------------------------------------------------------------------------------------------
-- -- 公告欄資料主檔
-- /*
-- DROP TABLE Va2BulletinItem;
-- go
-- DROP TABLE Va2Bulletin;
-- go
-- */
-- -----------------------------------------------------------------------------------------------------------------------
-- CREATE TABLE Va2Bulletin(
--  	--BbsId           int IDENTITY(1,1) NOT NULL,
-- 	BbsId       UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
-- 	TeamId    NVARCHAR(50) null,	--客戶代號,村里代號
-- 	UserId      UniqueIdentifier NULL,
--     VerifyId    UniqueIdentifier NULL, --驗證Id

-- 	Subjects    NVARCHAR(200) NOT NULL, --公告主題
-- 	AtDate      Date NOT NULL DEFAULT GetDate(), --公告日期
-- 	-- DocNo       NVARCHAR(200) null, --公告文號
-- 	-- SpeedType   NVARCHAR(200) null, --公告速別
-- 	-- SecretType  NVARCHAR(200) null, --密等級別
-- 	-- Recipient   NVARCHAR(200) null, --公告收件人
-- 	-- Secondary   NVARCHAR(200) null, --副本收件人
--     -- PdfFileUrl  NVARCHAR(200) , -- 公告本文正件PDF

--     -- IsTop       Bit NOT NULL DEFAULT 0, --是否置頂
-- 	-- TopDays     Int DEFAULT 0, --置頂天數
--     IsOnOff     Bit NOT NULL DEFAULT 0, --是否啟用
--     IsDelete    Bit NOT NULL DEFAULT 0,

-- 	ReadTimes  INT NOT NULL DEFAULT 0,
-- 	CreateTime  DateTime NOT NULL DEFAULT GetDate(),
-- 	OrderTime   DateTime NOT NULL DEFAULT GetDate(),  -- 更新排序時間，有按讚時更新排序


-- 	-- 以下每檔資料表都會有這些欄位
-- 	WriteInfo NVARCHAR(100),
-- 	CONSTRAINT Va2Bulletin_PrimaryKey PRIMARY KEY CLUSTERED (BbsId),
--     CONSTRAINT Va2Bulletin_Au1User FOREIGN KEY (UserId) REFERENCES Au1User(UserId),
-- 	CONSTRAINT Va2Bulletin_Aa1Master FOREIGN KEY (TeamId) REFERENCES Aa1Master(TeamId)
-- );
-- GO

-- -- CREATE UNIQUE CLUSTERED INDEX Va2Bulletin_AutoId ON Va2Bulletin(AutoId);
-- -- go
-- CREATE INDEX Va2Bulletin_BbsSubject  ON Va2Bulletin(TeamId,Subjects,AtDate);
-- GO


-- -- DROP TRIGGER Va2Bulletin_TriggerLog
-- -- go
-- CREATE TRIGGER Va2Bulletin_TriggerLog ON Va2Bulletin AFTER UPDATE,DELETE NOT FOR REPLICATION AS
-- Begin
-- 	DECLARE @tableName NVARCHAR(100);
-- 	SET @tableName='Va2Bulletin';

-- 	DECLARE @writeType Tinyint;
-- 	SET @writeType=0;

--  	DECLARE @TeamId NVARCHAR(50);

-- 	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
-- 		BEGIN
-- 			SET @writeType = 1;    -- Insert
-- 			Select @TeamId=TeamId From Inserted;
-- 		END
-- 	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
-- 		begin
-- 			SET @writeType = 2;    -- Update
-- 			Select @TeamId=TeamId From Inserted;
-- 		end
-- 	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
-- 		begin
--  			SET @writeType = 3;    -- Delete
-- 			Select @TeamId=TeamId From Deleted;
-- 		end

-- 	DECLARE @InsertData NVARCHAR(4000);
-- 	DECLARE @DeleteData NVARCHAR(4000);

-- 	SET @InsertData=SUBSTRING((Select * From Inserted For Json Auto),1,4000);
-- 	SET @DeleteData=SUBSTRING((Select * From Deleted For Json Auto),1,4000);

-- 	Insert Into AppLogTable(TableName,TeamId,InsertData,DeleteData,WriteType)
-- 	Values(@tableName,@TeamId,@InsertData,@DeleteData,@writeType);
-- End
-- Go

-- -----------------------------------------------------------------------------------------------------------------------
-- -- DROP TABLE Va2BulletinItem; -- 公告欄資料子檔
-- -----------------------------------------------------------------------------------------------------------------------
-- CREATE TABLE Va2BulletinItem(
--  	Id              int IDENTITY(1,1) NOT NULL,
--  	BbsId           UniqueIdentifier NOT NULL,
-- 	TeamId        NVARCHAR(50) NOT NULL,	--客戶代號,村里代號

--  	OrderSeq        int NOT NULL,
-- 	OrderNo         nvarchar(20) NOT NULL , --公告項目序號
-- 	Contents        nvarchar(4000) NOT NULL , --公告項目說明

-- 	-- 以下每檔資料表都會有這些欄位
-- 	WriteInfo NVARCHAR(100),
-- 	CONSTRAINT Va2BulletinItem_PrimaryKey PRIMARY KEY CLUSTERED (Id),
-- 	CONSTRAINT Va2BulletinItem_Va2Bulletin FOREIGN KEY (BbsId) REFERENCES Va2Bulletin (BbsId) ON UPDATE CASCADE ON DELETE CASCADE,
-- );
-- GO

-- -- CREATE UNIQUE CLUSTERED INDEX Va2BulletinItem_AutoId ON Va2BulletinItem(AutoId);
-- -- go
-- CREATE INDEX Va2BulletinItem_BbsId ON Va2BulletinItem(BbsId,OrderSeq);
-- GO


-- -- DROP TRIGGER Va2BulletinItem_TriggerLog
-- go
-- CREATE TRIGGER Va2BulletinItem_TriggerLog ON Va2BulletinItem AFTER UPDATE,DELETE NOT FOR REPLICATION AS
-- Begin
-- 	DECLARE @tableName NVARCHAR(50);
-- 	SET @tableName='Va2BulletinItem';

-- 	DECLARE @writeType Tinyint;
-- 	SET @writeType=0;

--  	DECLARE @TeamId NVARCHAR(50);

-- 	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
-- 		BEGIN
-- 			SET @writeType = 1;    -- Insert
-- 			Select @TeamId=TeamId From Inserted;
-- 		END
-- 	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
-- 		begin
-- 			SET @writeType = 2;    -- Update
-- 			Select @TeamId=TeamId From Inserted;
-- 		end
-- 	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
-- 		begin
--  			SET @writeType = 3;    -- Delete
-- 			Select @TeamId=TeamId From Deleted;
-- 		end

-- 	DECLARE @InsertData NVARCHAR(4000);
-- 	DECLARE @DeleteData NVARCHAR(4000);

-- 	SET @InsertData=SUBSTRING((Select * From Inserted For Json Auto),1,4000);
-- 	SET @DeleteData=SUBSTRING((Select * From Deleted For Json Auto),1,4000);

-- 	Insert Into AppLogTable(TableName,TeamId,InsertData,DeleteData,WriteType)
-- 	Values(@tableName,@TeamId,@InsertData,@DeleteData,@writeType);
-- End
-- GO


-- /*


-- insert into Va2Bulletin(BbsSubject,AtDate,IsTop,TopDays,UserId)
-- values('Bbs Subjects 1','2025-06-01',1,30,'e9d49073-853f-f011-89a6-18c04d10aecf')
-- go

-- insert into Va2Bulletin(BbsSubject,AtDate,IsTop,TopDays,UserId)
-- values('Bbs Subjects 2','2025-06-01',0,0,'e9d49073-853f-f011-89a6-18c04d10aecf')
-- go

-- insert into Va2Bulletin(BbsSubject,AtDate,IsTop,TopDays,UserId)
-- values('Bbs Subjects 3','2025-06-01',1,10,'e9d49073-853f-f011-89a6-18c04d10aecf')
-- go

-- insert into Va2Bulletin(BbsSubject,AtDate,IsTop,TopDays,UserId)
-- values('Bbs Subjects 4','2025-06-02',0,0,'e9d49073-853f-f011-89a6-18c04d10aecf')
-- go

-- */
