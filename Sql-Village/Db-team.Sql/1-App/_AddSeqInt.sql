Alter Table Au1KeyCode ADD AutoId int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX Au1KeyCode_AutoId ON Au1KeyCode(AutoId);
Go

Alter Table Au1KeyRule ADD AutoId int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX Au1KeyRule_AutoId ON Au1KeyRule(AutoId);
Go
