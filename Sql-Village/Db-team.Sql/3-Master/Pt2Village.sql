
-----------------------------------------------------------------------------------------------------------------------
/*
Drop Table Pt2VillageEducation;    -- 村里長學歷
Drop Table Pt2VillageExperience;   -- 村里長經歷
Drop Table Pt2VillagePolicy;   -- 村里長政見
Drop Table Pt2VillagePhoto;    -- 村里長相片
Drop Table Pt2Village;       -- 村里長姓名
*/
-----------------------------------------------------------------------------------------------------------------------
-- Drop Table Pt2Village       -- 村里長姓名
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2Village
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	VillageId UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),--村長代號
	TeamId UniqueIdentifier NOT NULL,--村里代號
	--客戶代號
	VillageName NVARCHAR(50) NOT NULL,
	-- 村里長姓名
	Sex NVARCHAR(1) NOT NULL,
	-- 性別: 1=男,2=女
	Birthday Date Null,--出生日期
	BirtCouty NVARCHAR(50) NOT NULL,--出生地縣市
	ElectYear INT NOT NULL DEFAULT 0,--當選日期,
	--ElectDate      Date Null,--當選日期,
	PhotoUrl NVARCHAR(500) ,--備註說明
	Notes NVARCHAR(500) ,--備註說明

	--for admin
	IsOnOff Bit NOT NULL DEFAULT 0,

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser NVARCHAR(100),
	CONSTRAINT Pt2Village_PrimaryKey PRIMARY KEY CLUSTERED(VillageId),
	CONSTRAINT Pt2Village_TeamId FOREIGN KEY (TeamId) REFERENCES Aa1Team (TeamId) ON UPDATE CASCADE ON DELETE CASCADE
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Pt2Village_AutoId ON Pt2Village(AutoId);
--go
-- CREATE UNIQUE INDEX Pt2Village_TeamId ON Pt2Village(TeamId,MasterName);
-- Go

-- 建立關連檔案
--Alter Table Pt2Village Add CONSTRAINT Pt2Village_TeamId
--	FOREIGN KEY (TeamId)
--	REFERENCES Aa1Team(TeamId)
--	ON UPDATE CASCADE
--	--ON DELETE CASCADE
--Go

-- DROP TRIGGER Pt2Village_TriggerLog
go
CREATE TRIGGER Pt2Village_TriggerLog ON Pt2Village AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Pt2Village';

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
End
Go


-----------------------------------------------------------------------------------------------------------------------
-- Drop Table Pt2VillageEducation    -- 村里長學歷
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2VillageEducation
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	Id int IDENTITY(1,1) NOT NULL,
	VillageId UniqueIdentifier NOT NULL,--村長代號
	--TeamId UniqueIdentifier NOT NULL,
	SeqNo decimal(10,2) NOT NULL,
	-- 次序
	OrderTitle NVARCHAR(10) ,
	-- 序號
	Descriptions NVARCHAR(500) NOT NULL,
	-- 學歷
	Notes NVARCHAR(500) ,--備註說明

	--for custom notes

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser NVARCHAR(100),
	CONSTRAINT Pt2VillageEducation_PrimaryKey PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT Pt2VillageEducation_VillageId FOREIGN KEY (VillageId) REFERENCES Pt2Village (VillageId) ON UPDATE CASCADE ON DELETE CASCADE,
	--CONSTRAINT Pt2VillageEducation_TeamId FOREIGN KEY (TeamId) REFERENCES Aa1Team (TeamId)
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Pt2VillageEducation_AutoId ON Pt2VillageEducation(AutoId);
--go
CREATE INDEX Pt2VillageEducation_TeamId ON Pt2VillageEducation(VillageId,SeqNo);
Go

-- DROP TRIGGER Pt2VillageEducation_TriggerLog
go
CREATE TRIGGER Pt2VillageEducation_TriggerLog ON Pt2VillageEducation AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Pt2VillageEducation';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

	IF EXISTS(Select 1
		From Inserted) AND NOT EXISTS(Select 1
		From Deleted)
		SET @writeType = 1;    -- Insert
	ELSE IF EXISTS(Select 1
		From Inserted) AND EXISTS(Select 1
		From Deleted)
		SET @writeType = 2;    -- Update
	ELSE IF NOT EXISTS(Select 1
		From Inserted) AND EXISTS(Select 1
		From Deleted)
		SET @writeType = 3;
	-- Delete

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select *
	From Inserted
	For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select *
	From Deleted
	For Json Auto),1,4000);

	Insert Into AppLogTable
		(TableName,InsertData,DeleteData,WriteType)
	Values(@tableName, @InsertData, @DeleteData, @writeType);
