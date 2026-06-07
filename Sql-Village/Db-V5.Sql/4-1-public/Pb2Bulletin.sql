-----------------------------------------------------------------------------------------------------------------------
-- 公告欄資料主檔
/*
DROP TABLE Pb2BulletinItem;
go
DROP TABLE Pb2Bulletin;

go
*/
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pb2Bulletin(
 	--BbsId           int IDENTITY(1,1) NOT NULL,
	BbsId      UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Subject    NVARCHAR(200) NOT NULL, --公告主題
	AtDate     Date NOT NULL DEFAULT GetDate(), --公告日期
	DocNo      NVARCHAR(200) null, --公告文號
	SpeedType  NVARCHAR(200) null, --公告速別
	SecretType NVARCHAR(200) null, --密等級別
	Recipient  NVARCHAR(200) null, --公告收件人
	Secondary  NVARCHAR(200) null, --副本收件人
    PdfFileUrl NVARCHAR(200) , -- 公告本文正件PDF

    IsTop      Bit NOT NULL DEFAULT 0, --是否置頂
	TopDays    Int DEFAULT 0, --置頂天數
    IsDelete   Bit NOT NULL DEFAULT 0,

	-- CustomerId NVARCHAR(100) NOT NULL,	--客戶代號,村里代號
	UserId     UniqueIdentifier NOT NULL,
	CreateTime DateTime NOT NULL DEFAULT GetDate(),

	ReadTimes INT NOT NULL DEFAULT 0,
	LikeTimes INT NOT NULL DEFAULT 0,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Pb2Bulletin_PrimaryKey PRIMARY KEY CLUSTERED (BbsId),
    CONSTRAINT Pb2Bulletin_Au1User FOREIGN KEY (UserId) REFERENCES Au1User(UserId)
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Pb2Bulletin_AutoId ON Pb2Bulletin(AutoId);
-- go
CREATE INDEX Pb2Bulletin_Subject  ON Pb2Bulletin(Subject,AtDate);
GO


-- DROP TRIGGER Pb2Bulletin_TriggerLog
-- go
CREATE TRIGGER Pb2Bulletin_TriggerLog ON Pb2Bulletin AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(50);

End
GO

Go

-----------------------------------------------------------------------------------------------------------------------
-- DROP TABLE Pb2BulletinItem; -- 公告欄資料子檔
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pb2BulletinItem(
 	Id              int IDENTITY(1,1) NOT NULL,
 	BbsId           UniqueIdentifier NOT NULL,
 	SortOrder       int NOT NULL,
	DocOrder        nvarchar(30) NOT NULL , --公告項目序號
	Contents        nvarchar(4000) NOT NULL , --公告項目說明
	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Pb2BulletinItem_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT Pb2BulletinItem_Pb2Bulletin FOREIGN KEY (BbsId) REFERENCES Pb2Bulletin (BbsId) ON UPDATE CASCADE ON DELETE CASCADE,
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Pb2BulletinItem_AutoId ON Pb2BulletinItem(AutoId);
-- go
CREATE INDEX Pb2BulletinItem_BbsId ON Pb2BulletinItem(BbsId);
GO


-- DROP TRIGGER Pb2BulletinItem_TriggerLog
go
CREATE TRIGGER Pb2BulletinItem_TriggerLog ON Pb2BulletinItem AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(50);

End
GO
