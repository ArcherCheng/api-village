Alter Table Au1Ctrller ADD AutoId int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX Au1Ctrller_AutoId ON Au1Ctrller(AutoId);
Go

Alter Table Au1Action ADD AutoId int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX Au1Action_AutoId ON Au1Action(AutoId);
Go

Alter Table Au1User ADD AutoId int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX Au1User_AutoId ON Au1User(AutoId);
Go

Alter Table Au1Role ADD AutoId int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX Au1Role_AutoId ON Au1Role(AutoId);
Go

Alter Table Au2RoleUser ADD AutoId int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX Au2RoleUser_AutoId ON Au2RoleUser(AutoId);
Go

Alter Table Au2RoleAction ADD AutoId int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX Au2RoleAction_AutoId ON Au2RoleAction(AutoId);
Go

Alter Table Au1UserPasswordLog ADD AutoId int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX Au1UserPasswordLog_AutoId ON Au1UserPasswordLog(AutoId);
Go