End
Go
-----------------------------------------------------------------------------------------------------------------------
-- Drop Table Pt2VillageExperience    -- 村里長經歷
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2VillageExperience
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	Id int IDENTITY(1,1) NOT NULL,
	VillageId UniqueIdentifier NOT NULL,--村長代號
	--TeamId UniqueIdentifier NOT NULL,
	SeqNo decimal(10,2) NOT NULL,
	-- 次序
	OrderTitle NVARCHAR(10) ,
	-- 序號
	Descriptions NVARCHAR(500) NOT NULL,
	-- 學歷
	Notes NVARCHAR(500) ,--備註說明

	--for custom notes

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser NVARCHAR(100),
	CONSTRAINT Pt2VillageExperience_PrimaryKey PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT Pt2VillageExperience_VillageId FOREIGN KEY (VillageId) REFERENCES Pt2Village (VillageId) ON UPDATE CASCADE ON DELETE CASCADE,
	--CONSTRAINT Pt2VillageExperience_TeamId FOREIGN KEY (TeamId) REFERENCES Aa1Team (TeamId)
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Pt2VillageExperience_AutoId ON Pt2VillageExperience(AutoId);
--go
CREATE INDEX Pt2VillageExperience_TeamId ON Pt2VillageExperience(VillageId,SeqNo);
Go

-- DROP TRIGGER Pt2VillageExperience_TriggerLog
go
CREATE TRIGGER Pt2VillageExperience_TriggerLog ON Pt2VillageExperience AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Pt2VillageExperience';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

	IF EXISTS(Select 1
		From Inserted) AND NOT EXISTS(Select 1
		From Deleted)
		SET @writeType = 1;    -- Insert
	ELSE IF EXISTS(Select 1
		From Inserted) AND EXISTS(Select 1
		From Deleted)
		SET @writeType = 2;    -- Update
	ELSE IF NOT EXISTS(Select 1
		From Inserted) AND EXISTS(Select 1
		From Deleted)
		SET @writeType = 3;
	-- Delete

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select *
	From Inserted
	For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select *
	From Deleted
	For Json Auto),1,4000);

	Insert Into AppLogTable
		(TableName,InsertData,DeleteData,WriteType)
	Values(@tableName, @InsertData, @DeleteData, @writeType);
End
Go
-----------------------------------------------------------------------------------------------------------------------
-- Drop Table Pt2VillagePolicy   -- 村里長政見
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2VillagePolicy
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	Id int IDENTITY(1,1) NOT NULL,
	VillageId UniqueIdentifier NOT NULL,--村里代號
	SeqNo decimal(10,2) NOT NULL,
	-- 次序
	OrderTitle NVARCHAR(10) ,
	-- 序號
	Descriptions NVARCHAR(500) NOT NULL,
	-- 學歷
	Notes NVARCHAR(500) ,--備註說明

	--for custom notes

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser NVARCHAR(100),
	CONSTRAINT Pt2VillagePolicy_PrimaryKey PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT Pt2VillagePolicy_VillageId FOREIGN KEY (VillageId) REFERENCES Pt2Village (VillageId) ON UPDATE CASCADE ON DELETE CASCADE,
	--CONSTRAINT Pt2VillagePolicy_TeamId FOREIGN KEY (TeamId) REFERENCES Aa1Team (TeamId)
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Pt2VillagePolicy_AutoId ON Pt2VillagePolicy(AutoId);
--go
CREATE INDEX Pt2VillagePolicy_TeamId ON Pt2VillagePolicy(VillageId,SeqNo);
Go

-- DROP TRIGGER Pt2VillagePolicy_TriggerLog
go
CREATE TRIGGER Pt2VillagePolicy_TriggerLog ON Pt2VillagePolicy AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Pt2VillagePolicy';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

	IF EXISTS(Select 1
		From Inserted) AND NOT EXISTS(Select 1
		From Deleted)
		SET @writeType = 1;    -- Insert
	ELSE IF EXISTS(Select 1
		From Inserted) AND EXISTS(Select 1
		From Deleted)
		SET @writeType = 2;    -- Update
	ELSE IF NOT EXISTS(Select 1
		From Inserted) AND EXISTS(Select 1
		From Deleted)
		SET @writeType = 3;
	-- Delete

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select *
	From Inserted
	For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select *
	From Deleted
	For Json Auto),1,4000);

	Insert Into AppLogTable
		(TableName,InsertData,DeleteData,WriteType)
	Values(@tableName, @InsertData, @DeleteData, @writeType);
