---------------------------------------------------------------------------------------------------------------
drop view ViewMemberChat
go
create view ViewMemberChat as
SELECT a.Id,a.SenderID,a.SendDateTime,a.SendContents,a.SenderDeleted
    ,a.RecipientId,a.ReadDateTime,a.IsRead,a.RecipientDeleted
    ,y1.MemberName as SenderName,y1.PhotoUrl as SenderPhotoUrl
    ,y2.MemberName as RecipientName,y2.PhotoUrl as RecipientPhotoUrl
FROM MemberChat a
INNER JOIN MemberData y1 ON a.SenderId = y1.MemberId
INNER JOIN MemberData y2 ON a.RecipientId = y2.MemberId
go
-- select * from ViewMemberChatX1
-- go

