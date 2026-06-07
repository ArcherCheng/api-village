----------------------------------------------------------------------------------------------
drop view ViewPt2ForumCounts
go
create view ViewPt2ForumCounts as
SELECT a.ForumId,COUNT(ReplyId) ReplyCounts, Max(a.CreateTime) ReplyLastTime
FROM Pt2ForumReply a
Group By a.ForumId
go

-- select * from ViewPt2ForumCounts
go

----------------------------------------------------------------------------------------------
drop view ViewPt2ForumX1
go

create view ViewPt2ForumX1 as
SELECT a.ForumId,a.GroupType,a.Subject,a.Contents,a.ReadTimes,a.IsDelete
,a.IsTop,a.TopDays,a.CreateTime, DATEADD(day,a.TopDays,a.CreateTime) as TopEndDate
,x1.UserId,x1.UserAlias
,y1.TeamId,y1.TeamName
,z1.LikeCounts,z2.UnLikeCounts
,z3.ReplyCounts,z3.ReplyLastTime
FROM Pt2Forum a
INNER JOIN Au1User x1 ON a.UserId = x1.UserId
INNER JOIN Au1Team10 y1 ON y1.TeamId = x1.TeamId
left join ViewPt2LikeCounts z1 ON a.ForumId = z1.FromId
left join ViewPt2UnLikeCounts z2 ON a.ForumId = z2.FromId
left join ViewPt2ForumCounts z3 ON a.ForumId = z3.ForumId
go


-- select * from ViewPt2ForumX1
go

----------------------------------------------------------------------------------------
drop view ViewPt2ForumReplyX1
go

create view ViewPt2ForumReplyX1 as
SELECT a.ReplyId,a.ForumId,a.Contents,a.IsDelete
,x1.UserId,x1.UserAlias
,y1.TeamId,y1.TeamName
,z1.LikeCounts,z2.UnLikeCounts
FROM Pt2ForumReply a
INNER JOIN Pt2Forum b ON a.ForumId = a.ForumId
INNER JOIN Au1User x1 ON a.UserId = x1.UserId
INNER JOIN Au1Team10 y1 ON y1.TeamId = x1.TeamId
left join ViewPt2LikeCounts z1 ON a.ReplyId = z1.FromId
left join ViewPt2UnLikeCounts z2 ON a.ReplyId = z2.FromId

go
-- select * from ViewPt2ForumReplyX1
go