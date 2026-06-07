/*
Drop Table AppLogTable;
go
Drop Table AppLogLogin;
go
Drop Table AppLogRequest;
go
Drop Table AppLogMachine;
go
Drop Table AppLogMessage;
go
*/
-----------------------------------------
-- 記錄每一筆資料的異動備份
-----------------------------------------
CREATE TABLE AppLogTable(
	Id         int IDENTITY(1,1) NOT NULL, --序號
	TeamId   NVARCHAR(50) NOT NULL,	--客戶代號,村里代號
	TableName  NVARCHAR(100) NOT NULL DEFAULT '', --表格名稱
	WriteType  integer NOT NULL DEFAULT 0, --異動類別
	WriteTime  DateTime NOT NULL Default Getdate(), --異動時間
	InsertData NVARCHAR(4000) Null,  -- 異動後資料, 一定要設成 max, 因為可能會有一次多筆的更新或刪除
	DeleteData NVARCHAR(4000) Null,  -- 異動前資料, 一定要設成 max, 因為可能會有一次多筆的更新或刪除
	CONSTRAINT AppLogTable_PrimaryKey PRIMARY KEY CLUSTERED (Id)
);
Go

CREATE INDEX AppLogTable_TableName ON AppLogTable(TeamId,WriteTime,TableName);
Go

-----------------------------------------
-- 記錄使用者的請求訪視網頁記錄
-- Drop Table AppLogLogin
-----------------------------------------
CREATE TABLE AppLogLogin(
	Id           int IDENTITY(1,1) NOT NULL,  --序號
	TeamId     NVARCHAR(50) NOT NULL,	--客戶代號,村里代號
	UserIdName   NVARCHAR(100) null,  --用戶代號
    LoginState   NVARCHAR(100) null,  --豋入狀態
	IsSuccess    bit,  --是否成功
	WriteIp		 NVARCHAR(50),  --IP位置
	WriteMac	 NVARCHAR(50),  --MAC位置
	WriteTime    DateTime Default Getdate(),  --查詢時間
	CONSTRAINT   AppLogLogin_PrimaryKey PRIMARY KEY CLUSTERED (Id)
);
Go

CREATE INDEX AppLogLogin_UserIdName ON AppLogLogin(TeamId,WriteTime,UserIdName);
Go
CREATE INDEX AppLogLogin_WriteIp ON AppLogLogin(TeamId,WriteTime,WriteIp);
Go
CREATE INDEX AppLogLogin_WriteMac ON AppLogLogin(TeamId,WriteTime,WriteMac);
Go


-----------------------------------------
-- 記錄使用者的請求訪視網頁記錄
-- Drop Table AppLogRequest
-----------------------------------------
CREATE TABLE AppLogRequest(
	Id           int IDENTITY(1,1) NOT NULL, --序號
	TeamId     NVARCHAR(50) NOT NULL,	--客戶代號,村里代號
	UserIdName   NVARCHAR(100) null,  --用戶代號
	CtrllerId    NVARCHAR(100) null,  --系統程式編號
	ActionId     NVARCHAR(100) null,  --執行動作編號
	HttpVerb     NVARCHAR(20) null,  --HTTP方法
	HttpRoute    NVARCHAR(100) null,  --HTTP網址
	QueryString  NVARCHAR(2000) null,  --查詢字串
	IsSuccess    Bit NOT NULL DEFAULT 0, --是否成功
	WriteIp		 NVARCHAR(50),  --查詢IP位置
	WriteTime    DateTime Default Getdate(),  --查詢時間
	CONSTRAINT   AppLogRequest_PrimaryKey PRIMARY KEY CLUSTERED (Id)
);
Go

CREATE INDEX AppLogRequest_CtrllerId ON AppLogRequest(TeamId,WriteTime,CtrllerId);
Go
CREATE INDEX AppLogRequest_UserIdName ON AppLogRequest(TeamId,WriteTime,UserIdName);
Go
CREATE INDEX AppLogRequest_HttpRoute ON AppLogRequest(TeamId,WriteTime,HttpRoute);
Go


