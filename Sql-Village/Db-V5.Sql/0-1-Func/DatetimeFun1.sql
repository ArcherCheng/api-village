/*
drop function dbo.is_yyyy
go
drop function dbo.is_year
go
drop function dbo.is_yyyymm
go
drop function dbo.is_year_month
go
drop function dbo.is_hhmm
go
drop function dbo.is_time
go

drop function dbo.to_iso_date
go
drop function dbo.to_year_month
go

drop function dbo.get_year_month_LastDate
go
drop function dbo.get_year_month_first_date
go

drop function dbo.add_time_hours
go
drop function dbo.add_time_mins
go
drop function dbo.add_time_hours_mins
go
*/
----------------------------------------------------------
-- CONSTRAINT Lv4Yer10_Check_AtYear Check( Dbo.Checkis_yyyy(PayMonth)>0)
----------------------------------------------------------
IF OBJECT_ID('dbo.is_yyyy') IS NOT NULL
	DROP FUNCTION dbo.is_yyyy;
go


CREATE FUNCTION dbo.is_yyyy (@int_year int)
RETURNS int
AS
BEGIN
	IF @int_year = 9999
		RETURN 1;

	DECLARE @curr_year int;
	SET @curr_year = DATEPART(year,GETDATE());

	IF @int_year <  @curr_year - 100 OR @int_year > @curr_year + 100
		RETURN 0;

	RETURN 1;
END;
GO

IF OBJECT_ID('dbo.is_year') IS NOT NULL
	DROP FUNCTION dbo.is_year;
go

CREATE FUNCTION dbo.is_year (@int_year int)
RETURNS int
AS
BEGIN
	IF @int_year = 9999
		RETURN 1;

	DECLARE @curr_year int;
	SET @curr_year = DATEPART(year,GETDATE());

	IF @int_year <  @curr_year - 100 OR @int_year > @curr_year + 100
		RETURN 0;

	RETURN 1;
END;
GO

select dbo.is_year(9999) a9999,dbo.is_year(1949) a1949, dbo.is_year(1950) a1950,dbo.is_year(2040) a2040,dbo.is_year(2036) a2036,dbo.is_year(2037) a2037,dbo.is_year(20301) a20301;
go

----------------------------------------------------------
-- CONSTRAINT In3Mon10_Check_PayMonth Check( Dbo.Checkis_yyyymm(PayMonth)>0)
----------------------------------------------------------

IF OBJECT_ID('dbo.is_yyyymm') IS NOT NULL
	DROP FUNCTION dbo.is_yyyymm;
go


CREATE FUNCTION dbo.is_yyyymm (@year_month nvarchar(7))
RETURNS int
AS
BEGIN
	IF @year_month='9999-12'
		RETURN 1;

	IF Len(@year_month)<>7 or SUBSTRING(@year_month,5,1)<>'-'
		RETURN 0;

	DECLARE @curr_year int;
	SET @curr_year = DATEPART(year,GETDATE());

	DECLARE @int_year int;
	SET @int_year =CAST(SUBSTRING(@year_month,1,4) as int);
	IF @int_year <  @curr_year - 100 OR @int_year > @curr_year + 100
		RETURN 0;

	DECLARE @int_month int;
	SET @int_month =CAST(SUBSTRING(@year_month,6,2) as int);
	IF @int_month < 1 OR @int_month > 12
		RETURN 0;

	RETURN 1;
END;
GO


IF OBJECT_ID('dbo.is_year_month') IS NOT NULL
	DROP FUNCTION dbo.is_year_month;
go

CREATE FUNCTION dbo.is_year_month (@year_month nvarchar(7))
RETURNS int
AS
BEGIN
	IF @year_month='9999-12'
		RETURN 1;

	IF Len(@year_month)<>7 or SUBSTRING(@year_month,5,1)<>'-'
		RETURN 0;

	DECLARE @curr_year int;
	SET @curr_year = DATEPART(year,GETDATE());

	DECLARE @int_year int;
	SET @int_year =CAST(SUBSTRING(@year_month,1,4) as int);
	IF @int_year <  @curr_year - 100 OR @int_year > @curr_year + 100
		RETURN 0;

	DECLARE @int_month int;
	SET @int_month =CAST(SUBSTRING(@year_month,6,2) as int);
	IF @int_month < 1 OR @int_month > 12
		RETURN 0;

	RETURN 1;
