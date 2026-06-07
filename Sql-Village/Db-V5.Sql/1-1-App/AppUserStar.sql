-----------------------------------------
-- 記錄使用者查詢資料的紀錄表
-- Drop Table AppUserStar
-----------------------------------------
CREATE TABLE AppUserStar(
	Id        BIGINT IDENTITY(1,1) NOT NULL,
	--Id      UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),

  	SourceTable NVARCHAR(100) NOT Null,    --查詢資料表
  	SourceId    UniqueIdentifier NOT Null,    --查詢資料ID
  	LikeStar    INT NOT Null,    --查詢資料ID

	UserId     UniqueIdentifier, --用戶代號
  	MacGuid    NVARCHAR(100),--機器編號 Media Access Control address
  	IpAddress  NVARCHAR(100),--IP位址
	TeamId     NVARCHAR(100), --村里代號
	WriteTime  Datetime Default Getdate(),  --查詢時間

	CONSTRAINT AppUserStar_PrimaryKey PRIMARY KEY CLUSTERED (Id),
);
GO

-- CREATE UNIQUE CLUSTERED INDEX AppUserStar_AutoId ON AppUserStar(AutoId);
-- go
CREATE INDEX AppUserStar_SourceId  ON AppUserStar(SourceId);
GO
CREATE INDEX AppUserStar_TeamId  ON AppUserStar(TeamId);
GO
CREATE INDEX AppUserStar_UserId  ON AppUserStar(UserId);
GO