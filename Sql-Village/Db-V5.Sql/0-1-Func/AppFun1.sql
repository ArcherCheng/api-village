----------------------------------------------------------
-- translate emp_no from a85027 A085027
----------------------------------------------------------
IF OBJECT_ID('dbo.sumReadTimes') IS NOT NULL
    DROP FUNCTION dbo.sumReadTimes;
go

CREATE FUNCTION dbo.sumReadTimes (@sourceId uniqueidentifier) RETURNS int AS BEGIN
    DECLARE @result int;
    SELECT @result = SUM(1) FROM AppUserLike WHERE SourceId = @sourceId;
    RETURN @result;
END;
go


IF OBJECT_ID('dbo.avgLikeStars') IS NOT NULL
    DROP FUNCTION dbo.avgLikeStars;
go

CREATE FUNCTION dbo.avgLikeStars (@sourceId uniqueidentifier) RETURNS DECIMAL(4,1) AS BEGIN
    DECLARE @result DECIMAL(4,1);
    SELECT @result = AVG(LikeStar) FROM AppUserStar WHERE SourceId = @sourceId;
    RETURN @result;
END;
go
