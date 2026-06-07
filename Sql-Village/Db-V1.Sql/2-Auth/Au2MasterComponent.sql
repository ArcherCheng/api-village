/*

drop table Au2MasterComponent
go

*/
--------------------------------------------------------------------
-- 用戶資料
--------------------------------------------------------------------
CREATE TABLE Au2MasterComponent
(
	--AutoId      int IDENTITY(1,1) NOT NULL,
	Id          int IDENTITY(1,1) NOT NULL,
	TeamId    NVARCHAR(50) NOT NULL,	--客戶代號,村里代號
	ComponentId NVARCHAR(100) NOT NULL,  --元件編號

    IsOnOff     Bit NOT NULL DEFAULT 0,
    IsInsert    Bit NOT NULL DEFAULT 0,
    IsUpdate    Bit NOT NULL DEFAULT 0,
    IsDelete    Bit NOT NULL DEFAULT 0,
	Notes       NVARCHAR(200),

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Au2MasterComponent_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT Au2MasterComponent_Au1Component FOREIGN KEY (ComponentId) REFERENCES Au1Component(ComponentId) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT Au2MasterComponent_Aa1Master FOREIGN KEY (TeamId) REFERENCES Aa1Master(TeamId), -- ON UPDATE CASCADE ON DELETE NO ACTION,
);
Go

-- CREATE UNIQUE CLUSTERED INDEXAu2RoleComponent_AutoId ON Au2RoleAction(AutoId);
-- go
CREATE UNIQUE INDEX Au2MasterComponent_ComponentId ON Au2MasterComponent (TeamId asc, ComponentId asc);
go

-- DROP TRIGGER Au2MasterComponent_TriggerLog
go
CREATE TRIGGER Au2MasterComponent_TriggerLog ON Au2MasterComponent AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Au2MasterComponent';

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


