--------------------------------------------------------------------
/*
Drop table Au1Team10
go
*/
--------------------------------------------------------------------
CREATE TABLE Au1Team10
(
	--AutoId      int IDENTITY(1,1) NOT NULL,
	--CustomerId   UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),	--客戶代號,村里代號
	TeamId        NVARCHAR(100) NOT NULL,	--客戶代號,村里代號
	TeamName      NVARCHAR(100) NOT NULL,	--客戶姓名，可由MemberData寫回
	MobileTel    NVARCHAR(100) NOT NULL,	-- 行動電話
	Email        NVARCHAR(100) null,	-- 電子郵件
	MonthAmt    INT NOT NULL DEFAULT 0,	-- 每月金額
	YearAmt     INT NOT NULL DEFAULT 0,	-- 年度金額
	BeginDate    date null DEFAULT GetDate()  ,--啟用日期
	EndDate      date null DEFAULT GetDate()  ,--到期日期
    Notes        NVARCHAR(200) null,	-- 備註說明

	--是否啟用 有效用戶
	IsOnOff     Bit NOT NULL DEFAULT 0,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Au1Team10_PrimaryKey PRIMARY KEY CLUSTERED (TeamId),
);
Go

--CREATE UNIQUE CLUSTERED INDEX Au1Team10_AutoId ON Au1Team10(AutoId);
go
CREATE INDEX Au1Team10_MobileTel ON Au1Team10 (MobileTel asc) --WHERE MobileTel Is NOT NULL;
go
CREATE INDEX Au1Team10_Email ON Au1Team10 (Email asc) --Where Email Is NOT NULL;
go

-- DROP TRIGGER Au1Team10_TriggerLog
go
CREATE TRIGGER Au1Team10_TriggerLog ON Au1Team10 AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Au1Team10';

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

	Insert Into AppLogTable(TableName,InsertData,DeleteData,WriteType)
	Values(@tableName,@InsertData,@DeleteData,@writeType);
end
go

insert into Au1Team10 (TeamId,TeamName,MobileTel,Email,MonthAmt,YearAmt,BeginDate,EndDate,Notes,IsOnOff,WriteInfo)
values ('VIP0001','測試會員1','0912345678','a012345678@b.com',1000,10000,'2025-01-01','2026-12-31','備註說明',1,'自動新增');
go

insert into Au1Team10 (TeamId,TeamName,MobileTel,Email,MonthAmt,YearAmt,BeginDate,EndDate,Notes,IsOnOff,WriteInfo)
values ('VIP0002','測試會員2','0912345678','a012345678@b.com',1000,10000,'2025-01-01','2026-12-31','備註說明',1,'自動新增');
go

insert into Au1Team10 (TeamId,TeamName,MobileTel,Email,MonthAmt,YearAmt,BeginDate,EndDate,Notes,IsOnOff,WriteInfo)
values ('VIP0003','測試會員3','0912345678','a012345678@b.com',1000,10000,'2025-01-01','2026-12-31','備註說明',1,'自動新增');
go