
--------------------------------------------------------------------------------
--PartyChatGroup
/*
drop table ChatConnection
go

drop table ChatGroup
go

drop table ChatConnection
go


*/


--drop table ChatGroup
go
CREATE TABLE ChatGroup
(
	ChatGroupId    NVARCHAR(200) NOT NULL,
	ChatGroupName  NVARCHAR(200) NOT NULL,
	CreateDateTime DateTime NOT NULL Default GetDate(),

	--以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser NVARCHAR(100),
	CONSTRAINT ChatGroup_PrimaryKey PRIMARY KEY (ChatGroupId)
);
go



--------------------------------------------------------------------------------
--drop table ChatConnection
go
CREATE TABLE ChatConnection
(
	ConnectionId   NVARCHAR(100) NOT NULL,
	ChatGroupId    NVARCHAR(200) NOT NULL,
	MemberId       NVARCHAR(100) NOT NULL,
	MemberName     NVARCHAR(100) NOT NULL,
	CreateDateTime DateTime NOT NULL Default GetDate(),

	--以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser NVARCHAR(100),
	CONSTRAINT ChatConnection_PrimaryKey PRIMARY KEY (ConnectionId)
);

go
CREATE UNIQUE INDEX ChatConnection_ConnectionId ON ChatConnection(ConnectionId) ;
go


Alter Table ChatConnection add CONSTRAINT ChatConnection_ChatGroup
	FOREIGN KEY (ChatGroupId)
	REFERENCES ChatGroup(ChatGroupId)
	ON UPDATE CASCADE
	ON DELETE CASCADE
go
