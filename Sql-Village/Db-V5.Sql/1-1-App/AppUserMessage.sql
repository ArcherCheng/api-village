-----------------------------------------
-- 記錄使用者的請求訪視網頁記錄
-- Drop Table AppUserMessage
-----------------------------------------
CREATE TABLE AppUserMessage(
	Id           BIGINT IDENTITY(1,1) NOT NULL, --序號
    SendNo       Nvarchar(50) not null, -- Email，行動電話
    -- SendName  Nvarchar(50) not null, -- 用戶姓名
    SendType     int not null, -- 1=SMS, 2=Email
	IsSuccess    Bit NOT NULL DEFAULT 0, --是否成功
  	SendDate     Datetime Not Null, -- 簡訊日期
	SendSubject  Nvarchar(50) null,  -- 簡訊內容
	SendMessage  Nvarchar(4000) null,  -- 簡訊內容
	ErrorMessage Nvarchar(4000) null,  -- 簡訊內容
	CONSTRAINT AppUserMessage_PrimaryKey PRIMARY KEY CLUSTERED (Id)
);
Go

CREATE INDEX Inx_SendNo ON AppUserMessage(SendNo,SendDate);
Go
CREATE INDEX Inx_SendDate ON AppUserMessage(SendDate);
Go
