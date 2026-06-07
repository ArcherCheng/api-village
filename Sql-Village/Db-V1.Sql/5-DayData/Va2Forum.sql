
-- -----------------------------------------------------------------------------------------------------------------------
-- /*
-- Drop Table Va2ForumReply
-- go
-- Drop Table Va2Forum
-- go
-- */
-- -----------------------------------------------------------------------------------------------------------------------
-- CREATE TABLE Va2Forum
-- (
-- 	--AutoId         int IDENTITY(1,1) NOT NULL,
-- 	ForumId    UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
-- 	TeamId    NVARCHAR(50) NOT NULL,	--客戶代號,村里代號
-- 	UserId      UniqueIdentifier NOT NULL,

-- 	GroupType  NVARCHAR(50) NOT NULL, --1:鄰居 2:我的團隊 3:我的團隊長
-- 	Subjects   NVARCHAR(200) NOT NULL,
-- 	Contents   NVARCHAR(4000) NOT NULL,--內容
-- 	Contents2  NVARCHAR(4000) NULL,--內容
-- 	Contents3  NVARCHAR(4000) NULL,--內容

--     IsTop      Bit NOT NULL DEFAULT 0, --是否置頂
-- 	TopDays    Int DEFAULT 0, --置頂天數

-- 	-- CustomerId NVARCHAR(100) NOT NULL,	--客戶代號,村里代號
-- 	UserId     UniqueIdentifier NOT NULL,
-- 	CreateTime DateTime NOT NULL DEFAULT GetDate(),
--     IsDelete   Bit NOT NULL DEFAULT 0,

-- 	ReadTimes INT NOT NULL DEFAULT 0,
--     -- IsOnOff    Bit NOT NULL DEFAULT 0, -- 是否啟用
--     -- LastTime   DateTime NOT NULL DEFAULT GetDate(), --最後更新時間

-- 	-- 以下每檔資料表都會有這些欄位
-- 	WriteInfo NVARCHAR(100),
-- 	CONSTRAINT Va2Forum_PrimaryKey PRIMARY KEY CLUSTERED(ForumId),
-- 	CONSTRAINT Va2Forum_AppUser FOREIGN KEY (UserId) REFERENCES AppUser(UserId) -- ON UPDATE CASCADE ON DELETE CASCADE
-- );
-- Go

-- --建立索引檔
-- --CREATE UNIQUE CLUSTERED INDEX Va2Mas10Image_AutoId ON Va2Mas10Image(AutoId);
-- --go
-- CREATE INDEX Va2Forum_UserId ON Va2Forum(UserId,CreateTime);
-- Go
-- CREATE INDEX Va2Forum_GroupType ON Va2Forum(GroupType,CreateTime);
-- Go

-- -- DROP TRIGGER Va2Forum_TriggerLog
-- go
-- CREATE TRIGGER Va2Forum_TriggerLog ON Va2Forum AFTER UPDATE,DELETE NOT FOR REPLICATION AS
-- Begin
-- 	DECLARE @tableName NVARCHAR(100);
-- 	SET @tableName='Va2Forum';

-- 	DECLARE @writeType Tinyint;
-- 	SET @writeType=0;

-- 	IF EXISTS(Select 1
-- 		From Inserted) AND NOT EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 1; -- Insert
-- 	ELSE IF EXISTS(Select 1
-- 		From Inserted) AND EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 2; -- Update
-- 	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 3;	-- Delete

-- 	DECLARE @InsertData NVARCHAR(4000);
-- 	DECLARE @DeleteData NVARCHAR(4000);

-- 	SET @InsertData=SUBSTRING((Select *	From Inserted For Json Auto),1,4000);
-- 	SET @DeleteData=SUBSTRING((Select *	From Deleted For Json Auto),1,4000);

-- 	Insert Into AppLogTable	(TableName,InsertData,DeleteData,WriteType)
-- 	Values(@tableName, @InsertData, @DeleteData, @writeType);
-- End
-- Go

-- -----------------------------------------------------------------------------------------------------------------------
-- --
-- -----------------------------------------------------------------------------------------------------------------------
-- CREATE TABLE Va2ForumReply
-- (
-- 	--AutoId         int IDENTITY(1,1) NOT NULL,
-- 	ReplyId    UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
-- 	ParentId   UniqueIdentifier NOT NULL,
-- 	ForumId    UniqueIdentifier NOT NULL,
-- 	Contents   NVARCHAR(4000) ,
--     IsDelete   Bit NOT NULL DEFAULT 0,

-- 	-- TeamId NVARCHAR(50) NOT NULL,	--客戶代號,村里代號
-- 	UserId      UniqueIdentifier NOT NULL,
-- 	CreateTime  DateTime NOT NULL DEFAULT GetDate(),
-- 	UpdateTime  DateTime NOT NULL DEFAULT GetDate(),

-- 	ReadTimes   INT NOT NULL DEFAULT 0,
-- 	-- LikeCounts  INT NOT NULL DEFAULT 0,
-- 	-- UnLikeCountsINT NOT NULL DEFAULT 0,

-- 	-- 以下每檔資料表都會有這些欄位
-- 	WriteInfo NVARCHAR(100),
-- 	CONSTRAINT Va2ForumReply_PrimaryKey PRIMARY KEY CLUSTERED(ReplyId),
-- 	CONSTRAINT Va2ForumReply_Va2Forum FOREIGN KEY (ForumId) REFERENCES Va2Forum(ForumId), -- ON UPDATE CASCADE ON DELETE CASCADE,
-- 	CONSTRAINT Va2ForumReply_AppUser FOREIGN KEY (UserId) REFERENCES AppUser(UserId) -- ON UPDATE CASCADE ON DELETE CASCADE
-- );
-- Go

-- --建立索引檔
-- --CREATE UNIQUE CLUSTERED INDEX Va2ForumReply_AutoId ON Va2ForumReply(AutoId);
-- --go
-- CREATE INDEX Va2ForumReply_UserId ON Va2ForumReply(UserId,ForumId);
-- Go
-- CREATE INDEX Va2ForumReply_ForumId ON Va2ForumReply(ForumId,UserId);
-- Go

-- -- DROP TRIGGER Va2Forum_TriggerLog
-- go
-- CREATE TRIGGER Va2ForumReply_TriggerLog ON Va2ForumReply AFTER UPDATE,DELETE NOT FOR REPLICATION AS
-- Begin
-- 	DECLARE @tableName NVARCHAR(100);
-- 	SET @tableName='Va2ForumReply';

-- 	DECLARE @writeType Tinyint;
-- 	SET @writeType=0;

-- 	IF EXISTS(Select 1
-- 		From Inserted) AND NOT EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 1; -- Insert
-- 	ELSE IF EXISTS(Select 1
-- 		From Inserted) AND EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 2; -- Update
-- 	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 3;	-- Delete

-- 	DECLARE @InsertData NVARCHAR(4000);
-- 	DECLARE @DeleteData NVARCHAR(4000);

-- 	SET @InsertData=SUBSTRING((Select *	From Inserted For Json Auto),1,4000);
-- 	SET @DeleteData=SUBSTRING((Select *	From Deleted For Json Auto),1,4000);

-- 	Insert Into AppLogTable	(TableName,InsertData,DeleteData,WriteType)
-- 	Values(@tableName, @InsertData, @DeleteData, @writeType);
-- End
-- Go