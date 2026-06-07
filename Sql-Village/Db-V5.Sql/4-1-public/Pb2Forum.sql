
-----------------------------------------------------------------------------------------------------------------------
/*
Drop Table Pb2ForumReply
go
Drop Table Pb2Forum
go
*/
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pb2Forum
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	ForumId    UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Title       NVARCHAR(200) NOT NULL, --陳情事項
	Category    NVARCHAR(50) NOT NULL, --陳情類別
	Content     NVARCHAR(4000) NULL, --陳情描述

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
	CONSTRAINT Pb2Forum_PrimaryKey PRIMARY KEY CLUSTERED(ForumId),
	CONSTRAINT Pb2Forum_Au1User FOREIGN KEY (UserId) REFERENCES Au1User(UserId) -- ON UPDATE CASCADE ON DELETE CASCADE
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Va2Mas10Image_AutoId ON Va2Mas10Image(AutoId);
--go
CREATE INDEX Pb2Forum_UserId ON Pb2Forum(UserId,CreateTime);
Go
CREATE INDEX Pb2Forum_Category ON Pb2Forum(Category,CreateTime);
Go
CREATE INDEX Pb2Forum_Title ON Pb2Forum(Title,CreateTime);
Go


-- DROP TRIGGER Pb2Forum_TriggerLog
go
CREATE TRIGGER Pb2Forum_TriggerLog ON Pb2Forum AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);

End
Go

-----------------------------------------------------------------------------------------------------------------------
-- Drop TABLE Pb2ForumReply
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pb2ForumReply
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
	CONSTRAINT Pb2ForumReply_PrimaryKey PRIMARY KEY CLUSTERED(ReplyId),
	CONSTRAINT Pb2ForumReply_Pb2Forum FOREIGN KEY (ForumId) REFERENCES Pb2Forum(ForumId), -- ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT Pb2ForumReply_Au1User FOREIGN KEY (UserId) REFERENCES Au1User(UserId) -- ON UPDATE CASCADE ON DELETE CASCADE
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Pb2ForumReply_AutoId ON Pb2ForumReply(AutoId);
--go
CREATE INDEX Pb2ForumReply_UserId ON Pb2ForumReply(UserId,ForumId);
Go
CREATE INDEX Pb2ForumReply_ForumId ON Pb2ForumReply(ForumId,UserId);
Go

-- DROP TRIGGER Pb2Forum_TriggerLog
go
CREATE TRIGGER Pb2ForumReply_TriggerLog ON Pb2ForumReply AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);

End
Go