
---------------------------------------------------------------------------------------------------------------
drop view ViewPartyChatGroupTotalChats
go
create view ViewPartyChatGroupTotalChats as
select a.PartyId,count(a.PartyId) TotalGroupChats
from PartyChatGroup a
group by a.PartyId
go
-- select * from ViewPartyChatGroupTotalChats
go

drop view ViewPartyChatGroupTotalBoyChats
go
create view ViewPartyChatGroupTotalBoyChats as
select a.PartyId,count(a.PartyId) TotalGroupBoyChats
from PartyChatGroup a,MemberData b
where a.SenderId = b.MemberId and b.Sex=1
group by a.PartyId
go
-- select * from ViewPartyChatGroupTotalBoyChats
go

drop view ViewPartyChatGroupTotalGirlChats
go
create view ViewPartyChatGroupTotalGirlChats as
select a.PartyId,count(a.PartyId) TotalGroupGirlChats
from PartyChatGroup a,MemberData b
where a.SenderId = b.MemberId and b.Sex=2
group by a.PartyId
go
-- select * from ViewPartyChatGroupTotalGirlChats
go
---------------------------------------------------------------------------------------------------
drop view ViewPartyChatOtherTotalChats
go
create view ViewPartyChatOtherTotalChats as
select a.PartyId,count(a.PartyId) TotalOtherChats
from PartyChatOther a
group by a.PartyId
go
-- select * from ViewPartyChatOtherTotalChats
go

drop view ViewPartyChatOtherTotalBoyChats
go
create view ViewPartyChatOtherTotalBoyChats as
select a.PartyId,count(a.PartyId) TotalOtherBoyChats
from PartyChatOther a,MemberData b
where a.SenderId = b.MemberId and b.Sex=1
group by a.PartyId
go
-- select * from ViewPartyChatOtherTotalBoyChats
go

drop view ViewPartyChatOtherTotalGirlChats
go
create view ViewPartyChatOtherTotalGirlChats as
select a.PartyId,count(a.PartyId) TotalOtherGirlChats
from PartyChatOther a,MemberData b
where a.SenderId = b.MemberId and b.Sex=2
group by a.PartyId
go
-- select * from ViewPartyChatOtherTotalGirlChats
go


---------------------------------------------------------------------------------------------------------------
--drop view ViewPartyChat
--go
--create view ViewPartyChat as
--select PartyId,SenderID,RecipientId,max(SendDate) SendDate
--from PartyChat
--group by PartyId,SenderID,RecipientId
--go
--select * from ViewPartyChat
--go
---------------------------------------------------------------------------------------------------------------
drop view ViewPartyChatGroupReaderX1Counts
go
create view ViewPartyChatGroupReaderX1Counts as
select ChatId,COUNT(MemberId) as ReadCounts from PartyChatGroupReader Group by ChatId
go

-- select * from ViewPartyChatGroupReaderX1Counts
-- go
---------------------------------------------------------------------------------------------------------------
drop view ViewPartyChatGroup
go
create view ViewPartyChatGroup as
SELECT a.ChatId,a.PartyId,a.SenderID,a.SendDateTime,a.SendContents,a.SenderDeleted
    ,x1.PartyName,x1.PartyDate,x1.PictureUrl
    ,y1.MemberName as SenderName,y1.PhotoUrl as SenderPhotoUrl
	,z2.ReadCounts
FROM PartyChatGroup a
INNER JOIN PartyData x1 ON a.PartyId = x1.PartyId
INNER JOIN MemberData y1 ON a.SenderId = y1.MemberId
LEFT JOIN ViewPartyChatGroupTotalChats z1 ON a.PartyId = z1.PartyId
LEFT JOIN ViewPartyChatGroupReaderX1Counts z2 ON a.ChatId = z2.ChatId
where x1.PartyDate >= DATEADD(MONTH, -1,  GETDATE())
go

-- select * from ViewPartyChatGroup
-- go


---------------------------------------------------------------------------------------------------------------
drop view ViewPartyChatOther
go
create view ViewPartyChatOther as
SELECT a.ChatId,a.PartyId,a.SenderID,a.SendDateTime,a.SendContents,a.SenderDeleted
    ,a.RecipientId,a.ReadDateTime,a.IsRead,a.RecipientDeleted
    ,x1.PartyName,x1.PartyDate,x1.PictureUrl
    ,y1.MemberName as SenderName,y1.PhotoUrl as SenderPhotoUrl
    ,y2.MemberName as RecipientName,y2.PhotoUrl as RecipientPhotoUrl
FROM PartyChatOther a
INNER JOIN PartyData x1 ON x1.PartyId = a.PartyId
INNER JOIN MemberData y1 ON a.SenderId = y1.MemberId
INNER JOIN MemberData y2 ON a.RecipientId = y2.MemberId
where x1.PartyDate >= DATEADD(MONTH, -1,  GETDATE())
go

-- select * from ViewPartyChatOther
-- go