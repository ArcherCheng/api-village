
-----------------------------------------------------------------------------------------------------------------------
/*
Drop Table Va1VillageEducation;    -- 村里長學歷
Drop Table Va1VillageExperience;   -- 村里長經歷
Drop Table Va1VillagePolicy;   -- 村里長政見
Drop Table Va1VillagePhoto;    -- 村里長相片
Drop Table Va1Village;       -- 村里長姓名
*/
-----------------------------------------------------------------------------------------------------------------------
-- Drop Table Va1Village       -- 村里長姓名
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Va1Village
(
	--AutoId         int IDENTITY(1,1) NOT NULL,
	VillageId UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),--村長代號
	TeamId NVARCHAR(50) NOT NULL,--村里代號
	--客戶代號
	VillageName NVARCHAR(50) NOT NULL,
	-- 村里長姓名
	Sex NVARCHAR(1) NOT NULL,
	-- 性別: 1=男,2=女
	Birthday Date Null,--出生日期
	BirtCouty NVARCHAR(50) NOT NULL,--出生地縣市
	ElectYearINT NOT NULL DEFAULT 0,--當選日期,
	--ElectDate      Date Null,--當選日期,
	PhotoUrl NVARCHAR(500) ,--備註說明
	Notes NVARCHAR(500) ,--備註說明

	--for admin
	IsOnOff Bit NOT NULL DEFAULT 0,

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser NVARCHAR(100),
	CONSTRAINT Va1Village_PrimaryKey PRIMARY KEY CLUSTERED(VillageId),
	CONSTRAINT Va1Village_TeamId FOREIGN KEY (TeamId) REFERENCES Aa1Master (TeamId) ON UPDATE CASCADE ON DELETE CASCADE
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Va1Village_AutoId ON Va1Village(AutoId);
--go
-- CREATE UNIQUE INDEX Va1Village_TeamId ON Va1Village(TeamId,MasterName);
-- Go


-- DROP TRIGGER Va1Village_TriggerLog
go
CREATE TRIGGER Va1Village_TriggerLog ON Va1Village AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Va1Village';

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
End
Go


-----------------------------------------------------------------------------------------------------------------------
-- Drop Table Va1VillageEducation    -- 村里長學歷
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Va1VillageEducation
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
	CONSTRAINT Va1VillageEducation_PrimaryKey PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT Va1VillageEducation_VillageId FOREIGN KEY (VillageId) REFERENCES Va1Village (VillageId) ON UPDATE CASCADE ON DELETE CASCADE,
	--CONSTRAINT Va1VillageEducation_TeamId FOREIGN KEY (TeamId) REFERENCES Aa1Master (TeamId)
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Va1VillageEducation_AutoId ON Va1VillageEducation(AutoId);
--go
CREATE INDEX Va1VillageEducation_TeamId ON Va1VillageEducation(VillageId,SeqNo);
Go

-- DROP TRIGGER Va1VillageEducation_TriggerLog
go
CREATE TRIGGER Va1VillageEducation_TriggerLog ON Va1VillageEducation AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Va1VillageEducation';

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
-- Drop Table Va1VillageExperience    -- 村里長經歷
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Va1VillageExperience
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
	CONSTRAINT Va1VillageExperience_PrimaryKey PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT Va1VillageExperience_VillageId FOREIGN KEY (VillageId) REFERENCES Va1Village (VillageId) ON UPDATE CASCADE ON DELETE CASCADE,
	--CONSTRAINT Va1VillageExperience_TeamId FOREIGN KEY (TeamId) REFERENCES Aa1Master (TeamId)
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Va1VillageExperience_AutoId ON Va1VillageExperience(AutoId);
--go
CREATE INDEX Va1VillageExperience_TeamId ON Va1VillageExperience(VillageId,SeqNo);
Go

-- DROP TRIGGER Va1VillageExperience_TriggerLog
go
CREATE TRIGGER Va1VillageExperience_TriggerLog ON Va1VillageExperience AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Va1VillageExperience';

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
-- Drop Table Va1VillagePolicy   -- 村里長政見
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Va1VillagePolicy
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
	CONSTRAINT Va1VillagePolicy_PrimaryKey PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT Va1VillagePolicy_VillageId FOREIGN KEY (VillageId) REFERENCES Va1Village (VillageId) ON UPDATE CASCADE ON DELETE CASCADE,
	--CONSTRAINT Va1VillagePolicy_TeamId FOREIGN KEY (TeamId) REFERENCES Aa1Master (TeamId)
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Va1VillagePolicy_AutoId ON Va1VillagePolicy(AutoId);
--go
CREATE INDEX Va1VillagePolicy_TeamId ON Va1VillagePolicy(VillageId,SeqNo);
Go

-- DROP TRIGGER Va1VillagePolicy_TriggerLog
go
CREATE TRIGGER Va1VillagePolicy_TriggerLog ON Va1VillagePolicy AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Va1VillagePolicy';

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
-- Drop Table Va1VillagePhoto    -- 村里長相片
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Va1VillagePhoto
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
	CONSTRAINT Va1VillagePhoto_PrimaryKey PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT Va1VillagePhoto_VillageId FOREIGN KEY (VillageId) REFERENCES Va1Village (VillageId) ON UPDATE CASCADE ON DELETE CASCADE,
	--CONSTRAINT Va1VillagePhoto_TeamId FOREIGN KEY(TeamId) REFERENCES Aa1Master(TeamId)
);
Go

--建立索引檔
--CREATE UNIQUE CLUSTERED INDEX Va1VillagePhoto_AutoId ON Va1VillagePhoto(AutoId);
--go
CREATE INDEX Va1VillagePhoto_TeamId ON Va1VillagePhoto(VillageId,SeqNo);
Go

-- DROP TRIGGER Va1VillagePhoto_TriggerLog
go
CREATE TRIGGER Va1VillagePhoto_TriggerLog ON Va1VillagePhoto AFTER UPDATE,DELETE NOT FOR REPLICATION AS
Begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Va1VillagePhoto';

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
-- -- Drop Table Va1VillageNeighbor    -- 鄰居村里
-- -----------------------------------------------------------------------------------------------------------------------
-- CREATE TABLE Va1VillageNeighbor(
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
-- 	CONSTRAINT Va1VillageNeighbor_PrimaryKey PRIMARY KEY CLUSTERED(Id),
-- 	CONSTRAINT Va1VillageNeighbor_TeamId FOREIGN KEY (TeamId) REFERENCES Va1Village (TeamId) ON UPDATE CASCADE ON DELETE CASCADE,
-- 	CONSTRAINT Va1VillageNeighbor_NeighborId FOREIGN KEY (NeighborId) REFERENCES Va1Village (TeamId)
-- );
-- Go

-- --建立索引檔
-- --CREATE UNIQUE CLUSTERED INDEX Va1VillagePhoto_AutoId ON Va1VillagePhoto(AutoId);
-- --go
-- CREATE UNIQUE INDEX Va1VillageNeighbor_TeamId ON Va1VillageNeighbor(TeamId,NeighborId);
-- Go

-- -- DROP TRIGGER Va1VillageNeighbor_TriggerLog
-- go
-- CREATE TRIGGER Va1VillageNeighbor_TriggerLog ON Va1VillageNeighbor AFTER UPDATE,DELETE NOT FOR REPLICATION AS
-- Begin
-- 	DECLARE @tableName NVARCHAR(100);
-- 	SET @tableName='Va1VillageNeighbor';

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