Alter Table AppKeyCode ADD auto_id int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX Inx_auto_id ON AppKeyCode(auto_id);
Go

Alter Table AppKeyRule ADD auto_id int IDENTITY(1,1);
Go
CREATE UNIQUE CLUSTERED INDEX Inx_auto_id ON AppKeyRule(auto_id);
Go
