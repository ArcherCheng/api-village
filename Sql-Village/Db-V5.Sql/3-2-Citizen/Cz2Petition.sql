


-----------------------------------------------------------------------------------------------------------------------
-- 陳情資料主檔
/*
DROP TABLE Cz2PetitionReply;
go
DROP TABLE Cz2Petition;
go

*/
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Cz2Petition(
	PetitionId UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Title       NVARCHAR(200) NOT NULL, --陳情事項
	Category    NVARCHAR(50) NOT NULL, --陳情類別
	Content     NVARCHAR(4000) NULL, --陳情描述

	IsTop       BIT DEFAULT 0, --是否置頂
	TopDays     Int DEFAULT 0, --置頂天數
	Status	    NVARCHAR(50) NOT NULL, --陳情狀態
	Priority	NVARCHAR(50) NOT NULL, --陳情優先順序
	CreateDate  Date NOT NULL DEFAULT GetDate(), --陳情日期
	UpadteDate  Date NOT NULL DEFAULT GetDate(), --陳情日期

	TeamId       NVARCHAR(100) NOT NULL,--村里代號
	UserId       UniqueIdentifier,
    CitizenName  NVARCHAR(100) NULL,
    CitizenPhone NVARCHAR(100) NULL,
    CitizenLineUserId NVARCHAR(100) NULL,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),

	CONSTRAINT Cz2Petition_PrimaryKey PRIMARY KEY CLUSTERED (PetitionId),
	CONSTRAINT Cz2Petition_ref_Au1Team FOREIGN KEY (TeamId) REFERENCES Au1Team (TeamId) ON UPDATE CASCADE ON DELETE NO ACTION,
	CONSTRAINT Cz2Petition_ref_Au1User FOREIGN KEY (UserId) REFERENCES Au1User (UserId) ON UPDATE CASCADE ON DELETE NO ACTION
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Cz2Petition_AutoId ON Cz2Petition(AutoId);
-- go
CREATE INDEX Inx_TeamId ON Cz2Petition(TeamId);
GO

CREATE INDEX Inx_UserId ON Cz2Petition(UserId);
GO

CREATE INDEX Inx_Category ON Cz2Petition(Category);
GO

-- DROP TRIGGER Cz2Petition_Trigger1
-- go
CREATE TRIGGER Cz2Petition_Trigger1 ON Cz2Petition AFTER UPDATE,DELETE NOT FOR REPLICATION AS
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
			Select @tableKey=Convert(NVARCHAR(100),PetitionId) From INSERTED;
		END
	ELSE IF (@insertRows=1) AND (@deleteRows=1)
		Begin	-- Update
			SET @writeType = 2;
			Select @tableKey=Convert(NVARCHAR(100),PetitionId) From INSERTED;
		END
	ELSE IF (@insertRows=0) AND (@deleteRows=1)
		Begin	-- Delete
			SET @writeType = 3;
			Select @tableKey=Convert(NVARCHAR(100),PetitionId) From Deleted;
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
	Values('Cz2Petition',@tableKey,@writeType,@newData,@oldData);
End
Go

-----------------------------------------------------------------------------------------------------------------------
-- DROP TABLE Cz2PetitionReply; -- 陳情資料主檔
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Cz2PetitionReply(
	ReplyId    UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	PetitionId   UniqueIdentifier NOT NULL,
    ReplyType  INT NOT NULL DEFAULT 0, --1:收到陳情 2:回報進度 3:陳情結案
	Content    NVARCHAR(4000) NOT NULL, --陳情事項
	UserId     UniqueIdentifier,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Cz2PetitionReply_PrimaryKey PRIMARY KEY CLUSTERED (ReplyId),
	CONSTRAINT Cz2PetitionReply_ref_Cz2Petition FOREIGN KEY (PetitionId) REFERENCES Cz2Petition (PetitionId) ON UPDATE CASCADE ON DELETE CASCADE,
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Cz2PetitionReply_AutoId ON Cz2PetitionReply(AutoId);
-- go
CREATE INDEX inx_PetitionId  ON Cz2PetitionReply(PetitionId);
GO


-- DROP TRIGGER Cz2PetitionReply_Trigger1
-- go
CREATE TRIGGER Cz2PetitionReply_Trigger1 ON Cz2PetitionReply AFTER UPDATE,DELETE NOT FOR REPLICATION AS
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
			Select @tableKey=Convert(NVARCHAR(100),ReplyId) From INSERTED;
		END
	ELSE IF (@insertRows=1) AND (@deleteRows=1)
		Begin	-- Update
			SET @writeType = 2;
			Select @tableKey=Convert(NVARCHAR(100),ReplyId) From INSERTED;
		END
	ELSE IF (@insertRows=0) AND (@deleteRows=1)
		Begin	-- Delete
			SET @writeType = 3;
			Select @tableKey=Convert(NVARCHAR(100),ReplyId) From Deleted;
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
	Values('Cz2PetitionReply',@tableKey,@writeType,@newData,@oldData);
End
Go