
-----------------------------------------------------------------------------------------------------------------------
/*
Drop Table Pt2ForumReply
go
Drop Table Pt2Forum
go
*/
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2Forum
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	ForumId    UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	GroupType  NVARCHAR(50) NOT NULL, --1:鄰居 2:我的團隊 3:我的團隊長
	Subject    NVARCHAR(200) NOT NULL,
	Contents   NVARCHAR(4000) NOT NULL,--內容
	Contents2  NVARCHAR(4000) NULL,--內容
	Contents3  NVARCHAR(4000) NULL,--內容

    IsTop      Bit NOT NULL DEFAULT 0, --是否置頂
	TopDays    Int DEFAULT 0, --置頂天數

	-- CustomerId NVARCHAR(100) NOT NULL,	--客戶代號,村里代號
	UserId     UniqueIdentifier NOT NULL,
	CreateTime DateTime NOT NULL DEFAULT GetDate(),
    IsDelete   Bit NOT NULL DEFAULT 0,

	ReadTimes INT NOT NULL DEFAULT 0,
    -- IsOnOff    Bit NOT NULL DEFAULT 0, -- 是否啟用
    -- LastTime   DateTime NOT NULL DEFAULT GetDate(), --最後更新時間

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Pt2Forum_PrimaryKey PRIMARY KEY CLUSTERED(ForumId),
	CONSTRAINT Pt2Forum_Au1User FOREIGN KEY (UserId) REFERENCES Au1User(UserId) -- ON UPDATE CASCADE ON DELETE CASCADE
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Va2Mas10Image_AutoId ON Va2Mas10Image(AutoId);
--go
CREATE INDEX Pt2Forum_UserId ON Pt2Forum(UserId,CreateTime);
Go
CREATE INDEX Pt2Forum_GroupType ON Pt2Forum(GroupType,CreateTime);
Go

-- DROP TRIGGER Pt2Forum_TriggerLog
go
CREATE TRIGGER Pt2Forum_TriggerLog ON Pt2Forum AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Pt2Forum';

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
--
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2ForumReply
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	ReplyId    UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	ParentId   UniqueIdentifier NOT NULL,
	ForumId    UniqueIdentifier NOT NULL,
	Contents   NVARCHAR(4000) ,

	-- CustomerId NVARCHAR(100) NOT NULL,	--客戶代號,村里代號
	UserId     UniqueIdentifier NOT NULL,
	CreateTime DateTime NOT NULL DEFAULT GetDate(),
    IsDelete   Bit NOT NULL DEFAULT 0,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Pt2ForumReply_PrimaryKey PRIMARY KEY CLUSTERED(ReplyId),
	CONSTRAINT Pt2ForumReply_Pt2Forum FOREIGN KEY (ForumId) REFERENCES Pt2Forum(ForumId), -- ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT Pt2ForumReply_Au1User FOREIGN KEY (UserId) REFERENCES Au1User(UserId) -- ON UPDATE CASCADE ON DELETE CASCADE
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Pt2ForumReply_AutoId ON Pt2ForumReply(AutoId);
--go
CREATE INDEX Pt2ForumReply_UserId ON Pt2ForumReply(UserId,ForumId);
Go
CREATE INDEX Pt2ForumReply_ForumId ON Pt2ForumReply(ForumId,UserId);
Go

-- DROP TRIGGER Pt2Forum_TriggerLog
go
CREATE TRIGGER Pt2ForumReply_TriggerLog ON Pt2ForumReply AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Pt2ForumReply';

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