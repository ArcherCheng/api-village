--------------------------------------------------------------------------------------------
-- 用戶使用機器登入驗證表
-- Drop Table AppUserMachine;
--------------------------------------------------------------------------------------------
CREATE TABLE AppUserMachine (
	Id                BIGINT IDENTITY(1,1) NOT NULL,
	--Id               UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),

  	VerifyCode       NVARCHAR(100) NOT NULL,--驗證碼
  	VerifyMinutes    INT ,    --驗證有效分鐘數 1440分鐘=一天, 10080分鐘=一週, 43200分鐘=一個月
  	CanVerifyTime    AS DATEADD(MINUTE,VerifyMinutes,WriteTime), --可驗證時間=寫入時間+驗證有效分鐘數
  	IsVerified       BIT NOT NULL, --是否已驗證
  	ErrorTimes       INT ,    --錯誤次數
	Notes			 NVARCHAR(100) , --備註

	UserId           UniqueIdentifier, --用戶代號
  	MacGuid          NVARCHAR(100),--機器編號 Media Access Control address
  	IpAddress        NVARCHAR(100),--IP位址
	TeamId           NVARCHAR(100), --村里代號
	WriteTime        Datetime Default Getdate(),  --查詢時間

	CONSTRAINT  AppUserMachine_PrimaryKey PRIMARY KEY CLUSTERED (Id),
);
Go

CREATE UNIQUE INDEX Inx_UserId  ON AppUserMachine(UserId, MacGuid, IpAddress);
Go



