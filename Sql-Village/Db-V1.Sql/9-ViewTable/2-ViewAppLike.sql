--------------------------------------------------------------------------------
drop view ViewAppLikeCounts
go

create view ViewAppLikeCounts as 
SELECT a.FromId,COUNT(1) as LikeCounts
FROM AppLike a 
WHERE a.IsLike = 1
Group By a.FromId
go

--------------------------------------------------------------------------------
drop view ViewAppUnLikeCounts
go

create view ViewAppUnLikeCounts as 
SELECT a.FromId,COUNT(1) as UnLikeCounts
FROM AppLike a
WHERE a.IsLike = 0
Group By a.FromId
go