--------------------------------------------------------------------------------------------
-- Drop Table AppLogMachine; -- 員工密碼孌更紀錄
--------------------------------------------------------------------------------------------
CREATE TABLE AppLogMachine (
	--Id               UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Id           int IDENTITY(1,1) NOT NULL,
	TeamId     NVARCHAR(50) NOT NULL,	--客戶代號,村里代號

	UserIdName   NVARCHAR(100) null,  --用戶代號
  	MacAddress   NVARCHAR(50) NOT NULL,--機器編號 Media Access Control address
  	IpAddress    NVARCHAR(50) NOT NULL,--IP位址

  	IsVerify     bit NOT NULL DEFAULT 0, --IP位址
  	VerifyCode   NVARCHAR(10),--IP位址

  	LoginDate    DateTime Null,    --登入日期
  	ErrorTimes   int Null,    --錯誤次數
	Notes        NVARCHAR(200),

	CONSTRAINT AppLogMachine_PrimaryKey PRIMARY KEY CLUSTERED (Id),
);
Go

--CREATE UNIQUE CLUSTERED INDEX AppLogMachine_AutoId ON AppLogMachine(AutoId);
go
CREATE INDEX AppLogMachine_MacAddress ON AppLogMachine(TeamId, LoginDate, MacAddress);
Go
CREATE INDEX AppLogMachine_IpAddress ON AppLogMachine(TeamId, LoginDate, IpAddress);
Go


--------------------------------------------------------------------------------------------
-- Drop Table AppLogMessage; -- 員工簡訊紀錄
--------------------------------------------------------------------------------------------
CREATE TABLE AppLogMessage (
	--Id               UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	Id               int IDENTITY(1,1) NOT NULL,
	TeamId         NVARCHAR(50) NOT NULL,	--客戶代號,村里代號

    SendNo           NVARCHAR(100) NOT NULL, -- Email，行動電話
    -- SendName      NVARCHAR(50) NOT NULL, -- 用戶姓名
    SendType         int NOT NULL, -- 1=SMS, 2=Email
	IsSuccess        Bit NOT NULL DEFAULT 0, --是否成功
  	SendDate         DateTime NOT NULL, -- 簡訊日期
	SendSubject      NVARCHAR(100) null,  -- 簡訊主旨
	SendMessage      NVARCHAR(4000) null,  -- 簡訊內容
	ErrorMessage     NVARCHAR(4000) null,  -- 簡訊內容
	CONSTRAINT AppLogMessage_PrimaryKey PRIMARY KEY CLUSTERED (Id),
);
Go

--CREATE UNIQUE CLUSTERED INDEX AppLogMessage_AutoId ON AppLogMessage(AutoId);
go
CREATE INDEX AppLogMessage_SendNo ON AppLogMessage(TeamId, SendDate, SendType, SendNo);
Go


--------------------------------------------------------------------------------------------
-- Drop Table AppLogVerify; -- 簡訊驗證碼紀錄
--------------------------------------------------------------------------------------------
CREATE TABLE AppLogVerify (
	--Id           int IDENTITY(1,1) NOT NULL,
	VerifyId     UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	TeamId     NVARCHAR(50) NOT NULL,	--客戶代號,村里代號

	MobileTel    NVARCHAR(50) NOT NULL,--行動電話
  	MacAddress   NVARCHAR(50) NOT NULL,--機器編號 Media Access Control address
  	IpAddress    NVARCHAR(50) NOT NULL,--IP位址

  	IsVerify     bit NOT NULL DEFAULT 0, --IP位址
  	VerifyCode   NVARCHAR(20),--IP位址
	SendTime     DateTime NOT NULL DEFAULT GetDate(),
	VerifyTime   DateTime null, --公告日期

	Notes        NVARCHAR(200),

	CONSTRAINT AppLogVerify_PrimaryKey PRIMARY KEY CLUSTERED (VerifyId),
);
Go

--CREATE UNIQUE CLUSTERED INDEX AppLogVerify_AutoId ON AppLogVerify(AutoId);
go
CREATE INDEX AppLogVerify_MobileTel ON AppLogVerify(TeamId, MobileTel);
Go

