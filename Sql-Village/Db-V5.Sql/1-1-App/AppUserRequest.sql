-----------------------------------------
-- 記錄使用者的請求訪視網頁記錄
-- Drop Table AppUserRequest
-----------------------------------------
CREATE TABLE AppUserRequest(
	Id           BIGINT IDENTITY(1,1) NOT NULL, --序號
	UserIdName   NVARCHAR(100) ,  --用戶代號
	ComponentId  NVARCHAR(100) ,  --系統程式編號
	ControllerId NVARCHAR(100) ,  --系統程式編號
	ActionId     NVARCHAR(100) ,  --執行動作編號
	HttpVerb     NVARCHAR(100) ,  --HTTP方法
	HttpRoute    NVARCHAR(400) ,  --HTTP網址
	QueryString  NVARCHAR(2000) ,  --查詢字串
	IsSuccess    Bit , --是否成功

	UserId    UniqueIdentifier, --用戶代號
  	MacGuid   NVARCHAR(100),--機器編號 Media Access Control address
  	IpAddress NVARCHAR(100),--IP位址
	TeamId    NVARCHAR(100), --村里代號
	WriteTime Datetime Default Getdate(),  --查詢時間

	CONSTRAINT AppUserRequest_PrimaryKey PRIMARY KEY CLUSTERED (Id)
);
Go

CREATE INDEX Inx_UserId ON AppUserRequest(UserIdName,WriteTime);
Go
CREATE INDEX Inx_ComponentId ON AppUserRequest(ComponentId,UserIdName,WriteTime);
Go
CREATE INDEX Inx_TeamId  ON AppUserRequest(TeamId,UserIdName,WriteTime);
Go
CREATE INDEX Inx_ControllerId ON AppUserRequest(ControllerId,UserIdName,WriteTime);
Go
