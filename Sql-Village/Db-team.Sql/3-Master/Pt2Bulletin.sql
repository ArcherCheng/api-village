-----------------------------------------------------------------------------------------------------------------------
-- 公告欄資料主檔
/*
DROP TABLE Pt2BulletinItem;
go
DROP TABLE Pt2Bulletin;

go
*/
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2Bulletin(
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
	CONSTRAINT Pt2Bulletin_PrimaryKey PRIMARY KEY CLUSTERED (BbsId),
    CONSTRAINT Pt2Bulletin_Au1User FOREIGN KEY (UserId) REFERENCES Au1User(UserId)
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Pt2Bulletin_AutoId ON Pt2Bulletin(AutoId);
-- go
CREATE INDEX Pt2Bulletin_Subject  ON Pt2Bulletin(Subject,AtDate);
GO


-- DROP TRIGGER Pt2Bulletin_TriggerLog
-- go
CREATE TRIGGER Pt2Bulletin_TriggerLog ON Pt2Bulletin AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Pt2Bulletin';

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
-- DROP TABLE Pt2BulletinItem; -- 公告欄資料子檔
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2BulletinItem(
 	Id              int IDENTITY(1,1) NOT NULL,
 	BbsId           UniqueIdentifier NOT NULL,
 	SortOrder       int NOT NULL,
	DocOrder        nvarchar(30) NOT NULL , --公告項目序號
	Contents        nvarchar(4000) NOT NULL , --公告項目說明

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Pt2BulletinItem_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT Pt2BulletinItem_Pt2Bulletin FOREIGN KEY (BbsId) REFERENCES Pt2Bulletin (BbsId) ON UPDATE CASCADE ON DELETE CASCADE,
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Pt2BulletinItem_AutoId ON Pt2BulletinItem(AutoId);
-- go
CREATE INDEX Pt2BulletinItem_BbsId ON Pt2BulletinItem(BbsId);
GO


-- DROP TRIGGER Pt2BulletinItem_TriggerLog
go
CREATE TRIGGER Pt2BulletinItem_TriggerLog ON Pt2BulletinItem AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(50);
	SET @tableName='Pt2BulletinItem';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
		SET @writeType = 1;    -- Insert
	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		SET @writeType = 2;    -- Update
	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		SET @writeType = 3;    -- Delete

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select * From Inserted For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select * From Deleted For Json Auto),1,4000);

	Insert Into AppLogTable(TableName,InsertData,DeleteData,WriteType) Values(@tableName,@InsertData,@DeleteData,@writeType);
End
GO
