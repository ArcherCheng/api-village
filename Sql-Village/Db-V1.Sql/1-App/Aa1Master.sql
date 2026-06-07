
--------------------------------------------------------------------
/*
Drop table Aa2Like
go
Drop table Aa2Image
go
Drop table Aa1Master
go

*/
--------------------------------------------------------------------
CREATE TABLE Aa1Master
(
	--AutoId      int IDENTITY(1,1) NOT NULL,
	--TeamId     NVARCHAR(50) NOT NULL,	--客戶代號,村里代號
	TeamId     NVARCHAR(50) NOT NULL,	--客戶代號,村里代號
	MasterName   NVARCHAR(100) NOT NULL,	--客戶姓名，可由MemberData寫回
	MobileTel    NVARCHAR(100) NOT NULL,	-- 行動電話
	Email        NVARCHAR(100) null,	-- 電子郵件
	Telephone    NVARCHAR(100) null,	-- 電話
	Fax          NVARCHAR(100) null,	-- 傳真
	MonthAmt    INT NOT NULL DEFAULT 0,	-- 每月金額
	YearAmt     INT NOT NULL DEFAULT 0,	-- 年度金額
	BeginDate    date null DEFAULT GetDate()  ,--啟用日期
	EndDate      date null DEFAULT GetDate()  ,--到期日期
    Notes        NVARCHAR(200) null,	-- 備註說明

	--是否啟用 有效用戶
	IsOnOff     Bit NOT NULL DEFAULT 0,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Aa1Master_PrimaryKey PRIMARY KEY CLUSTERED (TeamId),
);
Go

--CREATE UNIQUE CLUSTERED INDEX Aa1Master_AutoId ON Aa1Master(AutoId);
go
CREATE INDEX Aa1Master_MobileTel ON Aa1Master (MobileTel asc) --WHERE MobileTel Is NOT NULL;
go
CREATE INDEX Aa1Master_Email ON Aa1Master (Email asc) --Where Email Is NOT NULL;
go

-- IF EXISTS (SELECT * FROM sys.TRIGGERS WHERE name = 'Aa1Master_TriggerLog')
-- BEGIN
-- 	DROP TRIGGER Aa1Master_TriggerLog;
-- END

-- DROP TRIGGER Aa1Master_TriggerLog
-- go

CREATE TRIGGER Aa1Master_TriggerLog ON Aa1Master AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Aa1Master';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

 	DECLARE @TeamId NVARCHAR(50);

	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
		BEGIN
			SET @writeType = 1;    -- Insert
			Select @TeamId=TeamId From Inserted;
		END
	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		begin
			SET @writeType = 2;    -- Update
			Select @TeamId=TeamId From Inserted;
		end
	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		begin
 			SET @writeType = 3;    -- Delete
			Select @TeamId=TeamId From Deleted;
		end

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select * From Inserted For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select * From Deleted For Json Auto),1,4000);

	Insert Into AppLogTable(TableName,TeamId,InsertData,DeleteData,WriteType)
	Values(@tableName,@TeamId,@InsertData,@DeleteData,@writeType);
end
go

insert into Aa1Master (TeamId,MasterName,MobileTel,Email,MonthAmt,YearAmt,BeginDate,EndDate,Notes,IsOnOff,WriteInfo)
values ('NullTeamId','非會員用戶','0970922888','a0970922888@gmail.com',1000,10000,'2025-01-01','9999-12-31','備註說明',1,'自動新增');
go

insert into Aa1Master (TeamId,MasterName,MobileTel,Email,MonthAmt,YearAmt,BeginDate,EndDate,Notes,IsOnOff,WriteInfo)
values ('Master0001','測試會員1','0912345678','a012345678@b.com',1000,10000,'2025-01-01','2026-12-31','備註說明',1,'自動新增');
go

insert into Aa1Master (TeamId,MasterName,MobileTel,Email,MonthAmt,YearAmt,BeginDate,EndDate,Notes,IsOnOff,WriteInfo)
values ('Master0002','測試會員2','0912345678','a012345678@b.com',1000,10000,'2025-01-01','2026-12-31','備註說明',1,'自動新增');
go

insert into Aa1Master (TeamId,MasterName,MobileTel,Email,MonthAmt,YearAmt,BeginDate,EndDate,Notes,IsOnOff,WriteInfo)
values ('Master0003','測試會員3','0912345678','a012345678@b.com',1000,10000,'2025-01-01','2026-12-31','備註說明',1,'自動新增');
go

insert into Aa1Master (TeamId,MasterName,MobileTel,Email,MonthAmt,YearAmt,BeginDate,EndDate,Notes,IsOnOff,WriteInfo)
values ('0970922888','archer','0970922888','a0970922888@b.com',1000,10000,'2025-01-01','2026-12-31','備註說明',1,'自動新增');
go

