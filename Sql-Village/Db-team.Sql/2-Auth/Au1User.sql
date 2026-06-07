/*

Drop table Au1User
go
Drop table Au1Role
go
Drop table Au1Action
go
-- Drop table Au1Ctrller
-- go
Drop table Au1Component
go
-- Drop table Au1System
-- go
*/

--------------------------------------------------------------------
CREATE TABLE Au1User
(
	--AutoId      int IDENTITY(1,1) NOT NULL,
	UserId      UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),	--客戶代號,村里代號
	TeamId       NVARCHAR(100) NOT NULL,	--客戶代號,村里代號

	UserName    NVARCHAR(100) NOT NULL,	--用戶姓名
	UserAlias   NVARCHAR(100) NOT NULL,	--用戶別名
	MobileTel   NVARCHAR(100) NOT NULL,	-- 行動電話
	Email       NVARCHAR(100) null,	-- 電子郵件
	Birthday    Date NOT NULL,	-- 我的生日 --取回密碼用

	PhotoUrl    NVARCHAR(200) null,--封面相片網址，可由MemberData寫回
	UserRole    NVARCHAR(100) null,--用戶角色
	UserData    NVARCHAR(100) null,--用戶資料 ex:company, department, position
	UserType   INT NOT NULL DEFAULT 0,--用戶類別 0=一般(前台)使用者,1=後台使用者,2=系統使用者
	UserCode   INT NOT NULL DEFAULT 0,-- 1=verify code,2=reset password,
	Notes              NVARCHAR(200) null,

	--這部份內容由系統自動產生
	PasswordHash       varbinary(2000) null,	--雜湊密碼
	PasswordSalt       varbinary(2000) null,	--加鹽密碼
	PasswordChangeDate DateTime null,	--密碼日期

	--是否啟用 有效用戶
	IsOnOff            Bit NOT NULL DEFAULT 0,
	LoginThisTime      datetime null,--本次登入日期
	LoginLastTime      datetime null,--上次登入日期
	LoginErrorTimes   INT NOT NULL DEFAULT 0,--登入錯誤次數
	LoginErrorIp       NVARCHAR(100) null,--登入錯誤IP

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Au1User_PrimaryKey PRIMARY KEY CLUSTERED (UserId),
	CONSTRAINT Au1User_Au1Team10 FOREIGN KEY (TeamId) REFERENCES Au1Team10(TeamId)
);
Go

--CREATE UNIQUE CLUSTERED INDEX Au1User_AutoId ON Au1User(AutoId);
go

CREATE INDEX Au1User_MobileTel ON Au1User (MobileTel asc);
go
CREATE INDEX Au1User_Email ON Au1User (Email asc);
go

-- DROP TRIGGER Au1User_TriggerLog
go
CREATE TRIGGER Au1User_TriggerLog ON Au1User AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Au1User';

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

-- --------------------------------------------------------------------
-- -- Drop table Au1Role
-- --------------------------------------------------------------------
-- CREATE TABLE Au1Role
-- (
-- 	-- Id          int IDENTITY(1,1) NOT NULL,

-- 	RoleId      NVARCHAR(100) NOT NULL,  --角色代號
-- 	RoleName    NVARCHAR(100) NOT NULL,  --角色名稱
-- 	SortOrder  INT NOT NULL DEFAULT 0, --排序
-- 	IsOnOff     Bit NOT NULL DEFAULT 0, -- 是否啟用
-- 	Notes       NVARCHAR(200),
-- 	-- 以下每檔資料表都會有這些欄位
-- 	WriteInfo NVARCHAR(100),
-- 	CONSTRAINT Au1Role_PrimaryKey PRIMARY KEY CLUSTERED (RoleId)
-- );
-- Go


-- --CREATE UNIQUE CLUSTERED INDEX Au1Role_AutoId ON Au1Role(AutoId);
-- --go

-- -- DROP TRIGGER Au1Role_TriggerLog
-- go
-- CREATE TRIGGER Au1Role_TriggerLog ON Au1Role AFTER UPDATE,DELETE NOT FOR REPLICATION AS
-- begin
-- 	DECLARE @tableName NVARCHAR(100);
-- 	SET @tableName='Au1Role';

-- 	DECLARE @writeType Tinyint;
-- 	SET @writeType=0;

-- 	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 1;    -- Insert
-- 	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 2;    -- Update
-- 	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 3;    -- Delete

-- 	DECLARE @InsertData NVARCHAR(4000);
-- 	DECLARE @DeleteData NVARCHAR(4000);

-- 	SET @InsertData=SUBSTRING((Select * From Inserted For Json Auto),1,4000);
-- 	SET @DeleteData=SUBSTRING((Select * From Deleted For Json Auto),1,4000);

-- 	Insert Into AppLogTable(TableName,InsertData,DeleteData,WriteType)
-- 	Values(@tableName,@InsertData,@DeleteData,@writeType);
-- end
-- go

-- --------------------------------------------------------------------
-- -- Drop table Au1Action
-- --------------------------------------------------------------------
-- CREATE TABLE Au1Action
-- (
-- 	--AutoId        int IDENTITY(1,1) NOT NULL,
-- 	--ActionId  UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),