END;
GO

select dbo.is_year_month('9999-12') a999912,dbo.is_year_month('1949-12') a194912, dbo.is_year_month('1950-01') a195001
	,dbo.is_year_month('2026-01') a202601,dbo.is_year_month('2026-13') a202613,dbo.is_year_month('2040-12') a204012
	,dbo.is_year_month('2036-01') a203601,dbo.is_year_month('2036-12') a203612,dbo.is_year_month('2037-01') a203701;
go
select dbo.is_year_month('1999-12') a999912,dbo.is_year_month('2019-20') a194912, dbo.is_year_month('2020-05') a195001;
go

----------------------------------------------------------
-- CONSTRAINT Ot2Day10_Check_BeginTime Check( Dbo.is_hhmm(BeginTime)>0)
----------------------------------------------------------
IF OBJECT_ID('dbo.is_hhmm') IS NOT NULL
	DROP FUNCTION dbo.is_hhmm;
go

CREATE FUNCTION dbo.is_hhmm (@str_hhmm nvarchar(5))
RETURNS int
AS
BEGIN
	IF Len(@str_hhmm)<>5 or SUBSTRING(@str_hhmm,3,1)<>':'
		RETURN 0;

	DECLARE @str_hh CHAR(2);
	SET @str_hh = SUBSTRING (@str_hhmm,1,2);
	IF @str_hh < '00' OR @str_hh > '23'
		RETURN 0

	DECLARE @str_mm CHAR(2);
	SET @str_mm = SUBSTRING (@str_hhmm,4,2);
	IF @str_mm < '00' OR @str_mm > '59'
		RETURN 0

	RETURN 1
END;
GO

IF OBJECT_ID('dbo.is_time') IS NOT NULL
	DROP FUNCTION dbo.is_time;
go

CREATE FUNCTION dbo.is_time (@str_hhmm nvarchar(5))
RETURNS int
AS
BEGIN
	IF Len(@str_hhmm)<>5 or SUBSTRING(@str_hhmm,3,1)<>':'
		RETURN 0;

	DECLARE @str_hh CHAR(2);
	SET @str_hh = SUBSTRING (@str_hhmm,1,2);
	IF @str_hh < '00' OR @str_hh > '23'
		RETURN 0

	DECLARE @str_mm CHAR(2);
	SET @str_mm = SUBSTRING (@str_hhmm,4,2);
	IF @str_mm < '00' OR @str_mm > '59'
		RETURN 0

	RETURN 1
END;
GO


select dbo.is_time('99:99') a9999,dbo.is_time('24:00') a2400, dbo.is_time('00:00') a0000,dbo.is_time('12:30') a1230,dbo.is_time('12:60') a1260,dbo.is_time('a2:60') aa260;
go
select dbo.is_time('9999') a9999,dbo.is_time('2400') a2400, dbo.is_time('0000') a0000,dbo.is_time('1230') a1230,dbo.is_time('1260') a1260;
go


IF OBJECT_ID('dbo.to_iso_date') IS NOT NULL
	DROP FUNCTION dbo.to_iso_date;
go

CREATE FUNCTION dbo.to_iso_date (@str_date nvarchar(50))
RETURNS nvarchar(10)
AS
BEGIN
	IF isnull(@str_date,'') = ''
		RETURN NULL;

	IF IsDate(@str_date)=1
		return CONVERT(nvarchar(10),Convert(DateTime,@str_date),23);

	DECLARE @xyear CHAR(4);
	SET @xyear = SUBSTRING (@str_date,1,4);

	DECLARE @xmonth CHAR(2);
	SET @xmonth = SUBSTRING (@str_date,5,2);

	DECLARE @xday CHAR(2);
	SET @xday = SUBSTRING (@str_date,7,2);

	DECLARE @xdate CHAR(10);
	SET @xdate = @xyear + '-' + @xmonth + '-' + @xday;

	RETURN @xdate
