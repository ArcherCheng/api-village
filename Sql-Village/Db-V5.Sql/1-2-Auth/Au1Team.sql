/*

Drop table Au1Team;
go

*/


--------------------------------------------------------------------
-- Drop table Au1Team;
--------------------------------------------------------------------
CREATE TABLE Au1Team
(
    --Id         INT IDENTITY(1,1) NOT NULL,
    --村里代號
    --TeamId NVARCHAR(100) NOT NULL DEFAULT NewSequentialId(),
    TeamId NVARCHAR(100) NOT NULL,

    NationId NVARCHAR(10),--縣市代碼
    CityId NVARCHAR(100),--縣市代碼
    City NVARCHAR(100),--縣市名稱
    CityCode NVARCHAR(1),--縣市代碼(1碼)
    CityShort NVARCHAR(3),--縣市簡稱(3碼)

    TownId NVARCHAR(100),--鄉鎮區代碼
    Town NVARCHAR(100),--鄉鎮區名稱
    PostalCode NVARCHAR(100),--郵遞區號

    VillageId NVARCHAR(100),--村里代碼
    Village NVARCHAR(100),--村里名稱

    CityOrder INT,
    TownOrder INT,
    VillageOrder INT,
    -- url_name        NVARCHAR(100) ,--Url名稱
    Notes NVARCHAR(200),--備註說明
	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
    CONSTRAINT Au1Team_PrimaryKey PRIMARY KEY CLUSTERED(TeamId),
);
Go

CREATE INDEX Inx_CityId ON Au1Team (CityId,TownId,VillageId);
go
CREATE INDEX Inx_City ON Au1Team (City,Town,Village);
go
CREATE INDEX Inx_VillageId ON Au1Team (VillageId);
go



-- DROP TRIGGER Au1Team_Trigger1;
-- go

CREATE TRIGGER Au1Team_Trigger1 ON Au1Team AFTER UPDATE,DELETE NOT FOR REPLICATION AS
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
			Select @tableKey=Convert(NVARCHAR(100),TeamId) From INSERTED;
		END
	ELSE IF (@insertRows=1) AND (@deleteRows=1)
		Begin	-- Update
			SET @writeType = 2;
			Select @tableKey=Convert(NVARCHAR(100),TeamId) From INSERTED;
		END
	ELSE IF (@insertRows=0) AND (@deleteRows=1)
		Begin	-- Delete
			SET @writeType = 3;
			Select @tableKey=Convert(NVARCHAR(100),TeamId) From Deleted;
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
	Values('Au1Team',@tableKey,@writeType,@newData,@oldData);
End
Go

/*
select * from Au1Team
go

insert into Au1Team(TeamId,NationId,CityId,City,CityCode,CityShort,TownId,Town,PostalCode,VillageId,Village,CityOrder,TownOrder,VillageOrder)
values('0970922888','TW','68','桃園市','H','TAO','68000010','桃園區','330','68000010049','中寧里',160,1,49)
go
select TeamId,NationId,CityId,City,CityCode,CityShort,TownId,Town,PostalCode,VillageId,Village,CityOrder,TownOrder,VillageOrder from au1team
Where VillageId='68000010049'
go

insert into Au1Team(TeamId,NationId,CityId,City,CityCode,CityShort,TownId,Town,PostalCode,VillageId,Village,CityOrder,TownOrder,VillageOrder)
values('0937452882','TW','68','桃園市','H','TAO','68000050','蘆竹區','338','68000050012','山腳里',160,5,12)
go
select TeamId,NationId,CityId,City,CityCode,CityShort,TownId,Town,PostalCode,VillageId,Village,CityOrder,TownOrder,VillageOrder from au1team
Where VillageId='68000050012'
go



select * from AppDataLog
go

update Au1Team Set Notes='Test1' where TeamId='0970922888'
go
update Au1Team Set Notes='Test1' where TeamId='0937452882'
go

-- test only 1 row will be recorded in AppDataLog
select * from Au1Team where Notes='Test1'
go
update Au1Team Set Notes='Test2' where Notes='Test1'
go
select * from AppDataLog
go


*/
