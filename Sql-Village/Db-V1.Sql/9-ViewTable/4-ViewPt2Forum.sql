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
,P1.ReplyCounts,P1.ReplyLastTime
,U.UserName,U.TeamId
,V1.LikeCounts,V2.UnLikeCounts
FROM Pt2Forum a
left join ViewPt2ForumCounts P1 ON a.ForumId = P1.ForumId
inner join AppUser U ON A.UserId = U.UserId
LEFT join ViewAppLikeCounts V1 ON A.ForumId = V1.FromId
LEFT join ViewAppUnLikeCounts V2 ON A.ForumId = V2.FromId
go

-- select * from ViewPt2ForumX1
go

----------------------------------------------------------------------------------------
drop view ViewPt2ForumReplyX1
go

create view ViewPt2ForumReplyX1 as
SELECT a.ReplyId,a.ForumId,a.Contents,a.IsDelete
,U.UserName,U.TeamId
,V1.LikeCounts,V2.UnLikeCounts
FROM Pt2ForumReply A
INNER JOIN Pt2Forum B ON A.ForumId = B.ForumId
INNER join AppUser U ON A.UserId = U.UserId
LEFT join ViewAppLikeCounts V1 ON A.ForumId = V1.FromId
LEFT join ViewAppUnLikeCounts V2 ON A.ForumId = V2.FromId
go
-- select * from ViewPt2ForumReplyX1
go