END;
GO

select dbo.to_iso_date('20251126') a1,dbo.to_iso_date('20251131') a2,dbo.to_iso_date('2025/11/30 12:00:00') a3,dbo.to_iso_date('2025.4.3') a4
go
select IsDate(dbo.to_iso_date('20251126')) a1, IsDate(dbo.to_iso_date('20251131')) a2,isDate(dbo.to_iso_date('20251331')) a3
go
----------------------------------------------------------
-- translate date to month
----------------------------------------------------------
IF OBJECT_ID('dbo.to_year_month') IS NOT NULL
	DROP FUNCTION dbo.to_year_month;
go

CREATE FUNCTION dbo.to_year_month (@source_date datetime)
RETURNS nvarchar(7)
AS
BEGIN
	-- DECLARE @xyear int;
	-- SET @xyear = DATEPART(yyyy, @source_date);

	-- DECLARE @xmonth int;
	-- SET @xyear = DATEPART(mm, @source_date);

	-- DECLARE @xday int;
	-- SET @xday = DATEPART(dd, @source_date);

	-- DECLARE @isoDate nvarchar(10);
	-- SET @isoDate = CONVERT(nvarchar(10),@source_date,23);

	-- DECLARE @result nvarchar(7);
	-- SET @result = SUBSTRING(@isoDate,1,7);

	--RETURN @result
	return Substring(Convert(nvarchar(10),@source_date,23),1,7);
END;
GO
select dbo.to_year_month('2025-11-26') a1,dbo.to_year_month('2025-11-26T12:00:00') a2,dbo.to_year_month('2025/11/30 12:00:00') a3,dbo.to_year_month(GETDATE()) CUrrentDate
go



----------------------------------------------------------
-- translate smon from 2021-01 2021-01-31
----------------------------------------------------------
IF OBJECT_ID('dbo.get_year_month_LastDate') IS NOT NULL
	DROP FUNCTION dbo.get_year_month_LastDate;
go

CREATE FUNCTION dbo.get_year_month_LastDate (@str_year_month nvarchar(7))
RETURNS nvarchar(10)
AS
BEGIN
	DECLARE @xdate datetime;
	SET @xdate = convert(datetime, @str_year_month + '-01' , 111);
	SET @xdate = DATEADD(month,1,@xdate);
	SET @xdate = DATEADD(day,-1,@xdate);

	RETURN CONVERT(nvarchar(10),@xdate,23)
END;
GO

select dbo.get_year_month_LastDate('2025-11') a1,dbo.get_year_month_LastDate('2025-02') a2,dbo.get_year_month_LastDate('2024-02') a3
go

----------------------------------------------------------
-- translate smon from 2021-12 2021-12-01
----------------------------------------------------------
IF OBJECT_ID('dbo.get_year_month_first_date') IS NOT NULL
	DROP FUNCTION dbo.get_year_month_first_date;
go

CREATE FUNCTION dbo.get_year_month_first_date (@str_year_month nvarchar(7))
RETURNS nvarchar(10)
AS
BEGIN
	DECLARE @xdate nvarchar(10);
	SET @xdate = CONVERT( nvarchar(10), CONVERT(date, @str_year_month + '-01', 23) , 23);

	RETURN @xdate
END;
GO

select dbo.get_year_month_first_date('2025-11') a1,dbo.get_year_month_first_date('2025-02') a2,dbo.get_year_month_first_date('2024-02') a3
go


----------------------------------------------------------
-- add_time_hours(21:30,8.5) = 30:00
----------------------------------------------------------
IF OBJECT_ID('dbo.add_time_hours') IS NOT NULL
	DROP FUNCTION dbo.add_time_hours;
