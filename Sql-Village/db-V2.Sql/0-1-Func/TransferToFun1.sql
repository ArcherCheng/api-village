----------------------------------------------------------
-- translate sdate from 20210125 2021-01-25
----------------------------------------------------------
drop function dbo.StrToDate
go
CREATE FUNCTION dbo.StrToDate (@strDate char(8))
RETURNS nvarchar(10)
AS
BEGIN
	IF isnull(@strDate,'') = ''
		RETURN NULL;

	DECLARE @xyear CHAR(4);
	SET @xyear = SUBSTRING (@strDate,1,4);

	DECLARE @xmonth CHAR(2);
	SET @xmonth = SUBSTRING (@strDate,5,2);

	DECLARE @xday CHAR(2);
	SET @xday = SUBSTRING (@strDate,7,2);

	DECLARE @xdate CHAR(10);
	SET @xdate = @xyear + '-' + @xmonth + '-' + @xday;

	RETURN @xdate
END;
GO

----------------------------------------------------------
-- translate smon from 202101 2021-01-31
----------------------------------------------------------
drop function dbo.YYYYMMToLastDate
go

CREATE FUNCTION dbo.YYYYMMToLastDate (@strMonth nvarchar(7))
RETURNS nvarchar(10)
AS
BEGIN
	DECLARE @xyear CHAR(4);
	SET @xyear = SUBSTRING (@strMonth,1,4);

	DECLARE @xmonth CHAR(2);
	SET @xmonth = SUBSTRING (@strMonth,6,2);

	DECLARE @xdate datetime;
	SET @xdate = convert(datetime, @xyear + '-' + @xmonth + '-01' , 111);
	SET @xdate = DATEADD(month,1,@xdate);
	SET @xdate = DATEADD(day,-1,@xdate);

	RETURN @xdate
END;
GO

drop function dbo.YYYYMMToFirstDate
go

CREATE FUNCTION dbo.YYYYMMToFirstDate (@strMonth nvarchar(7))
RETURNS nvarchar(10)
AS
BEGIN
	DECLARE @xyear CHAR(4);
	SET @xyear = SUBSTRING (@strMonth,1,4);

	DECLARE @xmonth CHAR(2);
	SET @xmonth = SUBSTRING (@strMonth,6,2);

	DECLARE @xdate datetime;
	SET @xdate = convert(datetime, @xyear + '-' + @xmonth + '-01' , 111);

	RETURN @xdate
END;
GO
----------------------------------------------------------
-- translate date to month
----------------------------------------------------------
drop function dbo.DateToYYYYMM
go

CREATE FUNCTION dbo.DateToYYYYMM (@sourceDate datetime)
RETURNS nvarchar(7)
AS
BEGIN
	-- DECLARE @xyear int;
	-- SET @xyear = DATEPART(yyyy, @sourceDate);

	-- DECLARE @xmonth int;
	-- SET @xyear = DATEPART(mm, @sourceDate);

	-- DECLARE @xday int;
	-- SET @xday = DATEPART(dd, @sourceDate);

	DECLARE @isoDate nvarchar(10);
	SET @isoDate = CONVERT(nvarchar(10),@sourceDate,23);

	DECLARE @result nvarchar(7);
	SET @result = SUBSTRING(@isoDate,1,7);

	RETURN @result
END;
GO

----------------------------------------------------------
-- translate emp_no from 85027 085027
----------------------------------------------------------
drop function dbo.UpperFirstChar
go
CREATE FUNCTION dbo.UpperFirstChar (@strWord NVARCHAR(50))
RETURNS NVARCHAR(50)
AS
BEGIN
	DECLARE @xresult NVARCHAR(50);
	SET @xresult = UPPER(LEFT(@strWord,1))+LOWER(SUBSTRING(Trim(@strWord),2,LEN(Trim(@strWord))))
	RETURN @xresult
END;
GO

----------------------------------------------------------
-- AddTime(2130,8.5) = 3000
----------------------------------------------------------
drop function dbo.AddTime
go
CREATE FUNCTION dbo.AddTime(@time1 nvarchar(5) , @addhours decimal(18,3) )
RETURNS char(5)
AS
BEGIN
	DECLARE @hrs int;
	DECLARE @mins int;
	DECLARE @totalmins int;
	--SET @hrs =Cast(SUBSTRING (@time1,1,2) as int) ;
	--SET @mins =Cast(SUBSTRING (@time1,3,2) as int) ;
	select @hrs=Cast(SUBSTRING (@time1,1,2) as int),@mins=Cast(SUBSTRING (@time1,4,2) as int);
	SET @totalmins = (@hrs * 60) + @mins + (@addhours * 60);

	DECLARE @hh nvarchar(2);
	DECLARE @mm nvarchar(2);
	--SET @hh = RIGHT('00'+convert(varchar(2), @totalmins / 60 ),2) ;
	--SET @mm = RIGHT('00'+convert(varchar(2), @totalmins % 60 ),2) ;
	select @hh=RIGHT('00'+convert(varchar(2), @totalmins / 60 ),2),@mm=RIGHT('00'+convert(varchar(2), @totalmins % 60 ),2);

	RETURN @hh+':'+@mm;
END;
GO

----------------------------------------------------------
-- compare max value
----------------------------------------------------------
drop function dbo.CompareMax
go
CREATE FUNCTION dbo.CompareMax (@val1 int,@val2 int)
RETURNS int
AS
BEGIN
  IF @val1 > @val2
    RETURN @val1
  RETURN isnull(@val2,@val1)
END;
GO
----------------------------------------------------------
-- compare min value
----------------------------------------------------------
drop function dbo.CompareMin
go
CREATE FUNCTION dbo.CompareMin (@val1 int,@val2 int)
RETURNS int
AS
BEGIN
  IF @val1 < @val2
    RETURN @val1
  RETURN isnull(@val2,@val1)
END;
GO


----------------------------------------------------------
-- dbo.Max2(1,2) = 2
----------------------------------------------------------
drop function dbo.Max2
go
CREATE FUNCTION dbo.Max2(@val1 int,@val2 int)
RETURNS int
AS
BEGIN
  IF @val1 > @val2
	RETURN @val1
  RETURN @val2
END;
GO
----------------------------------------------------------
-- dbo.Min2(1,2) = 1
----------------------------------------------------------
drop function dbo.Min2
go
CREATE FUNCTION dbo.Min2(@val1 int,@val2 int)
RETURNS int
AS
BEGIN
  IF @val1 > @val2
	RETURN @val2
  RETURN @val1
END;
GO