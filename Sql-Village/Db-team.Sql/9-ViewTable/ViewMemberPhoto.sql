---------------------------------------------------------------------------------------------------------------
drop view ViewMemberPhoto
go
create view ViewMemberPhoto as
SELECT a.Id,a.MemberId,a.Descriptions,a.PhotoUrl,a.IsMain ,a.IsShow
    ,b.MemberName ,b.Nickname, b.BirthYear
FROM MemberPhoto a
INNER JOIN MemberData b ON a.MemberId = b.MemberId
go

-- select * from ViewMemberPhoto
-- go

