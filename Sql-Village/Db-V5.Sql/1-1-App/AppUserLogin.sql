-----------------------------------------
-- 記錄使用者的登入記錄
-- Drop Table AppUserLogin
-----------------------------------------
CREATE TABLE AppUserLogin(
	Id          BIGINT IDENTITY(1,1) NOT NULL,  --序號
    LoginNname  NVARCHAR(100),  --用戶代號(XXX，登入成功)
    LoginStatus NVARCHAR(100) NOT NULL,  --登入狀態
	IsSuccess   BIT,  --是否成功

	TeamId      NVARCHAR(100), --村里代號
	IpAddress   NVARCHAR(100),  --IP位置
	MacGuid     NVARCHAR(100),  --MAC位置
	WriteTime   Datetime Default Getdate(),  --查詢時間
	CONSTRAINT AppUserLogin_PrimaryKey PRIMARY KEY CLUSTERED (Id)
);
Go


Go
CREATE INDEX Inx_LoginName ON AppUserLogin(LoginNname);
Go
CREATE INDEX Inx_IpAddress ON AppUserLogin(IpAddress);
Go
CREATE INDEX Inx_MacGuid ON AppUserLogin(MacGuid);
Go
CREATE INDEX Inx_WriteTime ON AppUserLogin(WriteTime);
Go