-- 	CtrlActnId      NVARCHAR(100) NOT NULL,  --程式動作編號
-- 	CtrllerId       NVARCHAR(100)  NOT NULL,  --程式編號
-- 	ActionId        NVARCHAR(100)  NOT NULL,  --動作編號
-- 	CtrllerDesc     NVARCHAR(100)  NOT NULL,  --程式說明
-- 	ActionDesc      NVARCHAR(100)  NOT NULL,  --動作說明
-- 	HttpMethod      NVARCHAR(100)  NOT NULL,  --HTTP方法
-- 	HttpRoute       NVARCHAR(200) NOT NULL,  --HTTP路由
-- 	IsRbacAuthorize BIT NOT NULL DEFAULT 0,  --檢查角色權限
-- 	SpaSystem       NVARCHAR(100)  null,   --首頁功能表代號

-- 	ExternalUrl     NVARCHAR(200),   --外部URL
-- 	SortOrder      INT NOT NULL DEFAULT 0, --排序
-- 	Notes           NVARCHAR(200),  --備註說明
-- 	-- 以下每檔資料表都會有這些欄位
-- 	WriteInfo NVARCHAR(100),
-- 	CONSTRAINT Au1Action_PrimaryKey PRIMARY KEY CLUSTERED (CtrlActnId)
-- );
-- Go

-- --CREATE UNIQUE CLUSTERED INDEX Au1Action_AutoId ON Au1Action(AutoId);
-- go
-- --CREATE UNIQUE INDEX Au1Action_HttpRoute ON Au1Action (HttpMethod asc, HttpRoute asc);
-- go
-- CREATE UNIQUE INDEX Au1Action_CtrllerAction ON Au1Action (CtrllerId asc, ActionId asc);
-- go


-- -- DROP TRIGGER Au1Action_TriggerLog
-- go
-- CREATE TRIGGER Au1Action_TriggerLog ON Au1Action AFTER UPDATE,DELETE NOT FOR REPLICATION AS
-- begin
-- 	DECLARE @tableName NVARCHAR(100);
-- 	SET @tableName='Au1Action';

-- 	DECLARE @writeType Tinyint;
-- 	SET @writeType=0;

-- 	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 1;    -- Insert
-- 	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 2;    -- Update
-- 	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 3;    -- Delete

-- 	DECLARE @InsertData NVARCHAR(4000);
-- 	DECLARE @DeleteData NVARCHAR(4000);

-- 	SET @InsertData=SUBSTRING((Select * From Inserted For Json Auto),1,4000);
-- 	SET @DeleteData=SUBSTRING((Select * From Deleted For Json Auto),1,4000);

-- 	Insert Into AppLogTable(TableName,InsertData,DeleteData,WriteType)
-- 	Values(@tableName,@InsertData,@DeleteData,@writeType);
-- end
-- go

-- --------------------------------------------------------------------
-- -- Drop table Au1Component
-- --------------------------------------------------------------------
-- CREATE TABLE Au1Component
-- (
-- 	--AutoId        int IDENTITY(1,1) NOT NULL,

-- 	ComponentId   NVARCHAR(100) NOT NULL,  --元件編號
-- 	SystemId      NVARCHAR(100) NOT NULL,  --系統編號
-- 	SubGroup      NVARCHAR(100) NOT NULL,  --子系統編號
-- 	ComponentDesc NVARCHAR(100) null,   --元件說明
-- 	SystemDesc    NVARCHAR(100) null,   --系統說明
-- 	SortOrder    INT NOT NULL DEFAULT 0,  --排序
-- 	Notes         NVARCHAR(200) null,   --備註說明
-- 	-- 以下每檔資料表都會有這些欄位
-- 	WriteInfo NVARCHAR(100),
-- 	CONSTRAINT Au1Component_PrimaryKey PRIMARY KEY CLUSTERED (ComponentId) ,
-- );
-- Go

-- --CREATE UNIQUE CLUSTERED INDEX Au1Component_AutoId ON Au1Action(AutoId);
-- go


-- -- DROP TRIGGER Au1Component_TriggerLog
-- go
-- CREATE TRIGGER Au1Component_TriggerLog ON Au1Component AFTER UPDATE,DELETE NOT FOR REPLICATION AS
-- begin
-- 	DECLARE @tableName NVARCHAR(100);
-- 	SET @tableName='Au1Component';

-- 	DECLARE @writeType Tinyint;
-- 	SET @writeType=0;

-- 	IF EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 1;    -- Insert
-- 	ELSE IF EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 2;    -- Update
-- 	ELSE IF NOT EXISTS(SELECT 1 FROM INSERTED) AND EXISTS(SELECT 1 FROM DELETED)
-- 		SET @writeType = 3;    -- Delete

-- 	DECLARE @InsertData NVARCHAR(4000);
-- 	DECLARE @DeleteData NVARCHAR(4000);

-- 	SET @InsertData=SUBSTRING((Select * From Inserted For Json Auto),1,4000);
-- 	SET @DeleteData=SUBSTRING((Select * From Deleted For Json Auto),1,4000);

-- 	Insert Into AppLogTable(TableName,InsertData,DeleteData,WriteType) Values(@tableName,@InsertData,@DeleteData,@writeType);
-- end
-- go













