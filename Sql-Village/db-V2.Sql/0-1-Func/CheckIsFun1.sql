----------------------------------------------------------
-- CONSTRAINT Lv4Yer10_Check_AtYear Check( Dbo.CheckIsYYYY(PayMonth)>0)
----------------------------------------------------------
drop function dbo.CheckIsYYYY
go
CREATE FUNCTION dbo.CheckIsYYYY (@intYear int)
RETURNS int
AS
BEGIN
	IF @intYear = 9999
		RETURN 1

	DECLARE @currYear int;
	SET @currYear = DATEPART(year,GETDATE());

	IF @intYear < 1970 OR @intYear > @currYear + 5
		RETURN 0
	RETURN 1
END;
GO


----------------------------------------------------------
-- CONSTRAINT In3Mon10_Check_PayMonth Check( Dbo.CheckIsYYYYMM(PayMonth)>0)
----------------------------------------------------------
drop function dbo.CheckIsYYYYMM
go
CREATE FUNCTION dbo.CheckIsYYYYMM (@yearMonth nvarchar(7))
RETURNS int
AS
BEGIN
	IF @yearMonth < '1970-01'
		RETURN 0

	IF @yearMonth = '9999-12'
		RETURN 1

	DECLARE @maxYearMonth int;
	SET @maxYearMonth =CAST((DATEPART(year,GETDATE())+5) as varchar(4)) +'-12';

	IF @yearMonth > @maxYearMonth
	RETURN 0

	DECLARE @strMM int;
	SET @strMM = SUBSTRING(@yearMonth,6,2);
	IF @strMM < '01' OR @strMM > '12'
		RETURN 0

	RETURN 1
END;
GO

----------------------------------------------------------
-- CONSTRAINT Ot2Day10_Check_BeginTime Check( Dbo.CheckIsHHMM(BeginTime)>0)
----------------------------------------------------------
drop function dbo.CheckIsHHMM
go
CREATE FUNCTION dbo.CheckIsHHMM (@strHHMM nvarchar(5))
RETURNS int
AS
BEGIN
	IF @strHHMM = '99:99'
	RETURN 1

	DECLARE @strHH CHAR(2);
	SET @strHH = SUBSTRING (@strHHMM,1,2);
	IF @strHH < '00' OR @strHH > '24'
		RETURN 0

	DECLARE @strMM CHAR(2);
	SET @strMM = SUBSTRING (@strHHMM,4,2);
	IF @strMM < '00' OR @strMM > '59'
		RETURN 0

	RETURN 1
END;
GO


----------------------------------------------------------
-- translate sdate from 20210125 2021-01-25
----------------------------------------------------------
drop function dbo.IsStrDate
go
CREATE FUNCTION dbo.IsStrDate (@strDate char(8))
RETURNS int
AS
BEGIN
	DECLARE @xyear CHAR(4);
	SET @xyear = SUBSTRING (@strDate,1,4);

	DECLARE @xmonth CHAR(2);
	SET @xmonth = SUBSTRING (@strDate,5,2);

	DECLARE @xday CHAR(2);
	SET @xday = SUBSTRING (@strDate,7,2);

	DECLARE @xdate CHAR(10);
	SET @xdate = @xyear + '-' + @xmonth + '-' + @xday;

	IF NOT (@xyear BETWEEN '0000' AND '9999') RETURN (0);
    IF NOT (@xmonth BETWEEN '01' AND '12') RETURN (0);
    IF NOT (@xday BETWEEN '01' AND '31') RETURN (0);
	IF ISDATE(@xdate)=0 RETURN (0);

	RETURN 1
END;
GO
