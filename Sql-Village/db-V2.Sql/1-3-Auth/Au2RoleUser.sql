
/*
-- 多對多關聯設定
Drop table Au2RoleUser
go
Drop table Au2RoleAction
go
Drop table Au2RoleComponent
go

select * from Au2RoleUser
select * from Au2RoleAction

*/
--------------------------------------------------------------------
-- 多對多的關聯設定
-- Drop table Au2RoleUser
--------------------------------------------------------------------
CREATE TABLE Au2RoleUser
(
	--AutoId   int IDENTITY(1,1) NOT NULL,
	--Id        UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Id          int IDENTITY(1,1) NOT NULL,

	RoleId      UniqueIdentifier NOT NULL,
	UserId      UniqueIdentifier NOT NULL,
	IsOnOff     Bit NOT NULL DEFAULT 0,
	Notes       NVARCHAR(200),

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser  NVARCHAR(100),
	CONSTRAINT Au2RoleUser_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT Au2RoleUser_Au1Role FOREIGN KEY (RoleId) REFERENCES Au1Role (RoleId) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT Au2RoleUser_Au1User FOREIGN KEY (UserId) REFERENCES Au1User (UserId) ON UPDATE CASCADE ON DELETE CASCADE,
);
Go

--CREATE UNIQUE CLUSTERED INDEX Au2RoleUser_AutoId ON Au2RoleUser(AutoId);
go
CREATE UNIQUE INDEX Au2RoleUserRoleUser ON Au2RoleUser (RoleId asc, UserId asc);
go
-- DROP TRIGGER Au2RoleUser_trigger1
go
CREATE TRIGGER Au2RoleUser_trigger1 ON Au2RoleUser AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Au2RoleUser';

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
end
go

--------------------------------------------------------------------
-- 多對多的關聯設定
-- Drop table Au2RoleAction
--------------------------------------------------------------------
CREATE TABLE Au2RoleAction
(
	--AutoId    int IDENTITY(1,1) NOT NULL,
	--Id         UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Id           int IDENTITY(1,1) NOT NULL,

	RoleId       UniqueIdentifier NOT NULL,
	CtrlActnId   NVARCHAR(100) NOT NULL,
    IsOnOff      Bit NOT NULL DEFAULT 0,
	Notes        NVARCHAR(200),

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser  NVARCHAR(100),
	CONSTRAINT Au2RoleAction_PrimaryKey PRIMARY KEY CLUSTERED  (Id),
	CONSTRAINT Au2RoleAction_Au1Role FOREIGN KEY (RoleId) REFERENCES Au1Role (RoleId) ON UPDATE CASCADE,
	CONSTRAINT Au2RoleAction_Au1Action FOREIGN KEY (CtrlActnId) REFERENCES Au1Action (CtrlActnId) ON UPDATE CASCADE
);
Go

--CREATE UNIQUE CLUSTERED INDEX Au2RoleAction_AutoId ON Au2RoleAction(AutoId);
go
CREATE UNIQUE INDEX Au2RoleAction_RoleId ON Au2RoleAction (RoleId asc, CtrlActnId asc);
go
-- DROP TRIGGER Au2RoleAction_trigger1
go
CREATE TRIGGER Au2RoleAction_trigger1 ON Au2RoleAction AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Au2RoleAction';

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
end
go

--------------------------------------------------------------------
-- 多對多的關聯設定
-- Drop table Au2RoleComponent
--------------------------------------------------------------------
CREATE TABLE Au2RoleComponent
(
	--AutoId    int IDENTITY(1,1) NOT NULL,
	--Id         UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Id           int IDENTITY(1,1) NOT NULL,

	RoleId         UniqueIdentifier NOT NULL,
	ComponentId    NVARCHAR(100) NOT NULL,
    IsOnOff        Bit NOT NULL DEFAULT 0,
	Notes          NVARCHAR(200),

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser  NVARCHAR(100),
	CONSTRAINT Au2RoleComponent_PrimaryKey PRIMARY KEY CLUSTERED  (Id),
	CONSTRAINT Au2RoleComponent_Au1Role FOREIGN KEY (RoleId) REFERENCES Au1Role (RoleId) ON UPDATE CASCADE,
	CONSTRAINT Au2RoleComponent_Au1Component FOREIGN KEY (ComponentId) REFERENCES Au1Component (ComponentId) ON UPDATE CASCADE
);
Go

--CREATE UNIQUE CLUSTERED INDEXAu2RoleComponent_AutoId ON Au2RoleAction(AutoId);
go
CREATE UNIQUE INDEX Au2RoleComponent_RoleId ON Au2RoleComponent (RoleId asc, ComponentId asc);
go
-- DROP TRIGGER Au2RoleComponent_trigger1
go
CREATE TRIGGER Au2RoleComponent_trigger1 ON Au2RoleComponent AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Au2RoleComponent';

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
end
go
