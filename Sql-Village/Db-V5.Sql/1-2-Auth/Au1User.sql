/*
Drop table Au1User
go
*/
--------------------------------------------------------------------
-- Drop table Au1User
--------------------------------------------------------------------
CREATE TABLE Au1User
(
	-- auto_id   int IDENTITY(1,1) NOT NULL,
	--用戶代號
	UserId   UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	--客戶代號,村里代號
	TeamId NVARCHAR(100) NOT NULL,
	-- 用戶姓名
	UserName NVARCHAR(100) NOT NULL,
	-- 行動電話
	MobileTel NVARCHAR(100) NOT NULL,
	-- 我的生日--取回密碼用
	Birthday Date NOT NULL,
	-- 電子郵件
	Email NVARCHAR(100) null,
	--員工主檔也要加入以下這些欄位
	PhotoUrl NVARCHAR(200) null,--封面相片網址

	--是否啟用 有效用戶
	IsOnOff  Bit NOT NULL DEFAULT 0,
	UserType INT NOT NULL DEFAULT 0,--用戶類別 0=村里使用者,1=村里使用者,2=鄉鎮區使用者,3=縣市使用者,4=系統使用者
	UserCode INT NOT NULL DEFAULT 0,-- 1=verify code,2=reset password,
	UserData NVARCHAR(100) null,--用戶資料 ex:company, department, position
	UserRole NVARCHAR(100) null,--用戶角色 ex:company, department, position

	--這部份內容由系統自動產生
	LoginDate  datetime null,--本次登入日期
	LastDate   datetime null,--上次登入日期

	--密碼日期
	PasswordChangeDate DATETIME null,
	--是否需要變更密碼
	IsNeedChangePassword Bit NOT NULL DEFAULT 0,

	Notes NVARCHAR(200) null,

	--雜湊密碼
	PasswordHash varbinary(2000) null,
	--加鹽密碼
	PasswordSalt varbinary(2000) null,

	--寫入資訊
	WriteInfo NVARCHAR(100),

	CONSTRAINT Au1User_PrimaryKey PRIMARY KEY CLUSTERED (UserId),
	-- CONSTRAINT Au1User_ref_Au1Team FOREIGN KEY (TeamId) REFERENCES Au1Team(TeamId) ON UPDATE CASCADE ON DELETE CASCADE
);
Go

-- FOREIGN KEY
-- Alter Table Au1User add CONSTRAINT Au1User_ref_Au1Team
-- FOREIGN KEY (TeamId)
-- REFERENCES Au1Team(TeamId)
-- ON UPDATE CASCADE
-- ON DELETE NO ACTION
-- go


-- go
CREATE UNIQUE INDEX Inx_MobileTel ON Au1User (MobileTel asc);
go
CREATE INDEX Inx_Email ON Au1User (Email asc);
go

-- DROP TRIGGER Au1User_trigger1
go
CREATE TRIGGER Au1User_trigger1 ON Au1User AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
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
			Select @tableKey=Convert(NVARCHAR(100),UserId) From INSERTED;
		END
	ELSE IF (@insertRows=1) AND (@deleteRows=1)
		Begin	-- Update
			SET @writeType = 2;
			Select @tableKey=Convert(NVARCHAR(100),UserId) From INSERTED;
		END
	ELSE IF (@insertRows=0) AND (@deleteRows=1)
		Begin	-- Delete
			SET @writeType = 3;
			Select @tableKey=Convert(NVARCHAR(100),UserId) From Deleted;
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
	Values('Au1User',@tableKey,@writeType,@newData,@oldData);
end
go

/*

insert into Au1User(TeamId,UserName,MobileTel,Birthday,Email)
values('宜蘭縣三星鄉人和村','Archer','0970922888','1964-12-06','a0970922888@gmail.com')
go

insert into Au1User(TeamId,UserName,MobileTel,Birthday,Email)
values('宜蘭縣三星鄉人和村','Archer2','0970922889','1964-12-06','a0970922889@gmail.com')
go

select * from aut1User
go

select * from AppDataLog
go

update Au1User set UserCode=1 where MobileTel='0970922888'
go
update Au1User set UserCode=0
go

select * from AppDataLog
go

*/
