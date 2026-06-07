--------------------------------------------------------------------------------
drop view ViewPt2LikeCounts
go

create view ViewPt2LikeCounts as
SELECT a.FromId,COUNT(*) as LikeCounts
FROM Pt2Like a
Group By a.FromId
go

--------------------------------------------------------------------------------
drop view ViewPt2UnLikeCounts
go

create view ViewPt2UnLikeCounts as
SELECT a.FromId,COUNT(*) as UnLikeCounts
FROM Pt2UnLike a
Group By a.FromId
go