insert into Aa1Master (TeamId,MasterName,MobileTel,Email,MonthAmt,YearAmt,BeginDate,EndDate,Notes,IsOnOff,WriteInfo)
values ('0931388546','rebaca','0931388546','a0931388546@b.com',1000,10000,'2025-01-01','2026-12-31','備註說明',1,'自動新增');
go

select * from Aa1Master
go

-----------------------------------------------------------------------------------------------------------------------
-- 圖片資料檔
/*
DROP TABLE Aa2Image;
go
DROP TABLE Aa2Like;
go
*/
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Aa2Image
(
	Id         int IDENTITY(1,1) NOT NULL,
	TeamId   NVARCHAR(50) NOT NULL,	--客戶代號,村里代號
	FromTable  NVARCHAR(100) NOT NULL,
	FromId     UniqueIdentifier NOT NULL,
	ImageOrderINT NOT NULL DEFAULT 0,
	ImageUrl   NVARCHAR(500),
	ImageNotes nvarchar(500),
	-- ImageBase64 VARCHAR(max),  --must use varchar(max), not NVARCHAR(4000)
	-- ImageImage image,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Aa2Image_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	-- CONSTRAINT Aa2Image_Aa1Master FOREIGN KEY (TeamId) REFERENCES Aa1Master(TeamId) ON UPDATE CASCADE ON DELETE CASCADE
);
GO


-- 建立關連檔案
--Alter Table Aa2Image Add CONSTRAINT Aa2Image_TeamId
--	FOREIGN KEY (TeamId)
--	REFERENCES Aa1Master(TeamId)
--	ON UPDATE CASCADE --NO ACTION  -- Set Null
--	ON DELETE CASCADE --NO ACTION  -- Set Null
--Go

-- CREATE UNIQUE CLUSTERED INDEX Aa2Image_AutoId ON Aa2Image(AutoId);
-- go
CREATE INDEX Aa2Image_FromTable ON Aa2Image(FromTable,FromId);
GO
CREATE INDEX Aa2Image_FromId ON Aa2Image(FromId);
GO

-- DROP TRIGGER Aa2Image_TriggerLog
-- go

CREATE TRIGGER Aa2Image_TriggerLog ON Aa2Image AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Aa2Image';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

 	DECLARE @TeamId NVARCHAR(50);

	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
		BEGIN
			SET @writeType = 1;    -- Insert
			Select @TeamId=TeamId From Inserted;
		END
	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		begin
			SET @writeType = 2;    -- Update
			Select @TeamId=TeamId From Inserted;
		end
	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		begin
 			SET @writeType = 3;    -- Delete
			Select @TeamId=TeamId From Deleted;
		end

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select * From Inserted For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select * From Deleted For Json Auto),1,4000);

	Insert Into AppLogTable(TableName,TeamId,InsertData,DeleteData,WriteType)
	Values(@tableName,@TeamId,@InsertData,@DeleteData,@writeType);
end
go

-----------------------------------------------------------------------------------------------------------------------
-- Like資料檔，是否按讚，一定要先登入
-- DROP TABLE Aa2Like;
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Aa2Like
(
	Id         int IDENTITY(1,1) NOT NULL,
	TeamId   NVARCHAR(50) NOT NULL,	--客戶代號,村里代號
	UserId     UniqueIdentifier NOT NULL,
	FromTable  NVARCHAR(100) NOT NULL,
	FromId     UniqueIdentifier NOT NULL,
	LikeType   int NOT NULL DEFAULT 1,  -- 1=按讚支持,2=不支持
	-- IsLike     bit NOT NULL DEFAULT 1,  -- 是否按讚

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Aa2Like_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	-- CONSTRAINT Aa2Like_Au1User FOREIGN KEY (UserId) REFERENCES Au1User(TeamId) ON UPDATE CASCADE ON DELETE CASCADE,
	-- CONSTRAINT Aa2Like_Aa1Master FOREIGN KEY (TeamId) REFERENCES Aa1Master(TeamId) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Aa2Like_AutoId ON Aa2Like(AutoId);
-- go
CREATE UNIQUE INDEX Aa2Like_FromId ON Aa2Like(FromId,UserId);
GO
CREATE INDEX Aa2Like_FromTable ON Aa2Like(TeamId,FromTable);
GO


-- DROP TRIGGER Aa2Like_TriggerLog
-- go

CREATE TRIGGER Aa2Like_TriggerLog ON Aa2Like AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Aa2Like';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

 	DECLARE @TeamId NVARCHAR(50);

	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
		BEGIN
			SET @writeType = 1;    -- Insert
			Select @TeamId=TeamId From Inserted;
		END
	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		begin
			SET @writeType = 2;    -- Update
			Select @TeamId=TeamId From Inserted;
		end
	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
		begin
 			SET @writeType = 3;    -- Delete
			Select @TeamId=TeamId From Deleted;
		end

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select * From Inserted For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select * From Deleted For Json Auto),1,4000);

	Insert Into AppLogTable(TableName,TeamId,InsertData,DeleteData,WriteType)
	Values(@tableName,@TeamId,@InsertData,@DeleteData,@writeType);
end
go

