--delete AppUser
--delete PartyData
go
select * from AppUser
go
select * from MemberData order by MemberId
go
select * from PartyData
go

--truncate table PartyMember
--go

select * from PartyMember
go

select * from MemberPhoto
go
UPDATE MemberPhoto set IsMain=0

UPDATE MemberPhoto set IsMain=1 from MemberPhoto a where id = (select MIN(Id) from MemberPhoto b where a.MemberId=b.MemberId)

UPDATE MemberData set PhotoUrl=b.photoUrl from MemberData a,MemberPhoto b where a.MemberId=b.MemberId and b.IsMain=1

