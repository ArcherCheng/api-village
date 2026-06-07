/*

Drop table Au1User
go

*/

--------------------------------------------------------------------
-- 用戶資料
--------------------------------------------------------------------
CREATE TABLE Au1User
(
	--AutoId      int IDENTITY(1,1) NOT NULL,
	UserId      UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	TeamId    NVARCHAR(50) NOT NULL,	--客戶代號,村里代號

	FirstName   NVARCHAR(100) NOT NULL,	--用戶姓氏
	LastName    NVARCHAR(100) NOT NULL,	--用戶名字
	UserName    as Trim(FirstName) + '*' + Trim(Substring(LastName,2,99)),	--用戶全名

	MobileTel   NVARCHAR(100) NOT NULL,	-- 行動電話
	Email       NVARCHAR(100) null,	-- 電子郵件
	Birthday    Date NOT NULL,	-- 我的生日 --取回密碼用

	PhotoUrl    NVARCHAR(200) null,--封面相片網址，可由MemberData寫回
	UserRole    NVARCHAR(100) null,--用戶角色
	UserData    NVARCHAR(100) null,--用戶資料 ex:company, department, position
	UserType   INT NOT NULL DEFAULT 0,--用戶類別 0=一般(前台)使用者,1=後台使用者,2=系統使用者
	UserCode   INT NOT NULL DEFAULT 0,-- 1=verify code,2=reset password,
	Notes       NVARCHAR(200) null,

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
	CONSTRAINT Au1User_Aa1Master FOREIGN KEY (TeamId) REFERENCES Aa1Master(TeamId) --ON UPDATE CASCADE ON DELETE NO ACTION
);
Go

--CREATE UNIQUE CLUSTERED INDEX Au1User_AutoId ON Au1User(AutoId);
go

CREATE INDEX Au1User_MobileTel ON Au1User (TeamId,MobileTel);
go
CREATE INDEX Au1User_Email ON Au1User (TeamId,Email );
go

-- DROP TRIGGER Au1User_TriggerLog
go
CREATE TRIGGER Au1User_TriggerLog ON Au1User AFTER UPDATE,DELETE NOT FOR REPLICATION AS
begin
	DECLARE @tableName NVARCHAR(100);
	SET @tableName='Au1User';

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








