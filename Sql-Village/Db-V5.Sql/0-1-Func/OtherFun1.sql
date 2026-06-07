/*
drop function dbo.max_value
go
drop function dbo.compare_greater_value
go
drop function dbo.min_value
go
drop function dbo.compare_less_value
go
drop function dbo.upper_first_char
go


*/
----------------------------------------------------------
-- dbo.MyMax(1,2) = 2
----------------------------------------------------------
IF OBJECT_ID('dbo.max_value') IS NOT NULL
	DROP FUNCTION dbo.max_value;
go

CREATE FUNCTION dbo.max_value(@val1 DECIMAL,@val2 DECIMAL)
RETURNS DECIMAL
AS
BEGIN
  IF @val1 > @val2
	RETURN @val1
  RETURN @val2
END;
GO

select dbo.max_value(12,15),dbo.max_value(12.9,15.0),dbo.max_value('12','15'),dbo.max_value('12.2','15.6')
go

----------------------------------------------------------
-- compare max value
----------------------------------------------------------
IF OBJECT_ID('dbo.compare_greater_value') IS NOT NULL
	DROP FUNCTION dbo.compare_greater_value;
go
CREATE FUNCTION dbo.compare_greater_value (@val1 nvarchar(100),@val2 nvarchar(100))
RETURNS nvarchar(100)
AS
BEGIN
  IF @val1 > @val2
    RETURN @val1
  RETURN isnull(@val2,@val1)
END;
GO

select dbo.compare_greater_value('12','15'),dbo.compare_greater_value('12.2','15.6'),dbo.compare_greater_value(124.2,155.6)
go

----------------------------------------------------------
-- dbo.MyMin(1,2) = 1
----------------------------------------------------------
IF OBJECT_ID('dbo.min_value') IS NOT NULL
	DROP FUNCTION dbo.min_value;
go
CREATE FUNCTION dbo.min_value(@val1 DECIMAL,@val2 DECIMAL)
RETURNS DECIMAL
AS
BEGIN
  IF @val1 > @val2
	RETURN @val2
  RETURN @val1
END;
GO
select dbo.min_value(12,15),dbo.min_value(12.9,15.0),dbo.min_value('12','15'),dbo.min_value('12.2','15.6')
go
----------------------------------------------------------
-- compare min value
----------------------------------------------------------
IF OBJECT_ID('dbo.compare_less_value') IS NOT NULL
	DROP FUNCTION dbo.compare_less_value;
go
CREATE FUNCTION dbo.compare_less_value (@val1 nvarchar(100),@val2 nvarchar(100))
RETURNS nvarchar(100)
AS
BEGIN
  IF @val1 < @val2
    RETURN @val1
  RETURN isnull(@val2,@val1)
END;
GO
select dbo.compare_less_value('12','15'),dbo.compare_less_value('12.2','15.6'),dbo.compare_less_value(123.2,125.6),dbo.compare_less_value('123.2',125.6)
go


----------------------------------------------------------
-- translate emp_no from a85027 A085027
----------------------------------------------------------
IF OBJECT_ID('dbo.upper_first_char') IS NOT NULL
	DROP FUNCTION dbo.upper_first_char;
go
CREATE FUNCTION dbo.upper_first_char (@str_word NVARCHAR(500))
RETURNS NVARCHAR(500)
AS
BEGIN
	DECLARE @xresult NVARCHAR(500);
	SET @xresult = UPPER(LEFT(@str_word,1))+LOWER(SUBSTRING(Trim(@str_word),2,LEN(Trim(@str_word))))
	RETURN @xresult
END;
GO

select dbo.upper_first_char('abc001 A32131 ') a1,dbo.upper_first_char('aBC001') a2,dbo.upper_first_char('abC001') a3
go

