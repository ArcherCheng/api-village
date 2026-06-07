drop view ViewPt2BulletinX1
go

create view ViewPt2BulletinX1 As
select A.BbsId,A.BbsSubject,AtDate
--,A.DocNo,A.SpeedType,A.SecretType,A.Recipient,A.Secondary
,A.PdfFileUrl,A.IsTop,A.TopDays,A.IsDelete
,A.UserId,A.CreateTime,A.UpdateTime,A.ReadTimes
,U.UserName,U.TeamId
,V1.LikeCounts,V2.UnLikeCounts
from Pt2Bulletin A
inner join AppUser U ON A.UserId = U.UserId
LEFT join ViewAppLikeCounts V1 ON A.BbsId = V1.FromId
LEFT join ViewAppUnLikeCounts V2 ON A.BbsId = V2.FromId
go

