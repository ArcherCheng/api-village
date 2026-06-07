/*

Drop table Au1Action
go
Drop table Au1Component
go

*/


--------------------------------------------------------------------
-- Drop table Au1Action
--------------------------------------------------------------------
CREATE TABLE Au1Action
(
	--AutoId        int IDENTITY(1,1) NOT NULL,
	--ActionId  UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),

	CtrlActnId      NVARCHAR(100) NOT NULL,  --程式動作編號
	CtrllerId       NVARCHAR(100)  NOT NULL,  --程式編號
	ActionId        NVARCHAR(100)  NOT NULL,  --動作編號
	CtrllerDesc     NVARCHAR(100)  NOT NULL,  --程式說明
	ActionDesc      NVARCHAR(100)  NOT NULL,  --動作說明
	HttpMethod      NVARCHAR(100)  NOT NULL,  --HTTP方法
	HttpRoute       NVARCHAR(200) NOT NULL,  --HTTP路由
	IsRbacAuthorize BIT NOT NULL DEFAULT 0,  --檢查角色權限
	SpaSystem       NVARCHAR(100)  null,   --首頁功能表代號

	ExternalUrl     NVARCHAR(200),   --外部URL
	SortOrder      INT NOT NULL DEFAULT 0, --排序
	Notes           NVARCHAR(200),  --備註說明
	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Au1Action_PrimaryKey PRIMARY KEY CLUSTERED (CtrlActnId)
);
Go

--CREATE UNIQUE CLUSTERED INDEX Au1Action_AutoId ON Au1Action(AutoId);
go
CREATE UNIQUE INDEX Au1Action_CtrllerAction ON Au1Action (CtrllerId asc, ActionId asc);
go
--CREATE UNIQUE INDEX Au1Action_HttpRoute ON Au1Action (HttpMethod asc, HttpRoute asc);
go


--------------------------------------------------------------------
-- Drop table Au1Component
--------------------------------------------------------------------
CREATE TABLE Au1Component
(
	--AutoId        int IDENTITY(1,1) NOT NULL,

	ComponentId   NVARCHAR(100) NOT NULL,  --元件編號
	SystemId      NVARCHAR(100) NOT NULL,  --系統編號
	SubGroup      NVARCHAR(100) NOT NULL,  --子系統編號
	ComponentDesc NVARCHAR(100) null,   --元件說明
	SystemDesc    NVARCHAR(100) null,   --系統說明
	SortOrder    INT NOT NULL DEFAULT 0,  --排序
	Notes         NVARCHAR(200) null,   --備註說明
	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Au1Component_PrimaryKey PRIMARY KEY CLUSTERED (ComponentId) ,
);
Go

--CREATE UNIQUE CLUSTERED INDEX Au1Component_AutoId ON Au1Action(AutoId);
go