go
CREATE FUNCTION dbo.add_time_hours(@time1 nvarchar(5) , @addhours decimal(18,3) )
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

select dbo.add_time_hours('13:45',3.5) H13M45_H3M30,dbo.add_time_hours('13:30',3.5)  H13M30_H3M30,dbo.add_time_hours('16:21',1.5)  H16M21_H1M30 ,dbo.add_time_hours('19:21',6.5) H19M21_H1M30
go

----------------------------------------------------------
-- add_time_mins(2130,30) = 22:00
----------------------------------------------------------
IF OBJECT_ID('dbo.add_time_mins') IS NOT NULL
	DROP FUNCTION dbo.add_time_mins;
go
CREATE FUNCTION dbo.add_time_mins(@time1 nvarchar(5) , @addmins int )
RETURNS char(5)
AS
BEGIN
	DECLARE @hrs int;
	DECLARE @mins int;
	DECLARE @totalmins int;
	--SET @hrs =Cast(SUBSTRING (@time1,1,2) as int) ;
	--SET @mins =Cast(SUBSTRING (@time1,3,2) as int) ;
	select @hrs=Cast(SUBSTRING (@time1,1,2) as int),@mins=Cast(SUBSTRING (@time1,4,2) as int);
	SET @totalmins = (@hrs * 60) + @mins + @addmins;

	DECLARE @hh nvarchar(2);
	DECLARE @mm nvarchar(2);
	--SET @hh = RIGHT('00'+convert(varchar(2), @totalmins / 60 ),2) ;
	--SET @mm = RIGHT('00'+convert(varchar(2), @totalmins % 60 ),2) ;
	select @hh=RIGHT('00'+convert(varchar(2), @totalmins / 60 ),2),@mm=RIGHT('00'+convert(varchar(2), @totalmins % 60 ),2);

	RETURN @hh+':'+@mm;
END;
GO

select dbo.add_time_mins('13:45',35) H13M45_M35,dbo.add_time_mins('13:30',35) H13M30_M35,dbo.add_time_mins('16:21',15) H16M21_M15,dbo.add_time_mins('19:21',60) H19M21_M60
go


----------------------------------------------------------
-- add_time_hours_mins(21:30,8.5) = 30:00
----------------------------------------------------------
IF OBJECT_ID('dbo.add_time_hours_mins') IS NOT NULL
	DROP FUNCTION dbo.add_time_hours_mins;
go
CREATE FUNCTION dbo.add_time_hours_mins(@time1 nvarchar(5) , @addhours int,@addmins int)
RETURNS char(5)
AS
BEGIN
	DECLARE @hrs int;
	DECLARE @mins int;
	DECLARE @totalmins int;
	--SET @hrs =Cast(SUBSTRING (@time1,1,2) as int) ;
	--SET @mins =Cast(SUBSTRING (@time1,3,2) as int) ;
	select @hrs=Cast(SUBSTRING (@time1,1,2) as int),@mins=Cast(SUBSTRING (@time1,4,2) as int);
	SET @totalmins = (@hrs * 60) + @mins + (@addhours * 60) + @addmins;

	DECLARE @hh nvarchar(2);
	DECLARE @mm nvarchar(2);
	--SET @hh = RIGHT('00'+convert(varchar(2), @totalmins / 60 ),2) ;
	--SET @mm = RIGHT('00'+convert(varchar(2), @totalmins % 60 ),2) ;
	select @hh=RIGHT('00'+convert(varchar(2), @totalmins / 60 ),2),@mm=RIGHT('00'+convert(varchar(2), @totalmins % 60 ),2);

	RETURN @hh+':'+@mm;
END;
GO

select dbo.add_time_hours_mins('13:45',3,30) H13M45_H3M30,dbo.add_time_hours_mins('13:30',3,30)  H13M30_H3M30,dbo.add_time_hours_mins('16:21',1,39)  H16M21_H1M30 ,dbo.add_time_hours_mins('19:21',6,30) H19M21_H1M30
go