End
Go
-----------------------------------------------------------------------------------------------------------------------
-- Drop Table Pt2VillagePhoto    -- 村里長相片
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2VillagePhoto
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	Id int IDENTITY(1,1) NOT NULL,
	VillageId UniqueIdentifier NOT NULL,--村里代號
	--TeamId UniqueIdentifier NOT NULL,
	--客戶代號
	SeqNo decimal(10,2) NOT NULL DEFAULT 0,
	-- 次序
	IsMain Bit NOT NULL DEFAULT 0,

	PublicKey NVARCHAR(200) ,
	--
	PhotoUrl NVARCHAR(200) ,
	--
	Descriptions NVARCHAR(500) ,
	-- 說明
	Notes NVARCHAR(500) ,--備註說明

	--for custom notes

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser NVARCHAR(100),
	CONSTRAINT Pt2VillagePhoto_PrimaryKey PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT Pt2VillagePhoto_VillageId FOREIGN KEY (VillageId) REFERENCES Pt2Village (VillageId) ON UPDATE CASCADE ON DELETE CASCADE,
	--CONSTRAINT Pt2VillagePhoto_TeamId FOREIGN KEY(TeamId) REFERENCES Aa1Team(TeamId)
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Pt2VillagePhoto_AutoId ON Pt2VillagePhoto(AutoId);
--go
CREATE INDEX Pt2VillagePhoto_TeamId ON Pt2VillagePhoto(VillageId,SeqNo);
Go

-- DROP TRIGGER Pt2VillagePhoto_TriggerLog
go
CREATE TRIGGER Pt2VillagePhoto_TriggerLog ON Pt2VillagePhoto AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Pt2VillagePhoto';

	DECLARE @writeType Tinyint;
	SET @writeType=0;

	IF EXISTS(Select 1
		From Inserted) AND NOT EXISTS(Select 1
		From Deleted)
		SET @writeType = 1;    -- Insert
	ELSE IF EXISTS(Select 1
		From Inserted) AND EXISTS(Select 1
		From Deleted)
		SET @writeType = 2;    -- Update
	ELSE IF NOT EXISTS(Select 1
		From Inserted) AND EXISTS(Select 1
		From Deleted)
		SET @writeType = 3;
	-- Delete

	DECLARE @InsertData NVARCHAR(4000);
	DECLARE @DeleteData NVARCHAR(4000);

	SET @InsertData=SUBSTRING((Select *
	From Inserted
	For Json Auto),1,4000);
	SET @DeleteData=SUBSTRING((Select *
	From Deleted
	For Json Auto),1,4000);

	Insert Into AppLogTable
		(TableName,InsertData,DeleteData,WriteType)
	Values(@tableName, @InsertData, @DeleteData, @writeType);
End
Go


-- -----------------------------------------------------------------------------------------------------------------------
-- -- Drop Table Pt2VillageNeighbor    -- 鄰居村里
-- -----------------------------------------------------------------------------------------------------------------------
-- CREATE TABLE Pt2VillageNeighbor(
-- 	--AutoId         int IDENTITY(1,1) NOT NULL,
-- 	Id             int IDENTITY(1,1) NOT NULL,
-- 	TeamId       UniqueIdentifier NOT NULL,--村里代號
-- 	NeighborId     UniqueIdentifier NOT NULL,--鄰居村里代號
-- 	Direction      NVARCHAR(500) ,--方向說明
-- 	Notes          NVARCHAR(500) ,--備註說明

-- 	--for custom notes

-- 	-- 以下每檔資料表都會有這些欄位
-- 	CreateUser NVARCHAR(100),
-- 	UpdateUser NVARCHAR(100),
-- 	BatchUser  NVARCHAR(100),
-- 	CONSTRAINT Pt2VillageNeighbor_PrimaryKey PRIMARY KEY CLUSTERED(Id),
-- 	CONSTRAINT Pt2VillageNeighbor_TeamId FOREIGN KEY (TeamId) REFERENCES Pt2Village (TeamId) ON UPDATE CASCADE ON DELETE CASCADE,
-- 	CONSTRAINT Pt2VillageNeighbor_NeighborId FOREIGN KEY (NeighborId) REFERENCES Pt2Village (TeamId)
-- );
-- Go

-- --建立索引檔
-- --CREATE UNIQUE CLUSTERED INDEX Pt2VillagePhoto_AutoId ON Pt2VillagePhoto(AutoId);
-- --go
-- CREATE UNIQUE INDEX Pt2VillageNeighbor_TeamId ON Pt2VillageNeighbor(TeamId,NeighborId);
-- Go

-- -- DROP TRIGGER Pt2VillageNeighbor_TriggerLog
-- go
-- CREATE TRIGGER Pt2VillageNeighbor_TriggerLog ON Pt2VillageNeighbor AFTER UPDATE,DELETE NOT FOR REPLICATION AS
-- Begin
-- 	DECLARE @tableName NVARCHAR(100);
-- 	SET @tableName='Pt2VillageNeighbor';

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
-- End
-- Go