Alter Table AppKeyCode ADD AutoId int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX AppKeyCode_AutoId ON AppKeyCode(AutoId);
Go

Alter Table AppKeyRule ADD AutoId int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX AppKeyRule_AutoId ON AppKeyRule(AutoId);
Go
