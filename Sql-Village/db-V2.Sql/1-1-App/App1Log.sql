/*
Drop Table AppLogTable;
go
Drop Table AppLogRequest;
go
Drop Table AppLogSignin;
go
Drop Table AppLogMachine;
go
*/
-----------------------------------------
-- 記錄每一筆資料的異動備份
-----------------------------------------
CREATE TABLE AppLogTable(
	Id     bigint IDENTITY(1,1) NOT NULL, --序號
	TableName  NVARCHAR(100) NOT NULL, --表格名稱
	WriteType  integer NOT NULL, --異動類別
	WriteTime  Datetime NOT NULL Default Getdate(), --異動時間
	InsertData NVARCHAR(4000) Null,  -- 異動後資料, 一定要設成 max, 因為可能會有一次多筆的更新或刪除
	DeleteData NVARCHAR(4000) Null,  -- 異動前資料, 一定要設成 max, 因為可能會有一次多筆的更新或刪除
	CONSTRAINT AppLogTable_PrimaryKey PRIMARY KEY CLUSTERED  (Id)
);
Go

CREATE INDEX AppLogTable_TableName ON AppLogTable(TableName);
Go


-----------------------------------------
-- 記錄使用者的請求訪視網頁記錄
-- Drop Table AppLogRequest
-----------------------------------------
CREATE TABLE AppLogRequest(
	Id           bigint IDENTITY(1,1) NOT NULL, --序號
	UserId       UniqueIdentifier,  --用戶代號
	CtrllerId    NVARCHAR(100) null,  --系統程式編號
	ActionId     NVARCHAR(100) null,  --執行動作編號
	HttpVerb     NVARCHAR(20) null,  --HTTP方法
	HttpRoute    NVARCHAR(100) null,  --HTTP網址
	QueryString  NVARCHAR(2000) null,  --查詢字串
	IsSuccess    Bit NOT NULL DEFAULT 0, --是否成功
	WriteIp		 NVARCHAR(50),  --查詢IP位置
	WriteTime    Datetime Default Getdate(),  --查詢時間
	CONSTRAINT   AppLogRequest_PrimaryKey PRIMARY KEY CLUSTERED (Id)
);
Go


CREATE INDEX AppLogRequest_CtrllerId ON AppLogRequest(CtrllerId);
Go
CREATE INDEX AppLogRequest_ActionId ON AppLogRequest(ActionId);
Go
CREATE INDEX AppLogRequest_UserId ON AppLogRequest(UserId);
Go
CREATE INDEX AppLogRequest_HttpRoute ON AppLogRequest(HttpRoute);
Go

-----------------------------------------
-- 記錄使用者的請求訪視網頁記錄
-- Drop Table AppLogSignin
-----------------------------------------
CREATE TABLE AppLogSignin(
	Id           bigint IDENTITY(1,1) NOT NULL,  --序號
	UserId       UniqueIdentifier,  --用戶代號
    LoginState   NVARCHAR(100) NOT NULL,  --豋入狀態
	IsSuccess    bit,  --是否成功
	WriteIp		 NVARCHAR(50),  --IP位置
	WriteMac	 NVARCHAR(50),  --MAC位置
	WriteTime    Datetime Default Getdate(),  --查詢時間
	CONSTRAINT   AppLogSignin_PrimaryKey PRIMARY KEY CLUSTERED (Id)
);
Go


Go
CREATE INDEX AppLogSignin_UserId ON AppLogSignin(UserId);
Go
CREATE INDEX AppLogSignin_WriteIp ON AppLogSignin(WriteIp);
Go
CREATE INDEX AppLogSignin_WriteMac ON AppLogSignin(WriteMac);
Go
CREATE INDEX AppLogSignin_WriteTime ON AppLogSignin(WriteTime);
Go


--------------------------------------------------------------------------------------------
-- Drop Table AppLogMachine; -- 員工密碼孌更紀錄
--------------------------------------------------------------------------------------------
CREATE TABLE AppLogMachine (
	Id               bigint IDENTITY(1,1) NOT NULL,
	--Id               UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),

	UserId           UniqueIdentifier,  --用戶代號
  	MacAddress       NVARCHAR(50) NOT NULL,--機器編號 Media Access Control address
  	IpAddress        NVARCHAR(50) NOT NULL,--IP位址

  	IsVerified       bit NOT NULL, --IP位址
  	VerifyCode       NVARCHAR(10),--IP位址

  	LoginDate        Datetime Null,    --登入日期
  	ErrorTimes       int Null,    --錯誤次數
	Notes            NVARCHAR(200),

	CONSTRAINT  AppLogMachine_PrimaryKey PRIMARY KEY CLUSTERED (Id),
);
Go
--CREATE UNIQUE CLUSTERED INDEX AppLogMachine_AutoId ON AppLogMachine(AutoId);
go
CREATE UNIQUE INDEX AppLogMachine_UserId  ON AppLogMachine(UserId, MacAddress, IpAddress);
Go


