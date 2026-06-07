

-----------------------------------------------------------------------------------------------------------------------
-- 報修資料主檔
/*
DROP TABLE Tm4RepairReply;
go
DROP TABLE Tm4Repair;
go

*/
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Tm4Repair(
	RepairId    UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Title       NVARCHAR(200) NOT NULL, --報修事項
	Category    NVARCHAR(50) NOT NULL, --報修類別
	Content     NVARCHAR(4000) NULL, --報修描述

	Arrdess     NVARCHAR(200) NULL, --報修地址
    Latitude    FLOAT NULL,
    Longitude   FLOAT NULL,
	ImageUrl    NVARCHAR(500) NULL, --報修附件URL

	Source      NVARCHAR(50) NULL, --報修來源,Line,電話,APP等
	AtDate      Date NOT NULL DEFAULT GetDate(), --報修日期
	IsTop       BIT DEFAULT 0, --是否置頂
	TopDays     Int DEFAULT 0, --置頂天數
	Status	    NVARCHAR(50) NOT NULL, --報修狀態
	Priority	NVARCHAR(50) NOT NULL, --報修優先順序
	-- SpeedType   NVARCHAR(50) NOT NULL, --報修速別
	-- Urgency	    NVARCHAR(50) NOT NULL, --報修緊急程度

	TeamId       NVARCHAR(100) NOT NULL,--村里代號
	UserId       UniqueIdentifier,
    CitizenName  NVARCHAR(100) NULL,
    CitizenPhone NVARCHAR(100) NULL,
    CitizenLineUserId NVARCHAR(100) NULL,

	AiSummary NVARCHAR(500) NULL,
	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),

	-- ReadTimes  AS dbo.sumReadTimes(RepairId), --讀取次數
	-- LikeStars   AS dbo.avgLikeStars(RepairId), --喜歡分數
	-- SortDate   AS DATEADD(DAY,TopDays,AtDate), --排序用日期=報修日期+置頂天數

	CONSTRAINT Tm4Repair_PrimaryKey PRIMARY KEY CLUSTERED (RepairId),
	CONSTRAINT Tm4Repair_ref_Au1Team FOREIGN KEY (TeamId) REFERENCES Au1Team (TeamId) ON UPDATE CASCADE ON DELETE NO ACTION,
	CONSTRAINT Tm4Repair_ref_Au1User FOREIGN KEY (UserId) REFERENCES Au1User (UserId) ON UPDATE CASCADE ON DELETE NO ACTION
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Tm4Repair_AutoId ON Tm4Repair(AutoId);
-- go
CREATE INDEX Inx_TeamId ON Tm4Repair(TeamId);
GO

CREATE INDEX Inx_UserId ON Tm4Repair(UserId);
GO

CREATE INDEX Inx_Category ON Tm4Repair(Category);
GO

-- DROP TRIGGER Tm4Repair_Trigger1
-- go
CREATE TRIGGER Tm4Repair_Trigger1 ON Tm4Repair AFTER UPDATE,DELETE NOT FOR REPLICATION AS
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
			Select @tableKey=Convert(NVARCHAR(100),RepairId) From INSERTED;
		END
	ELSE IF (@insertRows=1) AND (@deleteRows=1)
		Begin	-- Update
			SET @writeType = 2;
			Select @tableKey=Convert(NVARCHAR(100),RepairId) From INSERTED;
		END
	ELSE IF (@insertRows=0) AND (@deleteRows=1)
		Begin	-- Delete
			SET @writeType = 3;
			Select @tableKey=Convert(NVARCHAR(100),RepairId) From Deleted;
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
	Values('Tm4Repair',@tableKey,@writeType,@newData,@oldData);
End
Go

-----------------------------------------------------------------------------------------------------------------------
-- DROP TABLE Tm4RepairReply; -- 報修資料主檔
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Tm4RepairReply(
	ReplyId    UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	RepairId   UniqueIdentifier NOT NULL,
    ReplyType  INT NOT NULL DEFAULT 0, --1:收到報修 2:回報進度 3:報修結案
	Content    NVARCHAR(4000) NOT NULL, --報修事項
	UserId     UniqueIdentifier,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Tm4RepairReply_PrimaryKey PRIMARY KEY CLUSTERED (ReplyId),
	CONSTRAINT Tm4RepairReply_ref_Tm4Repair FOREIGN KEY (RepairId) REFERENCES Tm4Repair (RepairId) ON UPDATE CASCADE ON DELETE CASCADE,
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Tm4RepairReply_AutoId ON Tm4RepairReply(AutoId);
-- go
CREATE INDEX inx_RepairId  ON Tm4RepairReply(RepairId);
GO


-- DROP TRIGGER Tm4RepairReply_Trigger1
-- go
CREATE TRIGGER Tm4RepairReply_Trigger1 ON Tm4RepairReply AFTER UPDATE,DELETE NOT FOR REPLICATION AS
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
	Values('Tm4RepairReply',@tableKey,@writeType,@newData,@oldData);
End
Go