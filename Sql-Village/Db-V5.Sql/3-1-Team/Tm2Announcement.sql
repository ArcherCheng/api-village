
-----------------------------------------------------------------------------------------------------------------------
-- 公告資料主檔
/*

DROP TABLE Tm2Announcement;
go


*/
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Tm2Announcement(
	AnnounceId   UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Title        NVARCHAR(200) NOT NULL, --公告主題
    Content      NVARCHAR(4000) NULL, --公告描述
	Category     NVARCHAR(50) , --公告類別
    Priority     NVARCHAR(50) , --公告優先順序
	IsTop        BIT DEFAULT 0, --是否置頂
	TopDays      Int DEFAULT 0, --置頂天數

    AttachmentUrl NVARCHAR(500) NULL, --公告附件URL

    AtDate       DateTime NOT NULL DEFAULT GetDate(), --公告日期
    Status	     NVARCHAR(50) NOT NULL, --公告狀態
    TeamId       NVARCHAR(100) NOT NULL,--村里代號
    UserId       UniqueIdentifier,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),

	CONSTRAINT Tm2Announcement_PrimaryKey PRIMARY KEY CLUSTERED (AnnounceId),
	CONSTRAINT Tm2Announcement_ref_Au1User FOREIGN KEY (UserId) REFERENCES Au1User (UserId) ON UPDATE CASCADE ON DELETE NO ACTION,
	CONSTRAINT Tm2Announcement_ref_Au1Team FOREIGN KEY (TeamId) REFERENCES Au1Team (TeamId) ON UPDATE CASCADE ON DELETE NO ACTION
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Tm2Announcement_AutoId ON Tm2Announcement(AutoId);
-- go
CREATE INDEX Inx_TeamId ON Tm2Announcement(TeamId);
GO
CREATE INDEX Inx_UserId ON Tm2Announcement(UserId);
GO
CREATE INDEX Inx_Category ON Tm2Announcement(Category);
GO


-- DROP TRIGGER Tm2Announcement_Trigger1
-- go
CREATE TRIGGER Tm2Announcement_Trigger1 ON Tm2Announcement AFTER UPDATE,DELETE NOT FOR REPLICATION AS
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
			Select @tableKey=Convert(NVARCHAR(100),AnnounceId) From INSERTED;
		END
	ELSE IF (@insertRows=1) AND (@deleteRows=1)
		Begin	-- Update
			SET @writeType = 2;
			Select @tableKey=Convert(NVARCHAR(100),AnnounceId) From INSERTED;
		END
	ELSE IF (@insertRows=0) AND (@deleteRows=1)
		Begin	-- Delete
			SET @writeType = 3;
			Select @tableKey=Convert(NVARCHAR(100),AnnounceId) From Deleted;
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
	Values('Tm2Announcement',@tableKey,@writeType,@newData,@oldData);
End
Go

